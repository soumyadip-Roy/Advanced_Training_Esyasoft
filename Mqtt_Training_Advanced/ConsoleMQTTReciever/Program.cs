using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Packets;
using System.Text;

namespace MQTTSubscriber
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var factory = new MqttFactory();
            var new_client_receiver = factory.CreateManagedMqttClient();

            var options = new MqttClientOptionsBuilder()
                .WithClientId("MyHomeSubscriber")
                .WithTcpServer("localhost", 1883)
                .WithWillTopic("localhost/will_topic_subscriber")
                .WithWillPayload("Reciever Down")
                .WithCleanSession(false)
                .Build();

            var managedOptions = new ManagedMqttClientOptionsBuilder()
                .WithClientOptions(options)
                .Build();

            new_client_receiver.ConnectedAsync += async p => {
                Console.WriteLine("Receiver: Connection Established");
            };

            new_client_receiver.DisconnectedAsync += async p => {
                Console.WriteLine("Receiver: Connection Lost");
            };

            new_client_receiver.ApplicationMessageReceivedAsync += async e => {
                Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss}] Received - Topic: {e.ApplicationMessage.Topic}, Payload: {e.ApplicationMessage.ConvertPayloadToString()}");
            };

            try
            {
                await new_client_receiver.StartAsync(managedOptions);

                List<MqttTopicFilter> all_topic = new List<MqttTopicFilter>
                {
                    new MqttTopicFilterBuilder()
                    .WithTopic("test/+")
                    .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                    .Build()
                };

                await new_client_receiver.SubscribeAsync(all_topic);
                Console.WriteLine("Subscribed to topics: test/+");
                Console.WriteLine("Press any key to exit...");

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Subscriber Error: {ex.Message}");
            }
        }
    }
}