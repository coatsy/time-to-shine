using System;
using System.Diagnostics;
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
        private readonly IConfigService _configService;
        //const string MqttBroker = "27.33.31.102";
        private string _mqttTopic = "msstore/vivid/light/";
        private string _dmxChannel = "1";
        MqttClient client;

        XAsyncLock _taskLock = new XAsyncLock();

        public MQTTService(IConfigService configService)
        {
            _configService = configService;

            _config();
            _queueManagement();
        }

        void _config()
        {
            var dChannel = _configService.DMXChannel;

            if (string.IsNullOrWhiteSpace(dChannel))
            {
                _dmxChannel = "1";
            }
            else
            {
                _dmxChannel = dChannel;
            }

            _mqttTopic = _configService.MqttTopic;

            if (_mqttTopic == null)
            {
                _configService.MqttTopic = "msstore/vivid/light/";
                _mqttTopic = _configService.MqttTopic;
            }
        }


        async void _queueManagement()
        {
            while (true)
            {
                _config();
                if (client != null && client.IsConnected)
                {
                    await Task.Delay(30000);
                    continue;
                }

                if (string.IsNullOrWhiteSpace(_configService.MqttBroker))
                {
                    await Task.Delay(30000);
                    continue;
                }

                if (client == null)
                {
                    client = new MqttClient(_configService.MqttBroker);
                }

                try
                {
                    client.Connect(Guid.NewGuid().ToString().Substring(0, 20));
                }
                catch
                {
                    
                }

                

                await Task.Delay(30000);
            }
        }

        private bool _isConnected => client != null && client.IsConnected;
        

        public async Task Publish(Colour colour)
        {
            if (!_isConnected)
            {
                return;
            }
            
            try
            {
                var t = DateTime.Now;
                Debug.WriteLine("Start");
                client.Publish($"{_mqttTopic}{_dmxChannel}", colour.ToJson());
                Debug.WriteLine("Stop" + DateTime.Now.Subtract(t).Seconds);
            }
            catch
            {
            }
        }
    }
}