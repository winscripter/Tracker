using ICSharpCode.AvalonEdit.Highlighting;
using System.Windows.Media;

namespace Tracker.AvalonEditStyle
{
    public class AvalonEditDarkMode : IAvalonEditStyleProvider
    {
        public SimpleHighlightingBrush HighlightingNumberLiteral => new(Colors.Aquamarine);

        public SimpleHighlightingBrush HighlightingComment => new(Colors.Green);

        public SimpleHighlightingBrush HighlightingMethodCall => new(Colors.Goldenrod);

        public SimpleHighlightingBrush HighlightingGetSetAddRemove => new(Colors.DodgerBlue);

        public SimpleHighlightingBrush HighlightingVisibility => new(Colors.White);

        public SimpleHighlightingBrush HighlightingParameterModifiers => new(Colors.DodgerBlue);

        public SimpleHighlightingBrush HighlightingModifiers => new(Colors.DodgerBlue);

        public SimpleHighlightingBrush HighlightingString => new(Colors.Orange);

        public SimpleHighlightingBrush HighlightingChar => new(Colors.Orange);

        public SimpleHighlightingBrush HighlightingPreprocessor => new(Colors.Gray);

        public SimpleHighlightingBrush HighlightingTrueFalse => new(Colors.DodgerBlue);

        public SimpleHighlightingBrush HighlightingKeywords => new(Colors.DodgerBlue);

        public SimpleHighlightingBrush HighlightingValueTypeKeywords => new(Colors.DodgerBlue);

        public SimpleHighlightingBrush HighlightingSemanticKeywords => new(Colors.DodgerBlue);

        public SimpleHighlightingBrush HighlightingNamespaceKeywords => new(Colors.DodgerBlue);

        public SimpleHighlightingBrush HighlightingReferenceTypeKeywords => new(Colors.DodgerBlue);

        public SimpleHighlightingBrush HighlightingThisOrBaseReference => new(Colors.DodgerBlue);

        public SimpleHighlightingBrush HighlightingNullOrValueKeywords => new(Colors.DodgerBlue);

        public SimpleHighlightingBrush HighlightingGotoKeywords => new(Colors.Magenta);

        public SimpleHighlightingBrush HighlightingContextKeywords => new(Colors.DodgerBlue);

        public SimpleHighlightingBrush HighlightingExceptionKeywords => new(Colors.Magenta);

        public SimpleHighlightingBrush HighlightingCheckedKeyword => new(Colors.Magenta);

        public SimpleHighlightingBrush HighlightingUnsafeKeywords => new(Colors.DodgerBlue);

        public SimpleHighlightingBrush HighlightingOperatorKeywords => new(Colors.DodgerBlue);

        public Brush LineNumberBackground => (SolidColorBrush)Cache.BrushConverter.ConvertFrom("#FF293955")!;

        public Brush LineNumberForeground => Brushes.White;

        public Brush Background => (SolidColorBrush)Cache.BrushConverter.ConvertFrom("#242424")!;
    }
}
