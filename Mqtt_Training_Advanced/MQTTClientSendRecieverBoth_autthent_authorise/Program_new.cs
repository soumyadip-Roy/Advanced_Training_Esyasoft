using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Packets;
using System.Text;
using System;

namespace MQTTClientSendRecieverBoth_autthent_authorise
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
                .WithClientId("HomeClient_1")
                .WithTcpServer("localhost", 1883)
                .WithWillTopic("localhost/HomeClient_1/will_topic_channel")
                .WithWillPayload("Client Down")
                .WithCredentials(username,password)
                .WithCleanSession(false)
                .Build();

            var managedOptions = new ManagedMqttClientOptionsBuilder()
                .WithClientOptions(options)
                .WithStorage(2)
                .WithAutoReconnectDelay(20)
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
                    if(_isConnected) {
                        
                        //foreach(var item in qDataOffline)
                        //{
                        //    var lost_message = qDataOffline.Dequeue();
                        //    await client_user.EnqueueAsync(lost_message);
                        //    await
                        //}

                        var temperatureMessage = new MqttApplicationMessageBuilder()
                            .WithTopic("test/temp")
                            .WithPayload(Encoding.UTF8.GetBytes(temperature.ToString()))
                            .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                            .Build();

                        await client_user.EnqueueAsync(temperatureMessage);
                        log = $"[{DateTime.UtcNow:HH:mm:ss}] Published temperature: {temperature}°C";
                        Console.WriteLine(log);
                        File.AppendAllText("log.txt", log + "\n");

                        await Task.Delay(500);

                        var message = new MqttApplicationMessageBuilder()
                            .WithTopic("test/sub1")
                            .WithPayload("Hello")
                            .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                            .Build();

                        await client_user.EnqueueAsync(message);
                        log = $"[{DateTime.UtcNow:HH:mm:ss}] Published: Hello";
                        Console.WriteLine(log);
                        File.AppendAllText("log.txt", log + "\n");

                        await Task.Delay(500);
                    }
                    

                    }

                }
                
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
