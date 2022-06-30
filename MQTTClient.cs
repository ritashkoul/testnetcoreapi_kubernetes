//using DockerizedTestAPI.Controllers;
//using Microsoft.Extensions.Logging;
//using MQTTnet;
//using MQTTnet.Client;
//using MQTTnet.Client.Options;
//using System.Linq;
//using System.Text;

//namespace DockerizedTestAPI
//{
//    public class MQTTClient
//    {
//        private readonly ILogger<WeatherForecastController> _logger;

//        private IMqttClient _mqttClient;
//        private IMqttClientOptions _mqttClientOptions;
//        private string topic = "mqttTopic";

//        public MQTTClient(ILogger<WeatherForecastController> logger)
//        {
//            _logger = logger;
//        }

//        public void Connect()
//        {
//            _mqttClientOptions = new MqttClientOptionsBuilder()
//                .WithClientId("MQTTClientAPI")
//                .WithTcpServer("mqtt-service.default.svc.cluster.local", 1883)
//                .WithCredentials("admin", "admin")
//                .WithCleanSession()
//                .Build();

//            _mqttClient = new MqttFactory().CreateMqttClient();

//            _logger.LogInformation("Starting connection with MQTT broker.");
//            _mqttClient.ConnectAsync(_mqttClientOptions).Wait();
//            _logger.LogInformation("Successfully connected to the MQTT broker.");
//        }

//        public void Publish(string payload)
//        {
//            var message = new MqttApplicationMessageBuilder()
//                .WithTopic(topic)
//                .WithPayload(Encoding.UTF8.GetBytes(payload))
//                .Build();

//            _logger.LogInformation($"Publishing message to the {topic}.");
//            _mqttClient.PublishAsync(message).Wait();
//            _logger.LogInformation("Message published.");
//        }

//        public void Subscribe()
//        {
//            _logger.LogInformation($"Subscribing messages from the topic {topic}.");
//            //_mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(topic).Build()).Wait();
//            _mqttClient.UseApplicationMessageReceivedHandler(e =>
//            {
//                var response = _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(topic).Build()).Result;
//                var data = response.Items.FirstOrDefault();
//                if(data != null)
//                {
//                    string message = $"{(int)data.ResultCode} : {data.ResultCode}";
//                    _logger.LogInformation(message);
//                }
//                _logger.LogInformation("### RECEIVED APPLICATION MESSAGE ###");
//                _logger.LogInformation($"Topic = {e.ApplicationMessage.Topic}");
//                _logger.LogInformation($"Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
//            });
//            _logger.LogInformation("Subscribing message completed.");
//        }
//    }
//}
