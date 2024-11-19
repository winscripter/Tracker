using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tracker.Explorer
{
    public partial class ColorCustomizationView : UserControl
    {
        public ColorCustomizationView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public SolidColorBrush PreviewFill { get; set; } = Brushes.Transparent;

        public static bool IsValidHex(string hex)
        {
            if (hex.Length == 0) return false;
            if (hex.Length != 7) return false;

            if (hex[0] != '#') return false;
            string afterHashtag = hex[1..];

            return afterHashtag.All(c => (c is >= '0' and <= '9') || (c is >= 'a' and <= 'f') || (c is >= 'A' and <= 'F'));
        }

        private void hexValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            hexValue.BorderThickness = new Thickness(1);
            if (IsValidHex(hexValue.Text))
            {
                hexValue.BorderBrush = Brushes.LightGray;
                PreviewFill = (SolidColorBrush?)Cache.BrushConverter.ConvertFrom(hexValue.Text) ?? Brushes.Transparent;
            }
            else
            {
                hexValue.BorderBrush = Brushes.Red;
            }
            e.Handled = false;
        }
    }
}
