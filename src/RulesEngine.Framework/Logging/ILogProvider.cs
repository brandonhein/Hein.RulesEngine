using System;

namespace Hein.RulesEngine.Framework.Logging
{
    public interface ILogProvider
    {
        void Debug(string message, object properties = null);
        void Info(string message, object properties = null);
        void Warn(string message, object properties = null);
        void Warn(string message, Exception ex = null, object properties = null);
        void Error(string message);
        void Error(Exception ex);
        void Error(string message, Exception ex, object properties = null);
    }
}
