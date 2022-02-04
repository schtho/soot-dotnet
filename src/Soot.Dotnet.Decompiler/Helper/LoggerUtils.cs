using NLog;
using NLog.Layouts;

namespace Soot.Dotnet.Decompiler.Helper
{
    public static class LoggerUtils
    {
        /// <summary>
        /// Initialize NLog with our configuration of console logging
        /// It is called one-time at AssemblyAnalyzer
        /// </summary>
        public static void InitNlog()
        {
            var config = new NLog.Config.LoggingConfiguration();
            
            // Targets where to log to: Console
            var logConsole = new NLog.Targets.ConsoleTarget("logconsole")
            {
                Layout = Layout.FromString("[Dotnet] " +
                                           "${level:uppercase=true:padding=-5} ${logger} - " +
                                           "${message}${literal:text=\n:when='${exception}'!=''} " +
                                           "${exception:format=tostring} " +
                                           "${literal:text=(Method <:when='${event-properties:item=TypeName}'!=''}" 
                                           + "${event-properties:item=TypeName}" 
                                           + "${literal:text=\\::when='${event-properties:item=Method}'!=''}" 
                                           + "${event-properties:item=Method}" +
                                           "${literal:text=>):when='${event-properties:item=TypeName}'!=''}")
            };

            // Rules for mapping loggers to targets
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logConsole);
            // Apply config
            LogManager.Configuration = config;
        }
    }
}