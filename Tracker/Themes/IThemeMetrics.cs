namespace Tracker.Themes
{
    internal interface IThemeMetrics
    {
        string Description { get; set; }
        bool IsPredefined { get; set; }
        string Name { get; set; }
        string Publisher { get; set; }
    }
}