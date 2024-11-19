using System.Xml.Linq;
using Tracker.Settings.Models;

namespace Tracker.Settings
{
    internal static class SettingsReader
    {
        public static SettingsModel Parse(XDocument document)
        {
            XElement rootObject = document.Root ?? throw new SettingsException("Missing root element");

            if (rootObject.Name != "settingsRoot")
                throw new SettingsException("Root tag name must be 'settingsRoot'");

            XElement element = rootObject.Element("activeTheme") ?? throw new SettingsException("Missing active theme");
            ActiveTheme activeTheme = ParseActiveTheme(element);

            return new SettingsModel() { ActiveTheme = activeTheme };
        }

        private static ActiveTheme ParseActiveTheme(XElement rootObject)
        {
            AssertElementName(rootObject, "activeTheme");

            XAttribute attribute = rootObject.Attribute("path") ?? throw new SettingsException("Missing 'path' attribute");
            return new ActiveTheme() { Path = attribute.Value };
        }

        private static void AssertElementName(XElement element, string name)
        {
            if (element.Name != name)
                throw new SettingsException($"Tag name must be '{name}'");
        }
    }
}
