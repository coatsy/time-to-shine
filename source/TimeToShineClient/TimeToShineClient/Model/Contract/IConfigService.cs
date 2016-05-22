namespace TimeToShineClient.Model.Contract
{
    public interface IConfigService
    {
        string ServiceBase { get; set; }
        string MqttBroker { get; set; }
        string MqttTopic { get; set; }
    }
}