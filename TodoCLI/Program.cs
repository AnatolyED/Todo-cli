using Serilog;
using TodoCLI.Logging;
using TodoCLI.Services;

namespace TodoCLI;

class Program
{
    static async Task Main(string[] args)
    {
        Log.Logger = LoggerConfig.CreateLogger();
        var repo = new InMemoryTodoRepository();

        // Если args пустые — интерактивный режим
        if (args.Length == 0)
        {
            await RunInteractiveMode(repo);
        }
        else
        {
            // Классический режим (чтобы можно было запускать из консоли)
            try
            {
                await CliLoop.RunAsync(args, repo);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Приложение упало");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }

    private static async Task RunInteractiveMode(ITodoRepository repo)
    {
        Console.WriteLine("Todo CLI (интерактивный режим)");
        Console.WriteLine("Команды: add, list, done <id>, delete <id>, exit");
        while (true)
        {
            Console.Write("> ");
            var line = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(line)) continue;

            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts[0].Equals("exit", StringComparison.OrdinalIgnoreCase))
                break;

            await CliLoop.RunAsync(parts, repo);
        }
    }
}
