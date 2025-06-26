using Microsoft.Extensions.Localization;
using System.Reflection;

namespace Business.Localization
{
    public class MessageService : IMessageService
    {
        private readonly IStringLocalizer _localizer;

        public MessageService(IStringLocalizerFactory factory)
        {
            var type = typeof(MessageService);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("Messages", assemblyName.Name);
        }

        public string this[string key] => _localizer[key];

        public string this[string name, params object[] arguments] => _localizer[name, arguments];
    }
}
