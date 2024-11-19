namespace Tracker.Object
{
    public sealed class DocumentInfo : ItemEntry
    {
        public static DocumentInfo FromItemEntry(ItemEntry item)
        {
            return new DocumentInfo
            {
                Created = item.Created,
                Updated = item.Updated,
                Description = item.Description
            };
        }
    }
}
