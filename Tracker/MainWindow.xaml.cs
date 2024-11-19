using System.Windows;
using Tracker.AppMain;

namespace Tracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ApplicationInitializer.Initialize();
            InitializeComponent();
        }
    }
}