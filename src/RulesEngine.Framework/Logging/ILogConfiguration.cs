namespace Hein.RulesEngine.Framework.Logging
{
    public interface ILogConfiguration
    {
        string SystemName { get; set; }
        string Environment { get; set; }
        string[] EnabledLevels { get; set; }
    }
}
