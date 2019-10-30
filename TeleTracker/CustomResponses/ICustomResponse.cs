namespace TeleTracker.CustomResponses
{
    public interface ICustomResponse
    {
        string Message { get; }

        string UserID { get; }

        string EntityID { get; }
    }
}
