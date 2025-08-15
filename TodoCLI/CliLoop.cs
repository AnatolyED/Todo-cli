using TodoCLI.Services;

namespace TodoCLI
{
    public static class CliLoop
    {
        public static async Task RunAsync(string[] args, ITodoRepository repo)
        {
            if (args.Length == 0)
            {
                PrintUsage();
                return;
            }

            var command = args[0].ToLowerInvariant();

            try
            {
                switch (command)
                {
                    case "add":
                        await HandleAddAsync(args, repo);
                        break;
                    case "list":
                        await HandleListAsync(repo);
                        break;
                    case "done":
                        await HandleDoneAsync(args, repo);
                        break;
                    case "delete":
                        await HandleDeleteAsync(args, repo);
                        break;
                    default:
                        PrintUsage();
                        break;
                }
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Ошибка: {ex.Message}");
                Console.ResetColor();
            }
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Использование:");
            Console.WriteLine("  dotnet run -- add \"Название задачи\"");
            Console.WriteLine("  dotnet run -- list");
            Console.WriteLine("  dotnet run -- done <id>");
            Console.WriteLine("  dotnet run -- delete <id>");
        }

        private static async Task HandleAddAsync(string[] args, ITodoRepository repo) 
        {
            if (args.Length < 2)
                throw new ArgumentException("Не указано название задачи.");

            var title = string.Join(' ', args.Skip(1));
            var id = await repo.AddAsync(title);
            Console.WriteLine($"Задача добавлена с id = {id}");
        }
        private static async Task HandleListAsync(ITodoRepository repo) 
        {
            var items = await repo.GetAllAsync();
            if(items.Count == 0)
            {
                Console.WriteLine("Список пуст");
                return;
            }

            foreach(var item in items)
            {
                var status = item.IsDone ? "[x]" : "[ ]";
                Console.WriteLine($"{item.Id,3} {status} {item.Title}" +
                    $"({item.CreatedAt:HH:mm dd-MM-yyyy})");
            }
        }
        private static async Task HandleDoneAsync(string[] args, ITodoRepository repo)
        {
            if (args.Length < 2 || !int.TryParse(args[1], out var id))
                throw new ArgumentException("Не указан или неверный id.");

            var ok = await repo.MarkDoneAsync(id);
            Console.WriteLine(ok ? $"Задача {id} помечена выполненной." : $"Задача {id} не найдена.");
        }
        private static async Task HandleDeleteAsync(string[] args, ITodoRepository repo)
        {
            if (args.Length < 2 || !int.TryParse(args[1], out var id))
                throw new ArgumentException("Не указан или неверный id.");

            var ok = await repo.DeleteAsync(id);
            Console.WriteLine(ok ? $"Задача {id} удалена." : $"Задача {id} не найдена.");
        }
    }
}
