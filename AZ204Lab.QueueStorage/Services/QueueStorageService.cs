
using Azure.Identity;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace AZ204Lab.QueueStorage.Services
{
    public class QueueStorageService : IQueueStorageService
    {
        private string queueConnectionString = "DefaultEndpointsProtocol=https;AccountName=az204labstorage;AccountKey=P32MM2eB6YK0B9ziCwctRddr29MHgB3DRcGYNHNDuGEujXk7dO5E6bTCL7EO7yXU/TGRLDyI0Qar+AStIcdO6Q==;EndpointSuffix=core.windows.net";

        public async Task SendMessageToQueueAsync(string queueName, string storageName, string message)
        {
            //setup queue client with Manage Identity 
            //var queueUri = new Uri($"https://{storageName}.queue.core.windows.net/{queueName}");
            //QueueClient queueClient = new QueueClient(queueUri, new DefaultAzureCredential());

            QueueClient queueClient = new QueueClient(queueConnectionString, queueName);

            //Create a queue 
            await queueClient.CreateIfNotExistsAsync();

            //Add message to queue
            await queueClient.SendMessageAsync(message + "2nd message");
            await queueClient.SendMessageAsync(message + "3rd message");
            SendReceipt receipt = await queueClient.SendMessageAsync(message + "last message");
        }
        public async Task<QueueMessage[]> GetQueueMessagesAsync(string queueName)
        {
            QueueClient queueClient = new QueueClient(queueConnectionString, queueName);
                    
            QueueMessage[] queueMessages = await queueClient.ReceiveMessagesAsync(maxMessages: 2);
            return queueMessages;
        }


        //Read the message from the queue but will not alter the actaul messages in the queue
        public async Task PeekMessagesAsync(string queueName)
        {
            QueueClient queueClient = new QueueClient(queueConnectionString, queueName);

            // Peek at messages in the queue
            PeekedMessage[] peekedMessages = await queueClient.PeekMessagesAsync(maxMessages: 2);

            foreach (PeekedMessage peekedMessage in peekedMessages)
            {
                // Display the message
                Console.WriteLine($"Message: {peekedMessage.MessageText}");
            }
        }

        
    }
}
