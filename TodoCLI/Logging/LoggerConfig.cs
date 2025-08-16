using Serilog;
namespace TodoCLI.Logging;

public static class LoggerConfig
{
    public static Serilog.ILogger CreateLogger() =>
        new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console(outputTemplate: "{Timestap:HH:mm:ss} [{Level:u3}] {Message:lj} {NewLine}{Exception}")
            .WriteTo.File
            (
                path: "Logs/todo-.txt",
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 7
            ).CreateLogger();

}
