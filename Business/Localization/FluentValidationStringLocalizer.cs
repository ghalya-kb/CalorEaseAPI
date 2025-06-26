using Microsoft.Extensions.Localization;
using System.Globalization;

namespace Business.Localization
{
    public class FluentValidationStringLocalizer : IStringLocalizer
    {
        private readonly IStringLocalizer _localizer;

        public FluentValidationStringLocalizer(IStringLocalizer localizer)
        {
            _localizer = localizer;
        }

        public LocalizedString this[string name] => _localizer[name];

        public LocalizedString this[string name, params object[] arguments] => _localizer[name, arguments];

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) => _localizer.GetAllStrings(includeParentCultures);
    }
}
