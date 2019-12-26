using Hein.Framework.DependencyInjection;
using Hein.RulesEngine.Framework.Logging;
using System;

namespace Hein.RulesEngine.Framework
{
    public static class RulesEngineLogger
    {
        private static ILogProvider _provider { get { return ServiceLocator.Get<ILogProvider>(); } }

        public static void Debug(string message)
        {
            _provider?.Debug(message);
        }

        public static void Debug(string message, object properties)
        {
            _provider?.Debug(message, properties);
        }

        public static void Info(string message)
        {
            _provider?.Info(message);
        }

        public static void Info(string message, object properties)
        {
            _provider?.Info(message, properties);
        }

        public static void Warn(string message)
        {
            _provider?.Warn(message, null);
        }

        public static void Warn(string message, object properties)
        {
            _provider?.Warn(message, properties);
        }

        public static void Warn(string message, Exception ex)
        {
            _provider?.Warn(message, ex, null);
        }

        public static void Warn(string message, Exception ex, object properties)
        {
            _provider?.Warn(message, ex, properties);
        }

        public static void Error(string message)
        {
            _provider?.Error(message);
        }

        public static void Error(Exception ex)
        {
            _provider?.Error(ex);
        }

        public static void Error(string message, Exception ex)
        {
            _provider?.Error(message, ex);
        }

        public static void Error(string message, Exception ex, object properties)
        {
            _provider?.Error(message, ex, properties);
        }
    }
}
