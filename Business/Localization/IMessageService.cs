
namespace Business.Localization
{
    public interface IMessageService
    {
        string this[string key] { get; }
        string this[string name, params object[] arguments] { get; }
    }
}
