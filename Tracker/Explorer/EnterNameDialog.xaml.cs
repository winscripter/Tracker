using System.Windows;
using System.Windows.Input;

namespace Tracker.Explorer
{
    /// <summary>
    /// Interaction logic for EnterNameDialog.xaml
    /// </summary>
    public partial class EnterNameDialog : Window
    {
        public string? NameResult { get; set; } = null;

        public EnterNameDialog()
        {
            InitializeComponent();
        }

        private void OnContinue(object? sender, MouseEventArgs e)
        {
            string text = nameField.Text;

            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show(
                    "Name cannot be empty or whitespace",
                    "Tracker",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                return;
            }

            if (text.Contains('/') || text.Contains('\\'))
            {
                MessageBox.Show(
                    "Name cannot contain forward slash or back slash characters.",
                    "Tracker",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                return;
            }

            NameResult = text;
            Close();
        }
    }
}
