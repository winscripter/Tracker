using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tracker.Explorer
{
    /// <summary>
    /// Interaction logic for TrackingTargetMemberControl.xaml
    /// </summary>
    public partial class TrackingTargetMemberControl : UserControl
    {
        public TrackingTargetMemberControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        public SolidColorBrush EllipseFill { get; set; } = Brushes.Transparent;
        public string DisplayName { get; set; } = "???";
        public string Done { get; set; } = "0%";

        public void UpdateDone()
        {
            var lgb = new LinearGradientBrush()
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 0)
            };

            lgb.GradientStops.Add(new GradientStop()
            {
                Color = EllipseFill.Color,
                Offset = 0D
            });

            int percentage = int.Parse(Done.TrimEnd('%'));
            double offset = percentage / 100D;

            lgb.GradientStops.Add(new GradientStop()
            {
                Color = EllipseFill.Color,
                Offset = offset
            });

            // We fill the rest with transparency...
            lgb.GradientStops.Add(new GradientStop()
            {
                Color = Brushes.Transparent.Color,
                Offset = offset
            });

            lgb.GradientStops.Add(new GradientStop()
            {
                Color = Brushes.Transparent.Color,
                Offset = 1D
            });

            progressRect.Fill = lgb;
        }
    }
}
