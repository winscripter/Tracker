using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tracker.Themes;
using Tracker.ViewModels;
using Tracker.Workspace;

namespace Tracker.Explorer
{
    public partial class ExplorerControl : UserControl
    {
        public ICommand NewCommand { get; set; }
        public ICommand OpenCommand { get; set; }
        public ICommand SaveAsCommand { get; set; }

        private FileListControl? _fileList = null;

        public ExplorerControl()
        {
            NewCommand = new RelayCommand(
                e => NewCommandCore(),
                e => true
            );

            OpenCommand = new RelayCommand(
                e => OpenCommandCore(),
                e => true
            );

            SaveAsCommand = new RelayCommand(
                e => SaveAsCommandCore(),
                e => true
            );

            InitializeComponent();
            _contentPresenter.Content = new NoProjectControl();
            DataContext = this;

            try
            {
                var menuItem = new MenuItem()
                {
                    Header = "Theme"
                };
                Theme.Assign(menuItem);

                menu.Items.Add(menuItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to display list of themes!\n\n{ex}",
                    "Tracker",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }

        private void NewCommandCore()
        {
            var fl = new FileListControl
            {
                ObjectFile = ObjectFile.Sample
            };
            fl.Awaken();

            _fileList = fl;
            _contentPresenter.Content = fl;
        }

        private void OpenCommandCore()
        {
            var ofd = new OpenFileDialog()
            {
                Title = "Open tracker file",
                Multiselect = false,
                Filter = "Tracker files|*.tracker|All files|*.*"
            };

            if (ofd.ShowDialog() == true)
            {
                string fileName = ofd.FileName;
                byte[] contents = File.ReadAllBytes(fileName);
                
                using var stream = new MemoryStream(contents);

                _fileList = new FileListControl
                {
                    ObjectFile = ObjectFile.Load(stream)
                };
                _fileList.Awaken();

                _contentPresenter.Content = _fileList;
            }
        }

        private void SaveAsCommandCore()
        {
            if (_fileList is null)
            {
                MessageBox.Show(
                    "No tracker file is selected",
                    "Tracker",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                return;
            }

            var saveAsDialog = new SaveFileDialog()
            {
                Title = "Save tracker file",
                Filter = "Tracker files|*.tracker|All files|*.*"
            };

            if (saveAsDialog.ShowDialog() == true)
            {
                string fileName = saveAsDialog.FileName;
                try
                {
                    byte[] result = _fileList.ObjectFile!.Export();
                    File.WriteAllBytes(fileName, result);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.ToString(),
                        "Tracker",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                    return;
                }
            }
        }
    }
}
