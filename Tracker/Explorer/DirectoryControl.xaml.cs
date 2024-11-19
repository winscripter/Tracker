using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Tracker.Explorer
{
    public partial class DirectoryControl : UserControl
    {
        public event EventHandler? Open;

        public DirectoryControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string DirectoryName { get; set; } = "(Unknown)";
        public string TypeAsString { get; set; } = "(Unknown)";

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            SolidColorBrush brush = (SolidColorBrush)App.Current.FindResource("DirObjectHoverBG");
            bdr.Background = brush;
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            SolidColorBrush brush = (SolidColorBrush)App.Current.FindResource("DirObjectBG");
            bdr.Background = brush;
        }

        private void bdr_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Open?.Invoke(this, EventArgs.Empty);
        }
    }
}
