using NatsDemo.Common;
using NatsDemo.Common.Models;

using System.Text.Json;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var connection = NatsConnection.GetConnection();
        Console.WriteLine("Publisher connected to NATS");

        try
        {
            // Loop to keep publishing messages
            while (true)
            {
                var message = new Message
                {
                    Content = $"Test message at {DateTime.Now}"
                };

                var jsonMessage = JsonSerializer.Serialize(message);

                // Publish the message to the "test.message" subject
                try
                {
                    connection.Publish("test.message", System.Text.Encoding.UTF8.GetBytes(jsonMessage));
                    Console.WriteLine($"Published message: {message.Content}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error publishing message: {ex.Message}");
                }

                await Task.Delay(1000);  // 1 second delay
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            connection.Close();
            Console.WriteLine("Connection closed.");
        }
    }
}
