using Azure.Messaging.ServiceBus;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyApplication
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await SendAsync(123, "Send this message to the Service Bus.");
        }

        public static async Task SendAsync(int id, string messageToSend)
        {
            var connectionString = "Environment.GetEnvironmentVariable("SERVICE_BUS_CONNECTION")";
            var topicName = "name-of-your-topic";

            await using var client = new ServiceBusClient(connectionString);
            ServiceBusSender sender = client.CreateSender(topicName);

            var messageObject = new
            {
                Id = id,
                Message = messageToSend
            };

            string json = JsonSerializer.Serialize(messageObject);
            var message = new ServiceBusMessage(json);

            try
            {
                await sender.SendMessageAsync(message);
                Console.WriteLine("Message sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
            }
        }
    }
}