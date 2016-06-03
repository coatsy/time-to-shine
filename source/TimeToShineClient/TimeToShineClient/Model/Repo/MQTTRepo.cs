using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using TimeToShineClient.Model.Contract;
using TimeToShineClient.Model.Entity;
using TimeToShineClient.Model.Messages;
using uPLibrary.Networking.M2Mqtt;
using XamlingCore.Portable.Messages.XamlingMessenger;
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
        Timer connectionTimer;
        bool checkingConnection = false;

        XAsyncLock _taskLock = new XAsyncLock();

        public MQTTService(IConfigService configService)
        {
            _configService = configService;

            connectionTimer = new Timer(new TimerCallback(_queueManagement), null, 500, 2000);
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


        void _queueManagement(object state)
        {
            if (checkingConnection) { return; }
            checkingConnection = true;

            _config();

            if (client != null && client.IsConnected)
            {
                new DebugMessage("Broker test - connected.").Send();
                checkingConnection = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(_configService.MqttBroker))
            {
                new DebugMessage("Broker test - no broker set").Send();
                checkingConnection = false;
                return;
            }

            try
            {
                if (client == null)
                {
                    new DebugMessage($"Creating new client from null - broker {_configService.MqttBroker}").Send();
                    client = new MqttClient(_configService.MqttBroker);
                }

                new DebugMessage("Connecting to client").Send();

                client.Connect(Guid.NewGuid().ToString().Substring(0, 20));
            }
            catch (Exception ex)
            {
                new DebugMessage($"Failed connection to client: exception: {ex.Message}").Send();
            }
            finally
            {
                checkingConnection = false;
            }

            checkingConnection = false;

        }

        private bool _isConnected => client != null && client.IsConnected;


        public async Task Publish(Colour colour)
        {
            if (!_isConnected)
            {
                new DebugMessage("Could not publish, client not connected").Send();
                return;
            }

            try
            {
                var json = colour.ToJson();
                new DebugMessage($"Sending: Topic: {_mqttTopic}, dmx: {_dmxChannel}, message: {json}").Send();

                var result = client.Publish($"{_mqttTopic}{_dmxChannel}", json);

                new DebugMessage($"Send result: {result}").Send();

            }
            catch (Exception ex)
            {
                new DebugMessage($"Failed to send to client {ex.Message}").Send();
            }
        }
    }
}