using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Packets;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace MQTTSubscriber
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            File.WriteAllText("log.txt", string.Empty);
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
                var log = "Receiver: Connection Established";
                Console.WriteLine(log);
                File.AppendAllText("log.txt", log + "\n");
            };

            new_client_receiver.DisconnectedAsync += async p => {
                var log = "Receiver: Connection Lost";
                Console.WriteLine(log);
                File.AppendAllText("log.txt", log + "\n");
            };

            new_client_receiver.ApplicationMessageReceivedAsync += async e => {
                var log = $"[{DateTime.UtcNow:HH:mm:ss}] Received - Topic: {e.ApplicationMessage.Topic}, Payload: {e.ApplicationMessage.ConvertPayloadToString()}";
                Console.WriteLine(log);
                File.AppendAllText("log.txt", log + "\n");
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
                var log = "Subscribed to topics: test/+";
                Console.WriteLine(log);
                File.AppendAllText("log.txt", log + "\n");
                log = "Press any key to exit...";
                Console.WriteLine(log);
                File.AppendAllText("log.txt", log + "\n");

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                var log = $"Subscriber Error: {ex.Message}";
                Console.WriteLine(log);
                File.AppendAllText("log.txt", log + "\n");
            }
        }
    }
}