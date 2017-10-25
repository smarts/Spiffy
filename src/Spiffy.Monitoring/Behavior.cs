using System;
using System.Diagnostics;

namespace Spiffy.Monitoring
{
    public static class Behavior
    {
        static Action<Level, string> _loggingAction;

        public static void UseBuiltInLogging(BuiltInLogging behavior)
        {
            switch (behavior)
            {
                case Monitoring.BuiltInLogging.Console:
                    _loggingAction = (level, message) => Console.WriteLine(message);
                    break;
                case Monitoring.BuiltInLogging.Trace:
                    _loggingAction = (level, message) => Trace.WriteLine(message);
                    break;
                default:
                    throw new NotSupportedException($"{behavior} is not supported");
            }

        }

        public static void UseCustomLogging(Action<Level, string> loggingAction)
        {
            _loggingAction = loggingAction;
        }

        internal static Action<Level, string> GetLoggingAction()
        {
            return _loggingAction;
        }
    }

    public enum BuiltInLogging
    {
        Trace,
        Console
    }
}