using ICSharpCode.AvalonEdit;
using System.Windows.Media;

namespace Tracker.AvalonEditStyle
{
    /// <summary>
    /// Handles theme/color value management for AvalonEdit.
    /// </summary>
    public static class AvalonEditTheming
    {
        public static void Apply(TextEditor editor, IAvalonEditStyleProvider styleProvider, bool assumeCSharp)
        {
            var highlighting = editor.SyntaxHighlighting;
            if (assumeCSharp)
            {
                highlighting.GetNamedColor("NumberLiteral").Foreground = styleProvider.HighlightingNumberLiteral;
                highlighting.GetNamedColor("Comment").Foreground = styleProvider.HighlightingComment;
                highlighting.GetNamedColor("MethodCall").Foreground = styleProvider.HighlightingMethodCall;
                highlighting.GetNamedColor("GetSetAddRemove").Foreground = styleProvider.HighlightingGetSetAddRemove;
                highlighting.GetNamedColor("Visibility").Foreground = styleProvider.HighlightingVisibility;
                highlighting.GetNamedColor("ParameterModifiers").Foreground = styleProvider.HighlightingParameterModifiers;
                highlighting.GetNamedColor("Modifiers").Foreground = styleProvider.HighlightingModifiers;
                highlighting.GetNamedColor("String").Foreground = styleProvider.HighlightingString;
                highlighting.GetNamedColor("Char").Foreground = styleProvider.HighlightingChar;
                highlighting.GetNamedColor("Preprocessor").Foreground = styleProvider.HighlightingPreprocessor;
                highlighting.GetNamedColor("TrueFalse").Foreground = styleProvider.HighlightingTrueFalse;
                highlighting.GetNamedColor("Keywords").Foreground = styleProvider.HighlightingKeywords;
                highlighting.GetNamedColor("ValueTypeKeywords").Foreground = styleProvider.HighlightingValueTypeKeywords;
                highlighting.GetNamedColor("SemanticKeywords").Foreground = styleProvider.HighlightingSemanticKeywords;
                highlighting.GetNamedColor("NamespaceKeywords").Foreground = styleProvider.HighlightingNamespaceKeywords;
                highlighting.GetNamedColor("ReferenceTypeKeywords").Foreground = styleProvider.HighlightingReferenceTypeKeywords;
                highlighting.GetNamedColor("ThisOrBaseReference").Foreground = styleProvider.HighlightingThisOrBaseReference;
                highlighting.GetNamedColor("NullOrValueKeywords").Foreground = styleProvider.HighlightingNullOrValueKeywords;
                highlighting.GetNamedColor("GotoKeywords").Foreground = styleProvider.HighlightingGotoKeywords;
                highlighting.GetNamedColor("ContextKeywords").Foreground = styleProvider.HighlightingContextKeywords;
                highlighting.GetNamedColor("ExceptionKeywords").Foreground = styleProvider.HighlightingExceptionKeywords;
                highlighting.GetNamedColor("CheckedKeyword").Foreground = styleProvider.HighlightingCheckedKeyword;
                highlighting.GetNamedColor("UnsafeKeywords").Foreground = styleProvider.HighlightingUnsafeKeywords;
                highlighting.GetNamedColor("OperatorKeywords").Foreground = styleProvider.HighlightingOperatorKeywords;
                highlighting.GetNamedColor("SemanticKeywords").Foreground = styleProvider.HighlightingSemanticKeywords;
            }
            
            editor.LineNumbersForeground = styleProvider.LineNumberForeground;
            editor.Background = styleProvider.Background;

            if (styleProvider is AvalonEditDarkMode)
            {
                editor.Foreground = Brushes.White;
            }
            else
            {
                editor.Foreground = Brushes.Black;
            }

            try
            {
                foreach (var color in highlighting.NamedHighlightingColors)
                {
                    color.FontWeight = null;
                }
            }
            catch
            {
            }

            editor.SyntaxHighlighting = null;
            editor.SyntaxHighlighting = highlighting;
        }
    }
}
