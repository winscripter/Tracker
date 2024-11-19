namespace Tracker.Object
{
    public sealed class DirectoryEntry : ItemEntry
    {
        public string? AbsoluteName { get; set; }
        public List<DirectoryEntry> Directories { get; set; } = [];
        public List<FileEntry> Files { get; set; } = [];
    }
}
