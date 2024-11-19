using System.IO;
using System.Text;
using Tracker.Object;

namespace Tracker.Workspace
{
    public sealed class ObjectFile
    {
        public static readonly ObjectFile Sample = new(
            new ObjectModel(
                new DocumentInfo()
                {
                    Created = (ulong)DateTime.Now.TimeOfDay.Ticks,
                    Updated = (ulong)DateTime.Now.TimeOfDay.Ticks,
                    Description = "My Document"
                },
                new DirectoryEntry()
                {
                    AbsoluteName = "/",
                    Created = (ulong)DateTime.Now.TimeOfDay.Ticks,
                    Updated = (ulong)DateTime.Now.TimeOfDay.Ticks,
                    Description = "My Document",
                    Directories =
                    [
                        new()
                        {
                            Created = (ulong)DateTime.Now.TimeOfDay.Ticks,
                            Updated = (ulong)DateTime.Now.TimeOfDay.Ticks,
                            Description = "My Document",
                            AbsoluteName = "my_folder",
                            Directories = [],
                            Files =
                            [
                                new()
                                {
                                    Created = (ulong)DateTime.Now.TimeOfDay.Ticks,
                                    Updated = (ulong)DateTime.Now.TimeOfDay.Ticks,
                                    Description = "My Document",
                                    Base64Contents = Convert.ToBase64String(Encoding.UTF8.GetBytes("using System;")),
                                    FileType = FileType.Simple,
                                    Name = "Foobar.txt"
                                }
                            ]
                        }
                    ],
                    Files =
                    [
                        new()
                        {
                            Created = (ulong)DateTime.Now.TimeOfDay.Ticks,
                            Updated = (ulong)DateTime.Now.TimeOfDay.Ticks,
                            Description = "My Document",
                            Base64Contents = Convert.ToBase64String(Encoding.UTF8.GetBytes("using System;")),
                            FileType = FileType.Simple,
                            Name = "text_file.txt"
                        }
                    ]
                }
            )
        );

        private readonly ObjectModel _document;

        private ObjectFile(ObjectModel document)
        {
            _document = document;
        }

        public ObjectModel Document => _document;

        public static ObjectFile Load(Stream stream)
        {
            using var br = new BinaryReader(stream, Encoding.UTF8, true);
            return LoadCore(br);
        }

        public byte[] Export()
        {
            using var memoryStream = new MemoryStream();
            using var binaryWriter = new BinaryWriter(memoryStream, Encoding.UTF8);

            WriteDocumentInfo(_document.DocumentInfo);
            WriteDirectoryEntry(_document.RootFolder);

            return memoryStream.ToArray();

            void WriteDocumentInfo(DocumentInfo documentInfo)
            {
                WriteItemEntry(documentInfo);
            }

            void WriteItemEntry(ItemEntry itemEntry)
            {
                binaryWriter.Write(itemEntry.Created);
                binaryWriter.Write(itemEntry.Updated);
                binaryWriter.Write(itemEntry.Description ?? "");
            }

            void WriteFileEntry(FileEntry fileEntry)
            {
                WriteItemEntry(fileEntry);
                binaryWriter.Write(fileEntry.Name ?? "");
                binaryWriter.Write(fileEntry.Base64Contents ?? "");
                binaryWriter.Write((uint)fileEntry.FileType);
            }

            void WriteDirectoryEntry(DirectoryEntry directoryEntry)
            {
                WriteItemEntry(directoryEntry);
                binaryWriter.Write(directoryEntry.AbsoluteName ?? "");
                binaryWriter.Write(directoryEntry.Directories.Count);

                for (int i = 0; i < directoryEntry.Directories.Count; i++)
                    WriteDirectoryEntry(directoryEntry.Directories[i]);

                binaryWriter.Write(directoryEntry.Files.Count);

                for (int i = 0; i < directoryEntry.Files.Count; i++)
                    WriteFileEntry(directoryEntry.Files[i]);
            }
        }

        public static ObjectFile FromModel(ObjectModel model) => new(model);

        private static ObjectFile LoadCore(BinaryReader br)
        {
            return LoadInternal(br);
        }

        private static ObjectFile LoadInternal(BinaryReader br)
        {
            return new(LoadModel(br));
        }

        private static ObjectModel LoadModel(BinaryReader br)
        {
            DocumentInfo documentInfo = ParseDocumentInfo(br);
            DirectoryEntry directory = ParseDirectoryEntry(br);
            return new(documentInfo, directory);
        }

        public DirectoryEntry GetDirectoryEntry(string path)
        {
            if (string.IsNullOrEmpty(path) || path == "/")
            {
                return Document.RootFolder;
            }
            var parts = path.Trim('/').Split('/');
            var currentDirectory = Document.RootFolder;
            
            foreach (var part in parts)
            {
                currentDirectory = currentDirectory.Directories.FirstOrDefault(d => d.AbsoluteName == part);
                if (currentDirectory is null)
                {
                    throw new InvalidOperationException($"Directory '{part}' not found in path '{path}'.");
                }
            }
            return currentDirectory;
        }

        public void DeleteDirectoryEntry(string path)
        {
            if (string.IsNullOrEmpty(path) || path == "/")
            {
                throw new InvalidOperationException("Cannot delete the root directory.");
            }

            var parts = path.Trim('/').Split('/');
            var currentDirectory = Document.RootFolder;
            DirectoryEntry? parentDirectory = null;

            foreach (var part in parts)
            {
                parentDirectory = currentDirectory;
                currentDirectory = currentDirectory.Directories.FirstOrDefault(d => d.AbsoluteName == part);
                if (currentDirectory == null)
                {
                    throw new InvalidOperationException($"Directory '{part}' not found in path '{path}'.");
                }
            }
            parentDirectory?.Directories.Remove(currentDirectory);
        }

        public bool FileExists(string path, string name)
        {
            try
            {
                _ = GetFileEntry(path, name);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public DirectoryEntry AddDirectoryEntry(string path, string newDirectoryName)
        {
            var parentDirectory = GetDirectoryEntry(path);
            var newDirectory = new DirectoryEntry
            {
                AbsoluteName = newDirectoryName
            };
            parentDirectory.Directories.Add(newDirectory);
            return newDirectory;
        }

        public bool DirectoryExists(string path)
        {
            try
            {
                GetDirectoryEntry(path);
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        public FileEntry GetFileEntry(string path, string fileName)
        {
            var directory = GetDirectoryEntry(path);
            var file = directory.Files.FirstOrDefault(f => f.Name == fileName);

            return file is null ? throw new InvalidOperationException($"File '{fileName}' not found in path '{path}'.") : file;
        }

        public void DeleteFileEntry(string path, string fileName)
        {
            var directory = GetDirectoryEntry(path);
            var file = directory.Files.FirstOrDefault(f => f.Name == fileName) ?? throw new InvalidOperationException($"File '{fileName}' not found in path '{path}'.");
            directory.Files.Remove(file);
        }

        public void ModifyFileEntry(string path, string fileName, string newBase64Contents)
        {
            var file = GetFileEntry(path, fileName);
            file.Base64Contents = newBase64Contents;
        }

        private static DirectoryEntry ParseDirectoryEntry(BinaryReader br)
        {
            ItemEntry itemEntry = ParseItemEntry(br);
            string absoluteName = br.ReadString();

            int numberOfFolders = br.ReadInt32();
            var folders = new List<DirectoryEntry>();

            for (int i = 0; i < numberOfFolders; i++)
            {
                folders.Add(ParseDirectoryEntry(br));
            }

            int numberOfFiles = br.ReadInt32();
            var files = new List<FileEntry>();

            for (int i = 0; i < numberOfFiles; i++)
            {
                files.Add(ParseFileEntry(br));
            }

            return new DirectoryEntry()
            {
                AbsoluteName = absoluteName,
                Created = itemEntry.Created,
                Description = itemEntry.Description,
                Directories = folders,
                Files = files,
                Updated = itemEntry.Updated,
            };
        }

        private static FileEntry ParseFileEntry(BinaryReader br)
        {
            ItemEntry itemEntry = ParseItemEntry(br);
            string name = br.ReadString();
            string base64contents = br.ReadString();
            FileType fileType = (FileType)br.ReadUInt32();

            return new()
            {
                Base64Contents = base64contents,
                Created = itemEntry.Created,
                Description = itemEntry.Description,
                FileType = fileType,
                Name = name,
                Updated = itemEntry.Updated
            };
        }

        public FileEntry AddFileEntry(string path, string fileName, string base64Contents, FileType fileType)
        {
            var directory = GetDirectoryEntry(path);
            var newFile = new FileEntry
            {
                Name = fileName,
                Base64Contents = base64Contents,
                FileType = fileType,
                Created = (ulong)DateTime.Now.TimeOfDay.Ticks,
                Updated = (ulong)DateTime.Now.TimeOfDay.Ticks,
                Description = ""
            };
            directory.Files.Add(newFile);
            return newFile;
        }

        private static DocumentInfo ParseDocumentInfo(BinaryReader br)
        {
            return DocumentInfo.FromItemEntry(ParseItemEntry(br));
        }

        private static ItemEntry ParseItemEntry(BinaryReader br)
        {
            ulong created = br.ReadUInt64();
            ulong modified = br.ReadUInt64();
            string description = br.ReadString();

            return new()
            {
                Created = created,
                Updated = modified,
                Description = description
            };
        }
    }
}
