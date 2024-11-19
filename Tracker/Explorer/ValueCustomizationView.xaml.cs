using System.Windows.Controls;
using System.Windows.Input;

namespace Tracker.Explorer
{
    public partial class ValueCustomizationView : UserControl
    {
        public int Scaling { get; set; } = 10;

        public ValueCustomizationView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string DisplayScale => $"Scale level: {Scaling}";

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Scaling >= 25) return;

            Scaling++;
            vb.Height = (double)Scaling * 4;
            vb.Width = (double)Scaling * 14;
        }

        private void TextBlock_MouseLeftButtonDown2(object sender, MouseButtonEventArgs e)
        {
            if (Scaling <= 5) return;

            Scaling--;
            vb.Height = (double)Scaling * 4;
            vb.Width = (double)Scaling * 14;
        }
    }
}
