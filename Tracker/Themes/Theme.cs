using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using Tracker.AppMain;
using Tracker.ViewModels;

namespace Tracker.Themes
{
    internal class Theme
    {
        private static readonly Uri s_mainWpfResourceDictionaryUri = new("pack://application:,,,/Tracker;component/Themes/WPF.xaml");

        private const string ThemesDirectory = "UI Themes";

        public IThemeMetrics Metrics { get; set; }
        public IThemeModel Model { get; set; }

        private Theme(IThemeMetrics metrics, IThemeModel model)
        {
            Metrics = metrics;
            Model = model;
        }

        public ResourceDictionary CreateDictionary()
        {
            var main = new ResourceDictionary
            {
                { "CommonPaneBG", Model.CommonPaneBG },
                { "CommonPaneFG", Model.CommonPaneFG },

                { "DefaultBG", Model.DefaultBG },
                { "DefaultFG", Model.DefaultFG },

                { "DirObjectBG", Model.DirObjectBG },
                { "DirObjectFG", Model.DirObjectFG },
                { "DirObjectHoverBG", Model.DirObjectHoverBG },
                { "DirObjectHoverFG", Model.DirObjectHoverFG },

                { "FileObjectBG", Model.FileObjectBG },
                { "FileObjectFG", Model.FileObjectFG },
                { "FileObjectHoverBG", Model.FileObjectHoverBG },
                { "FileObjectHoverFG", Model.FileObjectHoverFG },

                { "HyperlinkBG", Model.HyperlinkBG },
                { "HyperlinkFG", Model.HyperlinkFG },
                { "HyperlinkHoverBG", Model.HyperlinkHoverBG },
                { "HyperlinkHoverFG", Model.HyperlinkHoverFG },
                { "HyperlinkActiveBG", Model.HyperlinkActiveBG },
                { "HyperlinkActiveFG", Model.HyperlinkActiveFG },

                { "ParagraphBG", Model.ParagraphBG },
                { "ParagraphFG", Model.ParagraphFG },
                { "ParagraphHoverBG", Model.ParagraphHoverBG },
                { "ParagraphHoverFG", Model.ParagraphHoverFG },
                { "ParagraphActiveBG", Model.ParagraphActiveBG },
                { "ParagraphActiveFG", Model.ParagraphActiveFG },
            };
            var dictWpf = new ResourceDictionary { Source = s_mainWpfResourceDictionaryUri };
            var resourceDictionary = new ResourceDictionary();
            resourceDictionary.MergedDictionaries.Add(dictWpf);
            resourceDictionary.MergedDictionaries.Add(main);
            return resourceDictionary;
        }

        public static Theme LoadDocument(XDocument document)
        {
            XElement root = document.Root ?? throw new InvalidOperationException("Missing root tag");

            if (root.Name != "theme")
                throw new ThemeException($"Root tag must be 'theme'; it is {root.Name}");

            XElement propertiesElement = root.Element("properties") ?? throw new ThemeException("Missing properties element");
            IThemeMetrics metrics = CreateMetrics(propertiesElement);

            XElement modelElement = root.Element("model") ?? throw new ThemeException("Missing model element");
            IThemeModel model = ThemeModel.Load(modelElement);

            return new Theme(metrics, model);
        }

        public static Theme LoadFile(string file)
        {
            if (!File.Exists(file))
            {
                throw new ThemeException($"Cannot find theme file named '{file}'.");
            }

            return LoadDocument(XDocument.Parse(File.ReadAllText(file)));
        }

        public static IEnumerable<(Theme, string)> LoadValidThemes()
        {
            if (!Directory.Exists(ThemesDirectory))
            {
                throw new ThemeException("Themes directory was not found");
            }

            var themes = new List<(Theme, string)>();

            foreach (string file in Directory.GetFiles(ThemesDirectory))
            {
                try
                {
                    string contents = File.ReadAllText(file);
                    XDocument document = XDocument.Parse(contents);
                    themes.Add((LoadDocument(document), file));
                }
                catch
                {
                }
            }

            return themes;
        }

        public static void Assign(MenuItem menuItem)
        {
            menuItem.Items.Clear();

            foreach ((Theme theme, string path) in LoadValidThemes())
            {
                var mi = new MenuItem
                {
                    Header = theme.Metrics.Name,
                    Command = new RelayCommand(
                        e =>
                        {
                            File.WriteAllText("trackersettings.xml", $@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<settingsRoot>
    <activeTheme path=""{path}"" />
</settingsRoot>");
                            ResourceDictionary resourceDictionary = theme.CreateDictionary();
                            App.Current.Resources = resourceDictionary;

                            CurrentApp.DoInvokeThemeChanged();
                        },
                        e => true
                    )
                };
                menuItem.Items.Add(mi);
            }
        }

        private static ThemeMetrics CreateMetrics(XElement element)
        {
            // Double check
            if (element.Name != "properties")
            {
                throw new InvalidOperationException($"Theme metrics element not found");
            }

            var descendants = element.Elements();

            return new ThemeMetrics(
                descendants.First(d => d.Name == "name").Value,
                descendants.First(d => d.Name == "description").Value,
                descendants.First(d => d.Name == "publisher").Value,
                bool.Parse(descendants.First(d => d.Name == "predefined").Value)
            );
        }
    }
}
