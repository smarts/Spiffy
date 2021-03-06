using System;
using System.Diagnostics;

namespace Spiffy.Monitoring
{
    public static class Behavior
    {
        static Action<Level, string> _loggingAction;

        /// <summary>
        /// Whether or not to remove newline characters from logged values.
        /// </summary>
        /// <returns>
        /// <code>true</code> if newline characters will be removed from logged
        /// values, <code>false</code> otherwise.
        /// </returns>
        public static bool RemoveNewlines { get; set; }

        public static void UseBuiltInLogging(BuiltInLogging behavior)
        {
            switch (behavior)
            {
                case Monitoring.BuiltInLogging.Console:
                    _loggingAction = (level, message) =>
                    {
                        if (level == Level.Error)
                        {
                            Console.Error.WriteLine(message);
                        }
                        else
                        {
                            Console.WriteLine(message);
                        }
                    };
                    break;
                case Monitoring.BuiltInLogging.Trace:
                    _loggingAction = (level, message) =>
                    {
                        switch (level)
                        {
                            case Level.Info:
                                Trace.TraceInformation(message);
                                break;
                            case Level.Warning:
                                Trace.TraceWarning(message);
                                break;
                            case Level.Error:
                                Trace.TraceError(message);
                                break;
                            default:
                                Trace.WriteLine(message);
                                break;
                        }
                    };
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
