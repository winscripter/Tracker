using ICSharpCode.AvalonEdit.Highlighting;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Tracker.AvalonEditStyle;

namespace Tracker.Explorer
{
    public partial class TextFileManager : Window
    {
        private static readonly IAvalonEditStyleProvider s_darkMode = new AvalonEditDarkMode();
        private static readonly IAvalonEditStyleProvider s_lightMode = new AvalonEditLightMode();

        private bool isWordWrapActive = false;
        private bool isDarkThemeActive = false;

        public Action<string>? SaveCallback { get; set; }

        public TextFileManager()
        {
            InitializeComponent();
        }

        private static SolidColorBrush GetMatchingColor(bool boolean) => boolean ? Brushes.Green : Brushes.Transparent;

        private void OnWordWrapMouseDown(object? sender, MouseEventArgs e)
        {
            isWordWrapActive = !isWordWrapActive;
            IMAGE_CONTAINER_WordWrap.Background = GetMatchingColor(isWordWrapActive);

            textEditor.WordWrap = isWordWrapActive;
        }

        public bool IsCSharp
        {
            get
            {
                ComboBoxItem? item = cbxLanguage.SelectedItem as ComboBoxItem;
                if (item is not null)
                {
                    return item.Content.ToString() == "C#";
                }
                return false;
            }
        }

        private void OnDarkThemeMouseDown(object? sender, MouseEventArgs e)
        {
            isDarkThemeActive = !isDarkThemeActive;
            try
            {
                IMAGE_CONTAINER_DarkTheme.Background = GetMatchingColor(isDarkThemeActive);
                IAvalonEditStyleProvider styleProvider = isDarkThemeActive ? s_darkMode : s_lightMode;
                AvalonEditTheming.Apply(textEditor, styleProvider, IsCSharp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Tracker", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool FirstSelection { get; set; } = true;

        private void cbxLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FirstSelection)
            {
                FirstSelection = false;
                return;
            }

            ComboBoxItem? item = cbxLanguage.SelectedItem as ComboBoxItem;
            if (item is not null)
            {
                if (item.Content is null)
                {
                    return;
                }

                try
                {
                    textEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition(item.Content.ToString());
                }
                catch
                {
                }
            }
        }

        private void OnSaveChangesAndExit(object? sender, MouseEventArgs e)
        {
            if (SaveCallback is null)
            {
                MessageBox.Show(
                    "Callback is missing",
                    "Tracker",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                return;
            }

            SaveCallback(textEditor.Text);
        }
    }
}
