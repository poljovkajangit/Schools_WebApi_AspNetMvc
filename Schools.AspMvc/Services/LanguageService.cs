using Microsoft.Extensions.Localization;
using System.Reflection;

namespace Schools.MVC.Services
{
    /// <summary>
    /// dummy class to group shared resources
    /// </summary>
    public class SharedResources
    {

    }

    public class LanguageService
    {
        private readonly IStringLocalizer _localizer;
        public LanguageService(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResources);
            var assembly = type.GetTypeInfo().Assembly;
            var assemblyName = new AssemblyName(assembly.FullName ?? throw new Exception("assembly.FullName is null"));
            _localizer = factory.Create("SharedResource", assemblyName.Name ?? throw new Exception("assembly.FullName is null"));
        }

        public LocalizedString GetKey(string key)
        {
            return _localizer[key];
        }

    }
}
