using TodoCLI.Models;

namespace TodoCLI.Services;

public interface ITodoRepository
{
    Task<int> AddAsync(string title);
    Task<IReadOnlyList<TodoItem>> GetAllAsync();
    Task<bool> MarkDoneAsync(int id);
    Task<bool> DeleteAsync(int id);
}
