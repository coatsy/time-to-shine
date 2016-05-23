using Windows.UI;
using TimeToShineClient.Model.Contract;
using TimeToShineClient.Model.Entity;

namespace TimeToShineClient.Model.Service
{
    public class ColorService : IColorService
    {
        private readonly IMQTTService _mqttService;
        private readonly IConfigService _configService;

        public ColorService(IMQTTService mqttService, IConfigService configService)
        {
            _mqttService = mqttService;
            _configService = configService;
        }

        public void PublishSampleColor(Color c)
        {
            var colour = Colour.FromColor(c);
            colour.LightIds = _configService.LightIdArray;
            _mqttService.Publish(colour);
        }
    }
}
