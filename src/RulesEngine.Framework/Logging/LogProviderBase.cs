using Hein.Framework.Extensions;
using System;

namespace Hein.RulesEngine.Framework.Logging
{
    public abstract class LogProviderBase : ILogProvider
    {
        protected readonly ILogConfiguration _config;
        protected LogProviderBase(ILogConfiguration config)
        {
            _config = config;
        }

        public abstract void Debug(string message, object properties = null);
        public abstract void Error(string message);
        public abstract void Error(Exception ex);
        public abstract void Error(string message, Exception ex, object properties = null);
        public abstract void Info(string message, object properties = null);
        public abstract void Warn(string message, object properties = null);
        public abstract void Warn(string message, Exception ex = null, object properties = null);

        protected bool IsEnabled(string logLevel)
        {
            return logLevel.IsOneOf(_config.EnabledLevels);
        }
    }
}
