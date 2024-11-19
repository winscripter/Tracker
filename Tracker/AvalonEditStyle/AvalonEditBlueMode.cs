using ICSharpCode.AvalonEdit.Highlighting;
using System.Windows.Media;

namespace Tracker.AvalonEditStyle
{
    public class AvalonEditBlueMode : IAvalonEditStyleProvider
    {
        public SimpleHighlightingBrush HighlightingNumberLiteral => new(Colors.DarkSeaGreen);

        public SimpleHighlightingBrush HighlightingComment => new(Colors.Green);

        public SimpleHighlightingBrush HighlightingMethodCall => new(Colors.Goldenrod);

        public SimpleHighlightingBrush HighlightingGetSetAddRemove => new(Colors.Blue);

        public SimpleHighlightingBrush HighlightingVisibility => new(Colors.Black);

        public SimpleHighlightingBrush HighlightingParameterModifiers => new(Colors.Blue);

        public SimpleHighlightingBrush HighlightingModifiers => new(Colors.Blue);

        public SimpleHighlightingBrush HighlightingString => new(Colors.Orange);

        public SimpleHighlightingBrush HighlightingChar => new(Colors.Orange);

        public SimpleHighlightingBrush HighlightingPreprocessor => new(Colors.Gray);

        public SimpleHighlightingBrush HighlightingTrueFalse => new(Colors.Blue);

        public SimpleHighlightingBrush HighlightingKeywords => new(Colors.Blue);

        public SimpleHighlightingBrush HighlightingValueTypeKeywords => new(Colors.Blue);

        public SimpleHighlightingBrush HighlightingSemanticKeywords => new(Colors.Blue);

        public SimpleHighlightingBrush HighlightingNamespaceKeywords => new(Colors.Blue);

        public SimpleHighlightingBrush HighlightingReferenceTypeKeywords => new(Colors.Blue);

        public SimpleHighlightingBrush HighlightingThisOrBaseReference => new(Colors.Blue);

        public SimpleHighlightingBrush HighlightingNullOrValueKeywords => new(Colors.Blue);

        public SimpleHighlightingBrush HighlightingGotoKeywords => new(Colors.DarkMagenta);

        public SimpleHighlightingBrush HighlightingContextKeywords => new(Colors.Blue);

        public SimpleHighlightingBrush HighlightingExceptionKeywords => new(Colors.DarkMagenta);

        public SimpleHighlightingBrush HighlightingCheckedKeyword => new(Colors.DarkMagenta);

        public SimpleHighlightingBrush HighlightingUnsafeKeywords => new(Colors.Blue);

        public SimpleHighlightingBrush HighlightingOperatorKeywords => new(Colors.Blue);

        public Brush LineNumberBackground => (SolidColorBrush)Cache.BrushConverter.ConvertFrom("#FF293955")!;

        public Brush LineNumberForeground => Brushes.Gray;

        public Brush Background => Brushes.White;
    }
}
