using System.Windows.Controls;

namespace Tracker.Explorer
{
    public partial class TrackingTargetTableControl : UserControl
    {
        public TrackingTargetTableControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string DisplayMemberName { get; set; } = string.Empty;
    }
}
