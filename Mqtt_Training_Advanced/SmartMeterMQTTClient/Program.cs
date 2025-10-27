using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Packets;


namespace SmartMeterMQTTClient
{
    internal class Program
    {
        private static Random _random = new Random();
        static async Task Main(string[] args)
        {
            bool _isConnected = true;
            bool _isQueued = false;


            var username = "user3";
            var password = "1234";

            File.WriteAllText("log.txt", string.Empty);

            Queue<MqttApplicationMessage> qDataOffline = new Queue<MqttApplicationMessage>();

            var factory = new MqttFactory();
            var client_user = factory.CreateManagedMqttClient();

            var options = new MqttClientOptionsBuilder()
                .WithClientId("HomeClient")
                .WithTcpServer("localhost", 1883)
                .WithWillTopic("localhost/localhost/will_topic_channel")
                .WithWillPayload("Client Down")
                .WithCredentials(username, password)
                .WithCleanSession(false)
                .Build();

            var managedOptions = new ManagedMqttClientOptionsBuilder()
                .WithClientOptions(options)
                .Build();

            client_user.ConnectedAsync += async p => {
                var log = "Client: Connection Established";
                Console.WriteLine(log);
                File.AppendAllText("log.txt", log + "\n");
            };

            client_user.DisconnectedAsync += async p => {
                var log = "Client: Connection Lost";
                _isConnected = false;
                Console.WriteLine(log);
                File.AppendAllText("log.txt", log + "\n");
            };

            client_user.ApplicationMessageReceivedAsync += async e => {
                var log = $"[{DateTime.UtcNow:HH:mm:ss}] Received - Topic: {e.ApplicationMessage.Topic}, Payload: {e.ApplicationMessage.ConvertPayloadToString()}";
                Console.WriteLine(log);
                File.AppendAllText("log.txt", log + "\n");
            };

            client_user.ConnectionStateChangedAsync += async e => {
                var log = $"[{DateTime.UtcNow:HH:mm:ss}] {e.ToString()} ";
                Console.WriteLine(log);
                File.AppendAllText("log.txt", log + "\n");
            };

            try
            {
                await client_user.StartAsync(managedOptions);

                List<MqttTopicFilter> all_topic = new List<MqttTopicFilter>
                {
                    new MqttTopicFilterBuilder()
                    .WithTopic("test/+")
                    .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                    .Build()
                };

                await client_user.SubscribeAsync(all_topic);

                var log = "Subscribed to topics: test/+";
                Console.WriteLine(log);
                File.AppendAllText("log.txt", log + "\n");
                File.AppendAllText("log.txt", log + "\n");


                log = $"Publisher started at: {DateTime.UtcNow.TimeOfDay}";
                Console.WriteLine(log);
                File.AppendAllText("log.txt", log + "\n");

                while (true)
                {
                    double temperature = Math.Round(15 + (_random.NextDouble() * 20), 2);
                    JsonObject json_reading = new JsonObject();
                    double consumption = _random.NextDouble() * 5;
                    json_reading.Add("meter_id", 12345);
                    json_reading.Add("consumer_id", 12345);
                    json_reading.Add("consumption",consumption);
                    json_reading.Add("bill", consumption*2 );

                    if (_isConnected)
                    {
                        var message = new MqttApplicationMessageBuilder()
                            .WithTopic("test/meter")
                            .WithPayload(JsonSerializer.SerializeToUtf8Bytes(json_reading))
                            .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                            .Build();

                        await client_user.EnqueueAsync(message);
                        log = $"[{DateTime.UtcNow:HH:mm:ss}] Published: {json_reading}";
                        Console.WriteLine(log);
                        File.AppendAllText("log.txt", log + "\n");

                        await Task.Delay(500);
                    }
                    else
                    {
                        var temperatureMessage = new MqttApplicationMessageBuilder()
                            .WithTopic("test/temp")
                            .WithPayload(Encoding.UTF8.GetBytes(temperature.ToString()))
                            .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                            .Build();

                        var message = new MqttApplicationMessageBuilder()
                            .WithTopic("test/sub1")
                            .WithPayload("Hello")
                            .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                            .Build();

                        qDataOffline.Enqueue(temperatureMessage);
                        qDataOffline.Enqueue(message);

                        log = "Initial_queue: ";
                        Console.WriteLine(log);

                        foreach (var item in qDataOffline)
                        {
                            log += "\n" + item;
                            Console.WriteLine(item);
                        }



                    }

                }
                //log = "Press any key to exit...";
                //Console.WriteLine(log);
                //Console.ReadLine();
            }
            catch (Exception ex)
            {
                var log = $"Client Error: {ex.Message}";
                Console.WriteLine(log);
                File.AppendAllText("log.txt", log + "\n");
            }



        }

        private static Task Client_user_ConnectionStateChangedAsync(EventArgs arg)
        {
            throw new NotImplementedException();
        }
    }
}
