using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleTracker.BLL.Interfaces;
using TeleTracker.Core.DTOs;
using TeleTracker.Core.Interfaces;
using TeleTracker.Core.Models;

namespace TeleTracker.BLL.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private static string _endpointUri;
        private static string _primaryKey;
        private static string _databaseId;
        private static CosmosClient _client;
        private Database _database;
        private Container _container;

        public SubscriptionService(ICosmosConfiguration cosmosConfiguration)
        {
            _endpointUri = cosmosConfiguration.EndpointUri;
            _primaryKey = cosmosConfiguration.PrimaryKey;
            _databaseId = cosmosConfiguration.DatabaseId;
            _client = new CosmosClient(_endpointUri, _primaryKey);

        }

        public async Task InitializeService()
        {
            await CreateDatabaseAsync().ConfigureAwait(false);
            await CreateContainerAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<ShowSubscriptionDTO>> GetSubscribedShows(string userId)
        {
            var user = await CheckIfUserExists(userId);
            if (user is null)
                return new List<ShowSubscriptionDTO>();
            return user.ShowSubscriptions;
        }

        public async Task<bool> SubscribeToShow(ShowSubscriptionDTO show, string userId)
        {
            var user = await CheckIfUserExists(userId);
            if (user is null)
            {
                var newUser = new UserSubscription
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userId,
                    ShowSubscriptions = new List<ShowSubscriptionDTO>
                    {
                        show
                    }
                };
                return await CreateUserSubscriptionAsync(newUser);
            }
            user.ShowSubscriptions.Add(show);
            return await UpdateUserSubcriptionAsync(user);
        }

        private async Task<bool> CreateUserSubscriptionAsync(UserSubscription user)
        {
            var isCreated = await _container.CreateItemAsync(user, new PartitionKey(user.UserId));
            return isCreated.StatusCode == System.Net.HttpStatusCode.Created;
        }

        private async Task<bool> UpdateUserSubcriptionAsync(UserSubscription user)
        {
            var isUpdated = await _container.ReplaceItemAsync(user, user.Id);
            return isUpdated.StatusCode == System.Net.HttpStatusCode.OK;
        }

        private async Task<UserSubscription> CheckIfUserExists(string userId)
        {
            var linqQueryable = _container.GetItemLinqQueryable<UserSubscription>();
            var iterator = linqQueryable.Where(u => u.UserId == userId).ToFeedIterator();
            var results = await iterator.ReadNextAsync();
            return results.FirstOrDefault();
        }

        private async Task CreateDatabaseAsync() =>
            _database = await _client.CreateDatabaseIfNotExistsAsync(_databaseId);

        private async Task CreateContainerAsync() =>
            _container = await _database.CreateContainerIfNotExistsAsync("Subscriptions", "/userId");
    }
}
