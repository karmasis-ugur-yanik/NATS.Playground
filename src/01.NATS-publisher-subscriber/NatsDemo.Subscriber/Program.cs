using NatsDemo.Common;
using NatsDemo.Common.Models;

using System.Text.Json;
using System.Threading;

internal class Program
{
    private static void Main(string[] args)
    {
        // Create a CancellationTokenSource for token management
        var cts = new CancellationTokenSource();
        var token = cts.Token;

        var connection = NatsConnection.GetConnection();
        Console.WriteLine("Subscriber connected to NATS");

        var subscription = connection.SubscribeAsync("test.message");
        subscription.MessageHandler += (sender, args) =>
        {
            var jsonMessage = System.Text.Encoding.UTF8.GetString(args.Message.Data);
            var message = JsonSerializer.Deserialize<Message>(jsonMessage);
            Console.WriteLine($"Received message: {message?.Content}");
        };

        subscription.Start();

        Console.WriteLine("Listening for messages. Press CTRL+C to exit.");

        try
        {
            token.WaitHandle.WaitOne();
        }
        catch (OperationCanceledException)
        {            
            Console.WriteLine("Subscriber has been canceled.");
        }
        finally
        {
            // Unsubscribe and close the connection
            subscription.Unsubscribe();
            connection.Close();
        }
    }
}
