using System.Diagnostics.CodeAnalysis;
using System.Windows.Media;
using System.Xml.Linq;

namespace Tracker.Themes
{
    internal sealed class ThemeModel : IThemeModel
    {
        private static readonly BrushConverter s_brushConverter = new();

        public SolidColorBrush ParagraphBG { get; set; }
        public SolidColorBrush ParagraphFG { get; set; }
        public SolidColorBrush ParagraphHoverBG { get; set; }
        public SolidColorBrush ParagraphHoverFG { get; set; }
        public SolidColorBrush ParagraphActiveBG { get; set; }
        public SolidColorBrush ParagraphActiveFG { get; set; }

        public SolidColorBrush HyperlinkBG { get; set; }
        public SolidColorBrush HyperlinkFG { get; set; }
        public SolidColorBrush HyperlinkHoverBG { get; set; }
        public SolidColorBrush HyperlinkHoverFG { get; set; }
        public SolidColorBrush HyperlinkActiveBG { get; set; }
        public SolidColorBrush HyperlinkActiveFG { get; set; }

        public SolidColorBrush DefaultBG { get; set; }
        public SolidColorBrush DefaultFG { get; set; }

        public SolidColorBrush CommonPaneBG { get; set; }
        public SolidColorBrush CommonPaneFG { get; set; }

        public SolidColorBrush FileObjectBG { get; set; }
        public SolidColorBrush FileObjectFG { get; set; }
        public SolidColorBrush FileObjectHoverBG { get; set; }
        public SolidColorBrush FileObjectHoverFG { get; set; }

        public SolidColorBrush DirObjectBG { get; set; }
        public SolidColorBrush DirObjectFG { get; set; }
        public SolidColorBrush DirObjectHoverBG { get; set; }
        public SolidColorBrush DirObjectHoverFG { get; set; }

        public SolidColorBrush FileObjectBorder { get; set; }
        public SolidColorBrush DirObjectBorder { get; set; }

        private ThemeModel(
            SolidColorBrush paragraphBG, SolidColorBrush paragraphFG, SolidColorBrush paragraphHoverBG,
            SolidColorBrush paragraphHoverFG, SolidColorBrush paragraphActiveBG, SolidColorBrush paragraphActiveFG,
            SolidColorBrush hyperlinkBG, SolidColorBrush hyperlinkFG, SolidColorBrush hyperlinkHoverBG,
            SolidColorBrush hyperlinkHoverFG, SolidColorBrush hyperlinkActiveBG, SolidColorBrush hyperlinkActiveFG,
            SolidColorBrush defaultBG, SolidColorBrush defaultFG, SolidColorBrush commonPaneBG,
            SolidColorBrush commonPaneFG, SolidColorBrush fileObjectBG, SolidColorBrush fileObjectFG,
            SolidColorBrush fileObjectHoverBG, SolidColorBrush fileObjectHoverFG, SolidColorBrush dirObjectBG,
            SolidColorBrush dirObjectFG, SolidColorBrush dirObjectHoverBG, SolidColorBrush dirObjectHoverFG,
            SolidColorBrush fileObjectBorder, SolidColorBrush dirObjectBorder)
        {
            ParagraphBG = paragraphBG ?? throw new ArgumentNullException(nameof(paragraphBG));
            ParagraphFG = paragraphFG ?? throw new ArgumentNullException(nameof(paragraphFG));
            ParagraphHoverBG = paragraphHoverBG ?? throw new ArgumentNullException(nameof(paragraphHoverBG));
            ParagraphHoverFG = paragraphHoverFG ?? throw new ArgumentNullException(nameof(paragraphHoverFG));
            ParagraphActiveBG = paragraphActiveBG ?? throw new ArgumentNullException(nameof(paragraphActiveBG));
            ParagraphActiveFG = paragraphActiveFG ?? throw new ArgumentNullException(nameof(paragraphActiveFG));
            HyperlinkBG = hyperlinkBG ?? throw new ArgumentNullException(nameof(hyperlinkBG));
            HyperlinkFG = hyperlinkFG ?? throw new ArgumentNullException(nameof(hyperlinkFG));
            HyperlinkHoverBG = hyperlinkHoverBG ?? throw new ArgumentNullException(nameof(hyperlinkHoverBG));
            HyperlinkHoverFG = hyperlinkHoverFG ?? throw new ArgumentNullException(nameof(hyperlinkHoverFG));
            HyperlinkActiveBG = hyperlinkActiveBG ?? throw new ArgumentNullException(nameof(hyperlinkActiveBG));
            HyperlinkActiveFG = hyperlinkActiveFG ?? throw new ArgumentNullException(nameof(hyperlinkActiveFG));
            DefaultBG = defaultBG ?? throw new ArgumentNullException(nameof(defaultBG));
            DefaultFG = defaultFG ?? throw new ArgumentNullException(nameof(defaultFG));
            CommonPaneBG = commonPaneBG ?? throw new ArgumentNullException(nameof(commonPaneBG));
            CommonPaneFG = commonPaneFG ?? throw new ArgumentNullException(nameof(commonPaneFG));
            FileObjectBG = fileObjectBG ?? throw new ArgumentNullException(nameof(fileObjectBG));
            FileObjectFG = fileObjectFG ?? throw new ArgumentNullException(nameof(fileObjectFG));
            FileObjectHoverBG = fileObjectHoverBG ?? throw new ArgumentNullException(nameof(fileObjectHoverBG));
            FileObjectHoverFG = fileObjectHoverFG ?? throw new ArgumentNullException(nameof(fileObjectHoverFG));
            DirObjectBG = dirObjectBG ?? throw new ArgumentNullException(nameof(dirObjectBG));
            DirObjectFG = dirObjectFG ?? throw new ArgumentNullException(nameof(dirObjectFG));
            DirObjectHoverBG = dirObjectHoverBG ?? throw new ArgumentNullException(nameof(dirObjectHoverBG));
            DirObjectHoverFG = dirObjectHoverFG ?? throw new ArgumentNullException(nameof(dirObjectHoverFG));
            FileObjectBorder = fileObjectBorder ?? throw new ArgumentNullException(nameof(fileObjectBorder));
            DirObjectBorder = dirObjectBorder ?? throw new ArgumentNullException(nameof(dirObjectBorder));
        }

        public static ThemeModel Load([AllowNull] XElement element)
        {
            ArgumentNullException.ThrowIfNull(element, nameof(element));
            XElement[] elements = element.Elements().ToArray();

            return new(
                Object("ParagraphBG"),
                Object("ParagraphFG"),
                Object("ParagraphHoverBG"),
                Object("ParagraphHoverFG"),
                Object("ParagraphActiveBG"),
                Object("ParagraphActiveFG"),
                Object("HyperlinkBG"),
                Object("HyperlinkFG"),
                Object("HyperlinkHoverBG"),
                Object("HyperlinkHoverFG"),
                Object("HyperlinkActiveBG"),
                Object("HyperlinkActiveFG"),
                Object("DefaultBG"),
                Object("DefaultFG"),
                Object("CommonPaneBG"),
                Object("CommonPaneFG"),
                Object("FileObjectBG"),
                Object("FileObjectFG"),
                Object("FileObjectHoverBG"),
                Object("FileObjectHoverFG"),
                Object("DirObjectBG"),
                Object("DirObjectFG"),
                Object("DirObjectHoverBG"),
                Object("DirObjectHoverFG"),
                Object("FileObjectBorder"),
                Object("DirObjectBorder")
            );

            SolidColorBrush Object(string name)
            {
                string elementRawValue = element.Elements().First(e => e.Name.LocalName == name).Value;
                return (SolidColorBrush?)s_brushConverter.ConvertFrom(elementRawValue) ?? throw new InvalidOperationException($"Invalid theme object {elementRawValue}");
            }
        }
    }
}
