using ICSharpCode.AvalonEdit.Highlighting;
using System.Windows.Media;

namespace Tracker.AvalonEditStyle
{
    /// <summary>
    /// Color values for Avalon Edit.
    /// </summary>
    public interface IAvalonEditStyleProvider
    {
        /// <summary>
        /// Color value for a number literal
        /// </summary>
        SimpleHighlightingBrush HighlightingNumberLiteral { get; }

        /// <summary>
        /// Color value for a comment
        /// </summary>
        SimpleHighlightingBrush HighlightingComment { get; }

        /// <summary>
        /// Color value for a method invocation
        /// </summary>
        SimpleHighlightingBrush HighlightingMethodCall { get; }

        /// <summary>
        /// Color value for a C# accessor (get, set, add, remove)
        /// </summary>
        SimpleHighlightingBrush HighlightingGetSetAddRemove { get; }

        /// <summary>
        /// Color value for a visibility.
        /// </summary>
        SimpleHighlightingBrush HighlightingVisibility { get; }

        /// <summary>
        /// Color value for a parameter modifier (ref, out, in)
        /// </summary>
        SimpleHighlightingBrush HighlightingParameterModifiers { get; }

        /// <summary>
        /// Color value for modifiers (public, private, protected, internal)
        /// </summary>
        SimpleHighlightingBrush HighlightingModifiers { get; }

        /// <summary>
        /// Color value for strings
        /// </summary>
        SimpleHighlightingBrush HighlightingString { get; }

        /// <summary>
        /// Color value for chars
        /// </summary>
        SimpleHighlightingBrush HighlightingChar { get; }

        /// <summary>
        /// Color value for preprocessor directives
        /// </summary>
        SimpleHighlightingBrush HighlightingPreprocessor { get; }

        /// <summary>
        /// Color value for true or false keywords
        /// </summary>
        SimpleHighlightingBrush HighlightingTrueFalse { get; }

        /// <summary>
        /// Color value for general C# keywords
        /// </summary>
        SimpleHighlightingBrush HighlightingKeywords { get; }

        /// <summary>
        /// Color value for C# value type keywords
        /// </summary>
        SimpleHighlightingBrush HighlightingValueTypeKeywords { get; }

        /// <summary>
        /// Color value for C# semantic keywords
        /// </summary>
        SimpleHighlightingBrush HighlightingSemanticKeywords { get; }

        /// <summary>
        /// Color value for C# namespace keywords
        /// </summary>
        SimpleHighlightingBrush HighlightingNamespaceKeywords { get; }

        /// <summary>
        /// Color value for reference type keywords
        /// </summary>
        SimpleHighlightingBrush HighlightingReferenceTypeKeywords { get; }

        /// <summary>
        /// Color value for C# 'this' or 'base' keywords
        /// </summary>
        SimpleHighlightingBrush HighlightingThisOrBaseReference { get; }

        /// <summary>
        /// Color value for null
        /// </summary>
        SimpleHighlightingBrush HighlightingNullOrValueKeywords { get; }

        /// <summary>
        /// Color value for goto
        /// </summary>
        SimpleHighlightingBrush HighlightingGotoKeywords { get; }

        /// <summary>
        /// Color value for contextual keywords, like 'var', LINQ query expressions, 'notnull', and such.
        /// See <see href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/#contextual-keywords"/>.
        /// </summary>
        SimpleHighlightingBrush HighlightingContextKeywords { get; }

        /// <summary>
        /// Color value for exception keywords (try, catch, throw, finally)
        /// </summary>
        SimpleHighlightingBrush HighlightingExceptionKeywords { get; }

        /// <summary>
        /// Color value for checked keywords (checked, unchecked)
        /// </summary>
        SimpleHighlightingBrush HighlightingCheckedKeyword { get; }

        /// <summary>
        /// Color value for unsafe keywords (apparently unsafe and fixed)
        /// </summary>
        SimpleHighlightingBrush HighlightingUnsafeKeywords { get; }

        /// <summary>
        /// Color value for operator keywords
        /// </summary>
        SimpleHighlightingBrush HighlightingOperatorKeywords { get; }

        /// <summary>
        /// Color value for the line numbers background
        /// </summary>
        Brush LineNumberBackground { get; }

        /// <summary>
        /// Color value for the line numbers foreground
        /// </summary>
        Brush LineNumberForeground { get; }

        /// <summary>
        /// Color value for the editor background.
        /// </summary>
        Brush Background { get; }
    }
}
