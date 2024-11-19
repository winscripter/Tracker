using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Tracker.AppMain;
using Tracker.Object;
using Tracker.Tracking;
using Tracker.Workspace;

namespace Tracker.Explorer
{
    public partial class FileListControl : UserControl
    {
        public ObjectFile? ObjectFile { get; set; }
        public string? Path { get; set; } = "/";

        private const string TrackingTargetDefaultContents = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<trackingTarget>
    <tables>
    </tables>
</trackingTarget>";

        public event EventHandler? OnChange;

        public FileListControl()
        {
            CurrentApp.ThemeChanged += CurrentApp_ThemeChanged;
            InitializeComponent();
        }

        private void CurrentApp_ThemeChanged(object? sender, EventArgs e)
        {
            if (_contentLoaded && ObjectFile is not null)
            {
                Awaken();
            }
        }

        private static string CleanUpName(string name)
        {
            if (name == "/")
            {
                return "/";
            }

            return name.Split('/').Last();
        }

        private static string MakePathPointUpper(string path)
        {
            if (path == "/")
            {
                return "/";
            }

            string result = "/" + string.Join("/", path.Split('/')[..^1]);
            if (result.TakeWhile(c => c == '/').Count() > 1)
            {
                result = "/" + result.TrimStart('/');
            }

            return result;
        }

        public void Awaken()
        {
            if (ObjectFile is null)
            {
                return;
            }

            _stackPanel.Children.Clear();

            var folderContextControl = new TextBlock()
            {
                Text = $"You're in {Path!}",
                Foreground = Brushes.DodgerBlue,
                FontSize = 18D
            };
            _stackPanel.Children.Add(folderContextControl);

            var goUpControl = new DirectoryControl() { DirectoryName = "(Go one folder up)", TypeAsString = "Special folder" };
            goUpControl.Open += (s, e) =>
            {
                string oldPath = Path!;
                try
                {
                    string newPath = MakePathPointUpper(Path!);
                    Path = newPath;
                    Awaken();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Failed to peek whatever exists above.\n\n{ex}",
                        "Tracker",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                    Path = oldPath;
                    Awaken();
                }
            };
            _stackPanel.Children.Add(goUpControl);

            foreach (DirectoryEntry directory in ObjectFile.GetDirectoryEntry(Path!).Directories)
            {
                string name = CleanUpName(directory.AbsoluteName!);
                var directoryControl = new DirectoryControl() { DirectoryName = name, TypeAsString = "File folder" };
                directoryControl.Open += (s, e) =>
                {
                    string oldPath = Path!;
                    if (Path == "/")
                    {
                        Path += name;
                    }
                    else
                    {
                        Path += "/" + name;
                    }

                    try
                    {
                        Awaken();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            $"Failed to peek inside this folder.\n\n{ex}",
                            "Tracker",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                        );
                        Path = oldPath;
                    }
                };
                _stackPanel.Children.Add(directoryControl);
            }

            foreach (FileEntry file in ObjectFile.GetDirectoryEntry(Path!).Files)
            {
                string name = file.Name!;
                string pathAsString = Path!;
                string typeAsString = file.FileType == FileType.Simple ? "File" : file.FileType == FileType.Tracking ? "Tracking target" : "???";
                FileControl fc = new() { FileName = name, TypeAsString = typeAsString };

                if (file.FileType == FileType.Tracking)
                {
                    fc.FileIcon.Fill = Brushes.OrangeRed;
                    fc.FileIcon.Stroke = Brushes.OrangeRed;

                    fc.Open += (s, e) =>
                    {
                        TrackingTargetFile target = TrackingTargetFile.Parse(Encoding.UTF8.GetString(Convert.FromBase64String(file.Base64Contents!)));
                        var explorer = new TrackingTargetExplorer();
                        explorer.preview.File = target;
                        explorer.preview.UpdateTables();
                        explorer.ShowDialog();
                        string result = TrackingTargetFile.WriteXml(explorer.preview.File);
                        string base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(result));
                        ObjectFile.ModifyFileEntry(Path!, file.Name!, base64);
                    };

                    _stackPanel.Children.Add(fc);

                    continue;
                }

                fc.Open += (s, e) =>
                {
                    var dialog = new TextFileManager();
                    dialog.textEditor.Text = Encoding.UTF8.GetString(Convert.FromBase64String(file.Base64Contents!));
                    dialog.SaveCallback = (rawText) =>
                    {
                        string base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(rawText));
                        ObjectFile.ModifyFileEntry(Path!, file.Name!, base64);
                        dialog.Close();
                    };
                    dialog.ShowDialog();
                };

                fc.Delete += (s, e) =>
                {
                    ObjectFile.DeleteFileEntry(pathAsString!, file.Name!);
                    Awaken();
                };

                _stackPanel.Children.Add(fc);
            }
        }

        private static string? AskForName()
        {
            var dialog = new EnterNameDialog();
            dialog.ShowDialog();

            return dialog.NameResult;
        }

        public void AddTrackingTarget(object? source, RoutedEventArgs e)
        {
            string? name = AskForName() + ".trackingtarget";
            if (name is not null)
            {
                if (ObjectFile!.FileExists(Path!, name))
                {
                    MessageBox.Show(
                        $"File {name} already exists at '{Path!}'",
                        "Tracker",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                }
                else
                {
                    ObjectFile!.AddFileEntry(Path!, name, Convert.ToBase64String(Encoding.UTF8.GetBytes(TrackingTargetDefaultContents)), FileType.Tracking);
                    Awaken();
                }
            }
        }

        public void AddFolder(object? source, RoutedEventArgs e)
        {
            string? name = AskForName();
            if (name is not null)
            {
                if (ObjectFile!.DirectoryExists(Path! + "/" + name))
                {
                    MessageBox.Show(
                        $"Directory {name} already exists at '{Path!}'",
                        "Tracker",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                }
                else
                {
                    ObjectFile!.AddDirectoryEntry(Path!, name);
                    Awaken();
                }
            }
        }

        public void AddFile(object? source, RoutedEventArgs e)
        {
            string? name = AskForName();
            if (name is not null)
            {
                if (ObjectFile!.FileExists(Path!, name))
                {
                    MessageBox.Show(
                        $"File {name} already exists at '{Path!}'",
                        "Tracker",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                }
                else
                {
                    ObjectFile!.AddFileEntry(Path!, name, Convert.ToBase64String(Encoding.UTF8.GetBytes("")), FileType.Simple);
                    Awaken();
                }
            }
        }
    }
}
