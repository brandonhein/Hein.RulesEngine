namespace Hein.RulesEngine.Domain.Models
{
    public class Rule
    {
        public string Name { get; set; }
        public int Priority { get; set; }
        public string Setups { get; set; }
        public string Condition { get; set; }
        public string Actions { get; set; }
    }
}
