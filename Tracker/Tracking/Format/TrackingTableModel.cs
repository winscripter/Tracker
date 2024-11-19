namespace Tracker.Tracking.Format
{
    public sealed class TrackingTableModel
    {
        public string? Name { get; set; }
        public string? PrimaryHexColor { get; set; }
        public List<TrackingMemberModel> Members { get; set; } = [];
    }
}
