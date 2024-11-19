namespace Tracker.Workspace
{
    public sealed class ObjectFileException : Exception
    {
        public ObjectFileException()
        {
        }

        public ObjectFileException(string? message) : base(message)
        {
        }

        public ObjectFileException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
