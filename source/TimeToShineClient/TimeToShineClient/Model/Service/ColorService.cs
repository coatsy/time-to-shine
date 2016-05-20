using Windows.UI;
using TimeToShineClient.Model.Contract;
using TimeToShineClient.Model.Entity;

namespace TimeToShineClient.Model.Service
{
    public class ColorService : IColorService
    {
        private readonly IMQTTService _mqttService;

        public ColorService(IMQTTService mqttService)
        {
            _mqttService = mqttService;
        }

        public void PublishSampleColor(Color c)
        {
            var colour = Colour.FromColor(c);
            colour.LightId = 0;
            _mqttService.Publish(colour);
        }
    }
}
