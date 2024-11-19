namespace Tracker.Tracking.Format
{
    public sealed class TrackingMemberModel
    {
        public string? Name { get; set; } // attribute: name
        public bool Done { get; set; } // attribute: done
        public TrackingValueModel? Value { get; set; } // descendant element with tag name 'value'
    }
}
