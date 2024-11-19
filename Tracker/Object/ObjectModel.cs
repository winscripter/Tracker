namespace Tracker.Object
{
    public sealed class ObjectModel
    {
        public DocumentInfo DocumentInfo { get; set; }
        public DirectoryEntry RootFolder { get; set; }

        internal ObjectModel(DocumentInfo documentInfo, DirectoryEntry rootFolder)
        {
            DocumentInfo = documentInfo;
            RootFolder = rootFolder;
        }
    }
}
