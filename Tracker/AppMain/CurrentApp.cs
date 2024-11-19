using Tracker.Settings;

namespace Tracker.AppMain
{
    internal static class CurrentApp
    {
        public static SettingsModel Settings { get; set; } = new();

        public static event EventHandler? ThemeChanged;

        internal static void DoInvokeThemeChanged()
        {
            ThemeChanged?.Invoke(null, EventArgs.Empty);
        }
    }
}
