﻿using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TimeToShineClient.Model.Contract;
using TimeToShineClient.Model.Entity;
using TimeToShineClient.Model.Messages;
using uPLibrary.Networking.M2Mqtt;
using XamlingCore.Portable.Messages.XamlingMessenger;

namespace TimeToShineClient.Model.Repo
{
    public class MQTTService : IMQTTService
    {
        private readonly IConfigService _configService;
        //const string MqttBroker = "27.33.31.102";
        private string _mqttTopic = "msstore/vivid/light/";
        private string _dmxChannel = "1";
        MqttClient client;
        Colour latestColour = new Colour();
        int sentCount = 0;
        const int publishCycleTime = 200;
        AutoResetEvent publishEvent = new AutoResetEvent(false);


        public MQTTService(IConfigService configService)
        {
            _configService = configService;

            Task.Run(new Action(_publish));
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


        async void _publish()  //run async
        {
            while (true)
            {
                await Task.Delay(publishCycleTime);
                publishEvent.WaitOne();

                if (client == null || !client.IsConnected)
                {
                    try
                    {
                        if (client == null)
                        {
                            new DebugMessage($"Creating new client from null - broker {_configService.MqttBroker}").Send();
                            client = new MqttClient(_configService.MqttBroker);
                        }

                        new DebugMessage("Connecting to broker").Send();

                        client.Connect(Guid.NewGuid().ToString().Substring(0, 20));

                    }
                    catch (Exception ex)
                    {
                        new DebugMessage($"Failed connection to broker: exception: {ex.Message}").Send();
                    }
                }

                if (!_isConnected) {
                    continue;
                }


                try
                {
                    latestColour.MsgId = sentCount++;
                    latestColour.LightId = _configService.LightIdArray;


                    var json = latestColour.ToJson();

                    new DebugMessage($"Sending: Topic: {_mqttTopic}, dmx: {_dmxChannel}, Light Id: {Encoding.ASCII.GetString(json)}").Send();

                    var result = client.Publish($"{_mqttTopic}{_dmxChannel}", json);

                    new DebugMessage($"Send result: {result}").Send();

                }
                catch (Exception ex)
                {
                    new DebugMessage($"Failed to send to client {ex.Message}").Send();
                }
            }
        }

        private bool _isConnected => client != null && client.IsConnected;


        public void Publish(Colour colour)
        {
            if (colour.Red == latestColour.Red && colour.Green == latestColour.Green &&
                colour.Blue == latestColour.Blue && colour.White == latestColour.White) { return; }

            latestColour.Red = colour.Red;
            latestColour.Green = colour.Green;
            latestColour.Blue = colour.Blue;
            latestColour.White = colour.White;

            publishEvent.Set();

            return;
        }
    }
}