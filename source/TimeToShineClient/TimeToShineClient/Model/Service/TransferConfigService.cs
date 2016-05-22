using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using XamlingCore.Portable.Contract.Config;
using XamlingCore.Portable.Contract.Downloaders;
using XamlingCore.Portable.Contract.Network;
using XamlingCore.Portable.Messages.Network;
using XamlingCore.Portable.Messages.XamlingMessenger;
using XamlingCore.Portable.Net.DownloadConfig;
using XamlingCore.Portable.Net.Service;

namespace TimeToShineClient.Model.Service
{
    public class TransferConfigService : HttpTransferConfigServiceBase
    {
        private readonly IConfig _config;
        
        private readonly IDeviceNetworkStatus _deviceNetworkStatus;

        public TransferConfigService(IConfig config, IDeviceNetworkStatus deviceNetworkStatus)
        {
            _config = config;
           
            _deviceNetworkStatus = deviceNetworkStatus;
        }

        public override async Task<IHttpTransferConfig> GetConfig(string url, string verb)
        {
            var finalUrl = url;

            if (!finalUrl.StartsWith("http"))
            {
                finalUrl = _getBaseUrl() + finalUrl;
            }

            var config = new StandardHttpConfig
            {
                Accept = "application/json",
                IsValid = true,
                Url = finalUrl,
                BaseUrl = finalUrl,
                Verb = verb,
                Headers = new Dictionary<string, string>()
            };

            return config;
        }

        
        string _getBaseUrl()
        {
            return "http://192.168.0.11/testingq1232";
        }
    }
}
