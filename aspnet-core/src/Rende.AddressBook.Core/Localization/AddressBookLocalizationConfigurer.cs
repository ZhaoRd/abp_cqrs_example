using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace Rende.AddressBook.Localization
{
    public static class AddressBookLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(AddressBookConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(AddressBookLocalizationConfigurer).GetAssembly(),
                        "Rende.AddressBook.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
