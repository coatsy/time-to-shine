using System;
using TimeToShineClient.Model.Contract;
using TimeToShineClient.Model.Entity;
using uPLibrary.Networking.M2Mqtt;

namespace TimeToShineClient.Model.Repo
{
    public class MQTTService : IMQTTService
    {
        const string MqttBroker = "27.33.31.102";
        const string MqttTopic = "msstore/vivid/light/";

        MqttClient client;

        public MQTTService()
        {
            client = new MqttClient(MqttBroker);
            client.Connect(Guid.NewGuid().ToString().Substring(0, 20));
        }

        public void Publish(Colour colour)
        {
            client.Publish($"{MqttTopic}{colour.LightId}", colour.ToJson());
        }
    }
}
