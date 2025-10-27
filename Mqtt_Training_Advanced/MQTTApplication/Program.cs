using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using System.Text;

namespace MQTTPublisher
{
    internal class Program
    {
        private static Random _random = new Random();

        static async Task Main(string[] args)
        {
            var factory = new MqttFactory();
            var new_client = factory.CreateManagedMqttClient();

            var options = new MqttClientOptionsBuilder()
                .WithClientId("MyHomePublisher")
                .WithWillTopic("localhost/will_topic_receiver")
                .WithWillPayload("Publisher Down")
                .WithTcpServer("localhost", 1883)
                .Build();

            var managedOptions = new ManagedMqttClientOptionsBuilder()
                .WithClientOptions(options)
                .Build();

            new_client.ConnectedAsync += async p => {
                Console.WriteLine("Publisher: Connection Established");
            };

            new_client.DisconnectedAsync += async p => {
                Console.WriteLine("Publisher: Connection Lost");
            };

            try
            {
                await new_client.StartAsync(managedOptions);
                Console.WriteLine($"Publisher started at: {DateTime.UtcNow.TimeOfDay}");

                while (true)
                {
                    
                    double temperature = Math.Round(15 + (_random.NextDouble() * 20), 2);
                    var temperatureMessage = new MqttApplicationMessageBuilder()
                        .WithTopic("test/temp")
                        .WithPayload(Encoding.UTF8.GetBytes(temperature.ToString()))
                        .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                        .Build();

                    await new_client.EnqueueAsync(temperatureMessage);
                    Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss}] Published temperature: {temperature}°C");

                    await Task.Delay(500);

                    var message = new MqttApplicationMessageBuilder()
                        .WithTopic("test/sub1")
                        .WithPayload("Hello")
                        .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                        .Build();

                    await new_client.EnqueueAsync(message);
                    Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss}] Published: Hello");

                    await Task.Delay(500);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Publisher Error: {ex.Message}");
            }
        }
    }
}