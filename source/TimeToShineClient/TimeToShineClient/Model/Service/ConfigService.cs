﻿using System;
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

        public ConfigService(IConfigRepo config)
        {
            _config = config;
        }

        public string ServiceBase
        {
            get { return _config[SERVCICE_BASE]; }
            set { _config.Write(SERVCICE_BASE, value); }
        }

        public string MqttBroker
        {
            get { return _config[MQTT_BROKER]; }
            set { _config.Write(MQTT_BROKER, value); }
        }

        public string MqttTopic
        {
            get { return _config[MQTT_TOPIC]; }
            set { _config.Write(MQTT_TOPIC, value); }
        }
    }


}
