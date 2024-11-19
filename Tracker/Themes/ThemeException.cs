namespace Tracker.Themes
{
    public sealed class ThemeException : Exception
    {
        public ThemeException()
        {
        }

        public ThemeException(string? message) : base(message)
        {
        }

        public ThemeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}