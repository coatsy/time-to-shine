using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeToShineClient.Model.Contract;
using XamlingCore.Portable.Contract.Config;

namespace TimeToShineClient.Model.Service
{
    public class ConfigService : IConfigService
    {
        private readonly IConfigRepo _config;

        const string SERVCICE_BASE = "SERVCICE_BASE";
        const string MQTT_BROKER = "MQTT_BROKER";
        const string MQTT_TOPIC = "MQTT_TOPIC";
        const string LIGHTS = "LIGHTS";
        const string DMXCHANNEL = "DMXCHANNEL";

        public ConfigService(IConfigRepo config)
        {
            _config = config;
        }

        public string LightIds
        {
            get { return _config[LIGHTS]; }
            set { _config.Write(LIGHTS, value); }
        }

        public int[] LightIdArray
        {
            get
            {
                var lights = LightIds;
                return lights == null ? new int[0] : lights.Split(',').Select(l => Convert.ToInt32(l)).ToArray();
            }
           
        }

        public string ServiceBase
        {
            get { return _config[SERVCICE_BASE]; }
            set { _config.Write(SERVCICE_BASE, value); }
        }

        public string DMXChannel
        {
            get { return _config[DMXCHANNEL]; }
            set { _config.Write(DMXCHANNEL, value); }
        }

        public string MqttBroker
        {
            get { return _config[MQTT_BROKER] == null ? _config[MQTT_BROKER] : "10.10.11.161"; }
            set { _config.Write(MQTT_BROKER, value); }
        }

        public string MqttTopic
        {
            get { return _config[MQTT_TOPIC] == null ? _config[MQTT_TOPIC] : "msstore/vivid/light/"; }
            set { _config.Write(MQTT_TOPIC, value); }
        }
    }


}
