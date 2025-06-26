namespace Business.Localization
{
    public interface IMessageService
    {
        string this[string key] { get; }
    }
}
