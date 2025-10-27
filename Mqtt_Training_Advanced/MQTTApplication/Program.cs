using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace MQTTPublisher
{
    internal class Program
    {
        private static Random _random = new Random();

        static async Task Main(string[] args)
        {
            var username = "user3";
            var password = "1234";
            File.WriteAllText("log.txt", string.Empty);
            var factory = new MqttFactory();
            var new_client = factory.CreateManagedMqttClient();

            var options = new MqttClientOptionsBuilder()
                .WithClientId("MyHomePublisher")
                .WithWillTopic("localhost/will_topic_receiver")
                .WithWillPayload("Publisher Down")
                .WithCredentials(username,password)
                .WithTcpServer("localhost", 1883)
                .Build();

            var managedOptions = new ManagedMqttClientOptionsBuilder()
                .WithClientOptions(options)
                .Build();

            new_client.ConnectedAsync += async p => {
                var log = "Receiver: Connection Established";
                Console.WriteLine(log);
                File.AppendAllText("log.txt", log+"\n");
            };

            new_client.DisconnectedAsync += async p => {
                var log = "Receiver: Connection Lost";
                Console.WriteLine(log);
                File.AppendAllText("log.txt", log + "\n");
            };

            try
            {
                await new_client.StartAsync(managedOptions);
                var log = $"Publisher started at: {DateTime.UtcNow.TimeOfDay}";
                Console.WriteLine(log);
                File.AppendAllText("log.txt", log + "\n");

                while (true)
                {
                    
                    double temperature = Math.Round(15 + (_random.NextDouble() * 20), 2);
                    var temperatureMessage = new MqttApplicationMessageBuilder()
                        .WithTopic("test/temp")
                        .WithPayload(Encoding.UTF8.GetBytes(temperature.ToString()))
                        .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                        .Build();

                    await new_client.EnqueueAsync(temperatureMessage);
                    log = $"[{DateTime.UtcNow:HH:mm:ss}] Published temperature: {temperature}°C";
                    Console.WriteLine(log);
                    File.AppendAllText("log.txt", log + "\n");

                    await Task.Delay(500);

                    var message = new MqttApplicationMessageBuilder()
                        .WithTopic("test/sub1")
                        .WithPayload("Hello")
                        .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                        .Build();

                    await new_client.EnqueueAsync(message);
                    log = $"[{DateTime.UtcNow:HH:mm:ss}] Published: Hello";
                    Console.WriteLine(log);
                    File.AppendAllText("log.txt", log + "\n");

                    await Task.Delay(500);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                var log = $"Publisher Error: {ex.Message}";
                Console.WriteLine(log);
                File.AppendAllText("log.txt", log + "\n");
            }
        }
    }
}