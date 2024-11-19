using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Tracker.Explorer
{
    /// <summary>
    /// Interaction logic for FileControl.xaml
    /// </summary>
    public partial class FileControl : UserControl
    {
        public string SourceFolder { get; set; } = "???";

        public event EventHandler? Open;
        public event EventHandler? Delete;

        public FileControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string FileName { get; set; } = "(Unknown)";
        public string TypeAsString { get; set; } = "(Unknown)";

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            SolidColorBrush brush = (SolidColorBrush)App.Current.FindResource("FileObjectHoverBG");
            bdr.Background = brush;
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            SolidColorBrush brush = (SolidColorBrush)App.Current.FindResource("FileObjectBG");
            bdr.Background = brush;
        }

        private void CopyNameToClipboard(object sender, RoutedEventArgs e)
        {
            try
            {
                Clipboard.SetText(FileName);
            }
            catch (Exception ex)
            {
                string source = ex.TargetSite?.DeclaringType?.FullName ?? "???";
                MessageBox.Show(
                    $"Could not copy text to clipboard.\n\n{source}: {ex.Message}",
                    "Tracker",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }

        private void bdr_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Open?.Invoke(this, EventArgs.Empty);
        }

        // open
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Open?.Invoke(this, EventArgs.Empty);
        }

        // delete
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Delete?.Invoke(this, EventArgs.Empty);
        }
    }
}
