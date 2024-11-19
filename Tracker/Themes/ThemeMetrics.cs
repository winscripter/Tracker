namespace Tracker.Themes
{
    internal sealed class ThemeMetrics : IThemeMetrics
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public bool IsPredefined { get; set; }

        internal ThemeMetrics(string name, string description, string publisher, bool isPredefined)
        {
            Name = name;
            Description = description;
            Publisher = publisher;
            IsPredefined = isPredefined;
        }
    }
}
