namespace Tracker.Tracking
{
    public class TrackingException : Exception
    {
        public TrackingException()
        {
        }

        public TrackingException(string? message) : base(message)
        {
        }

        public TrackingException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
