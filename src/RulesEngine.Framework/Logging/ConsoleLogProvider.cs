using Hein.Framework.Serialization;
using System;

namespace Hein.RulesEngine.Framework.Logging
{
    public class ConsoleLogProvider : LogProviderBase
    {
        public ConsoleLogProvider(ILogConfiguration config) : base(config)
        { }

        public override void Debug(string message, object properties = null)
        {
            if (IsEnabled("Debug"))
            {
                var propString = properties != null ? string.Format("\n[DEBUG] Properites:{0}", properties.ToJson()) : "";
                Console.WriteLine(string.Format("[DEBUG] {0}{1}", message, propString));
            }
        }

        public override void Error(string message)
        {
            Error(message, null, null);
        }

        public override void Error(Exception ex)
        {
            Error(ex.Message, ex);
        }

        public override void Error(string message, Exception ex, object properties = null)
        {
            if (IsEnabled("Error"))
            {
                var propString = properties != null ? string.Format("\n[ERROR] Properites:{0}", properties.ToJson()) : "";
                Console.WriteLine(string.Format("[ERROR] {0}{1}", message, propString));
                if (ex != null)
                {
                    Console.WriteLine("========= Start Exception =========");
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine("===================================");
                }
            }
        }

        public override void Info(string message, object properties = null)
        {
            if (IsEnabled("Info"))
            {
                var propString = properties != null ? string.Format("\n[INFO] Properites:{0}", properties.ToJson()) : "";
                Console.WriteLine(string.Format("[INFO] {0}{1}", message, propString));
            }
        }

        public override void Warn(string message, object properties = null)
        {
            Warn(message, null, properties);
        }

        public override void Warn(string message, Exception ex = null, object properties = null)
        {
            if (IsEnabled("Warn"))
            {
                var propString = properties != null ? string.Format("\n[WARN] Properites:{0}", properties.ToJson()) : "";
                Console.WriteLine(string.Format("[WARN] {0}{1}", message, propString));
                if (ex != null)
                {
                    Console.WriteLine("========= Start Exception =========");
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine("===================================");
                }
            }
        }
    }
}
