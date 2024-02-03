using Azure.Storage.Queues.Models;

namespace AZ204Lab.QueueStorage.Services
{
    public interface IQueueStorageService
    {
        Task SendMessageToQueueAsync(string queueName, string storageName, string message);
        Task PeekMessagesAsync(string queueName);
        Task<QueueMessage[]> GetQueueMessagesAsync(string queueName);
    }
}
