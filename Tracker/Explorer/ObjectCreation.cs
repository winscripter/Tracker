using Tracker.Object;
using Tracker.Workspace;

namespace Tracker.Explorer
{
    internal static class ObjectCreation
    {
        public static ObjectFile? CreateNewObject()
        {
            var documentProperties = new SetDocumentPropertiesDialog();
            documentProperties.ShowDialog();
            if (documentProperties.Result is not null)
            {
                ItemEntry itemEntry = new()
                {
                    Created = (ulong)DateTime.Now.TimeOfDay.Ticks,
                    Updated = (ulong)DateTime.Now.TimeOfDay.Ticks,
                    Description = documentProperties.Result.Description
                };

                DocumentInfo documentInfo = DocumentInfo.FromItemEntry(itemEntry);

                ItemEntry baseFolderRootEntry = new()
                {
                    Created = (ulong)DateTime.Now.TimeOfDay.Ticks,
                    Updated = (ulong)DateTime.Now.TimeOfDay.Ticks,
                    Description = "This is the topmost folder."
                };

                DirectoryEntry entry = new()
                {
                    AbsoluteName = "/",
                    Created = baseFolderRootEntry.Created,
                    Description = baseFolderRootEntry.Description,
                    Directories = [],
                    Files = [],
                    Updated = baseFolderRootEntry.Updated
                };

                return ObjectFile.FromModel(new ObjectModel(documentInfo, entry));
            }
            return null;
        }
    }
}
