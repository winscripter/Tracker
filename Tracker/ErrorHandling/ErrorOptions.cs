namespace Tracker.ErrorHandling
{
    public sealed class ErrorOptions : IEquatable<ErrorOptions?>
    {
        public string? Message { get; set; }
        public Exception? Exception { get; set; }
        public ErrorKind Kind { get; set; }

        public ErrorOptions(string? message, Exception? exception, ErrorKind kind)
        {
            Message = message;
            Exception = exception;
            Kind = kind;
        }

        public ErrorOptions(string? message, ErrorKind kind) : this(message, null, kind)
        {
        }

        public ErrorOptions(Exception? ex, ErrorKind kind) : this(null, ex, kind)
        {
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as ErrorOptions);
        }

        public bool Equals(ErrorOptions? other)
        {
            return other is not null &&
                   Message == other.Message &&
                   EqualityComparer<Exception?>.Default.Equals(Exception, other.Exception) &&
                   Kind == other.Kind;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Message, Exception, Kind);
        }

        public static bool operator ==(ErrorOptions? left, ErrorOptions? right)
        {
            return EqualityComparer<ErrorOptions>.Default.Equals(left, right);
        }

        public static bool operator !=(ErrorOptions? left, ErrorOptions? right)
        {
            return !(left == right);
        }
    }
}
