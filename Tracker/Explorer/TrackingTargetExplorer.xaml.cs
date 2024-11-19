using System.Windows;
using System.Windows.Input;
using Tracker.ViewModels;

namespace Tracker.Explorer
{
    public partial class TrackingTargetExplorer : Window
    {
        public ICommand CtrlR { get; set; }

        public TrackingTargetExplorer()
        {
            CtrlR = new RelayCommand(
                e => CtrlRCore(),
                e => true
            );
            InitializeComponent();
            DataContext = this;
        }

        private void CtrlRCore()
        {
            preview?.UpdateTables();
        }
    }
}
