using System.Windows.Media;

namespace Tracker.Themes
{
    internal interface IThemeModel
    {
        SolidColorBrush CommonPaneBG { get; set; }
        SolidColorBrush CommonPaneFG { get; set; }
        SolidColorBrush DefaultBG { get; set; }
        SolidColorBrush DefaultFG { get; set; }
        SolidColorBrush DirObjectBG { get; set; }
        SolidColorBrush DirObjectFG { get; set; }
        SolidColorBrush DirObjectHoverBG { get; set; }
        SolidColorBrush DirObjectHoverFG { get; set; }
        SolidColorBrush FileObjectBG { get; set; }
        SolidColorBrush FileObjectFG { get; set; }
        SolidColorBrush FileObjectHoverBG { get; set; }
        SolidColorBrush FileObjectHoverFG { get; set; }
        SolidColorBrush HyperlinkActiveBG { get; set; }
        SolidColorBrush HyperlinkActiveFG { get; set; }
        SolidColorBrush HyperlinkBG { get; set; }
        SolidColorBrush HyperlinkFG { get; set; }
        SolidColorBrush HyperlinkHoverBG { get; set; }
        SolidColorBrush HyperlinkHoverFG { get; set; }
        SolidColorBrush ParagraphActiveBG { get; set; }
        SolidColorBrush ParagraphActiveFG { get; set; }
        SolidColorBrush ParagraphBG { get; set; }
        SolidColorBrush ParagraphFG { get; set; }
        SolidColorBrush ParagraphHoverBG { get; set; }
        SolidColorBrush ParagraphHoverFG { get; set; }
    }
}