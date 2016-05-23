using System;
using TimeToShineClient.Model.Contract;
using TimeToShineClient.Model.Entity;
using uPLibrary.Networking.M2Mqtt;

namespace TimeToShineClient.Model.Repo
{
    public class MQTTService : IMQTTService
    {
        //const string MqttBroker = "27.33.31.102";
        private string _mqttTopic = "msstore/vivid/light/";

        MqttClient client;

        public MQTTService(IConfigService configService)
        {
            client = new MqttClient(configService.MqttBroker);

            _mqttTopic = configService.MqttTopic;

            if (_mqttTopic == null)
            {
                configService.MqttTopic = "msstore/vivid/light/";
                _mqttTopic = configService.MqttTopic;
            }
            try
            {
                client.Connect(Guid.NewGuid().ToString().Substring(0, 20));
            }
            catch
            {
                
            }
            
        }

        public void Publish(Colour colour)
        {
            try
            {
                client.Publish($"{_mqttTopic}{colour.LightIds}", colour.ToJson());
            }
            catch
            {
                
            }
            
        }
    }
}
