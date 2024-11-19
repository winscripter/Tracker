using System.IO;
using System.Windows;
using System.Xml;
using System.Xml.Linq;
using Tracker.Settings;
using Tracker.Themes;

namespace Tracker.AppMain
{
    internal static class ApplicationInitializer
    {
        public static void Initialize()
        {
            var ss = new SplashScreenWindow();
            ss.Show();

            LoadSettings();
            SetTheme();

            ss.Close();
        }

        private static void AbruptClose(int errorCode = 1)
        {
            Environment.Exit(errorCode);
        }

        private static void SetTheme()
        {
            string activeThemePath = CurrentApp.Settings!.ActiveTheme!.Path!;
            if (!File.Exists(activeThemePath))
            {
                MessageBox.Show(
                    $"Theme file '{activeThemePath}' no longer exists.",
                    "Tracker",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                AbruptClose();
            }

            try
            {
                Theme theme = Theme.LoadFile(activeThemePath);
                ResourceDictionary dict = theme.CreateDictionary();
                App.Current.Resources = dict;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to apply theme.\n\n{ex.Message}",
                    "Tracker",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                AbruptClose();
            }
        }

        private static void LoadSettings()
        {
            if (!File.Exists(FileConstants.SettingsFile))
            {
                MessageBox.Show(
                    $"The settings file, {FileConstants.SettingsFile}, is missing. Application will exit now.",
                    "Tracker",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                AbruptClose();
            }

            try
            {
                string settingsContents = File.ReadAllText(FileConstants.SettingsFile);
                XDocument document = XDocument.Parse(settingsContents);
                SettingsModel model = SettingsReader.Parse(document);
                CurrentApp.Settings = model;
            }
            catch (XmlException)
            {
                MessageBox.Show(
                    $"The settings file, {FileConstants.SettingsFile}, contains an XML error.",
                    "Tracker",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                AbruptClose();
            }
            catch (IOException)
            {
                MessageBox.Show(
                    $"The settings file, {FileConstants.SettingsFile}, could not be loaded.",
                    "Tracker",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                AbruptClose();
            }
            catch (SettingsException ex)
            {
                MessageBox.Show(
                    $"The settings file, {FileConstants.SettingsFile}, is not a valid settings file.\n\n{ex.Message}",
                    "Tracker",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                AbruptClose();
            }
            catch (Exception ex)
            {
                string displayExceptionType = ex.Source?.GetType()?.FullName ?? "<unknown>";
                MessageBox.Show(
                    $"While attempting to read the settings file {FileConstants.SettingsFile}, an exception of type '{displayExceptionType}' occurred.",
                    "Tracker",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                AbruptClose();
            }
        }
    }
}
