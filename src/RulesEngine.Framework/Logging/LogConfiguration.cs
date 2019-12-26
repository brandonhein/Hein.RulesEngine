namespace Hein.RulesEngine.Framework.Logging
{
    public class LogConfiguration : ILogConfiguration
    {
        public string SystemName { get; set; }
        public string Environment { get; set; }
        public string[] EnabledLevels { get; set; }
    }
}
