namespace Tracker.Object
{
    public sealed class FileEntry : ItemEntry
    {
        public string? Name { get; set; }
        public string? Base64Contents { get; set; }
        public FileType FileType { get; set; }
    }
}
