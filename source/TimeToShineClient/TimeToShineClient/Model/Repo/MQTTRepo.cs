using System;
using System.Threading.Tasks;
using TimeToShineClient.Model.Contract;
using TimeToShineClient.Model.Entity;
using uPLibrary.Networking.M2Mqtt;
using XamlingCore.Portable.Util.Lock;
using XamlingCore.Portable.Util.TaskUtils;

namespace TimeToShineClient.Model.Repo
{
    public class MQTTService : IMQTTService
    {
        //const string MqttBroker = "27.33.31.102";
        private string _mqttTopic = "msstore/vivid/light/";
        private string _dmxChannel = "1";
        MqttClient client;

        XAsyncLock _taskLock = new XAsyncLock();

        public MQTTService(IConfigService configService)
        {
            if (string.IsNullOrWhiteSpace(configService.MqttBroker))
            {
                return;
            }

            var dChannel = configService.DMXChannel;

            if (string.IsNullOrWhiteSpace(dChannel))
            {
                _dmxChannel = "1";
            }
            else
            {
                _dmxChannel = dChannel;
            }

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

        async Task _lockConnect()
        {
            using (var l = await _taskLock.LockAsync())
            {
                if (client.IsConnected)
                {
                    return;
                }
                try
                {
                    client.Connect(Guid.NewGuid().ToString().Substring(0, 20));
                }
                catch
                {
                }

            }
            
        }

        public async Task Publish(Colour colour)
        {
            if (!client.IsConnected)
            {
                await _lockConnect();

            }

            try
            {
                client.Publish($"{_mqttTopic}{_dmxChannel}", colour.ToJson());
            }
            catch
            {
                
            }
            
        }
    }
}
