using System.Threading.Tasks;
using TimeToShineClient.Model.Entity;

namespace TimeToShineClient.Model.Contract
{
    public interface IMQTTService
    {
        Task Publish(Colour colour);
    }
}