using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;
using TeleTracker.BLL;
using TeleTracker.BLL.Interfaces;
using TeleTracker.BLL.Services;
using TeleTracker.Core.Interfaces;
using TeleTracker.DAL.Models;
using TeleTracker.DAL.Repositories;
using TeleTracker.Helpers;

namespace TeleTracker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private readonly string _myAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TeleTrackerContext>(item =>
                item.UseSqlServer(Configuration.GetConnectionString("DbConn")));

            services.AddCors(options =>
            {
                options.AddPolicy(_myAllowSpecificOrigins,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
            services.AddControllers();
            services.AddScoped<IAuthService, UserAuthenticationService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IServiceConfiguration>(_ =>
                new ServiceConfiguration(Configuration.GetSection("MovieDB_Key").Value));
            services.AddScoped<ICosmosConfiguration>(_ =>
                new CosmosConfiguration(Configuration.GetValue<string>("CosmosDB:EndpointUri"),
                                        Configuration.GetSection("CosmosDB").GetSection("PrimaryKey").Value,
                                        Configuration.GetSection("CosmosDB").GetSection("DatabaseId").Value));
            services.AddScoped<IShowService, ShowService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(Configuration.GetSection("Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                        }
                    });
                });
                app.UseHsts();
            }
            app.UseRouting();
            app.UseCors(_myAllowSpecificOrigins);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}