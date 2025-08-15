using TodoCLI.Models;

namespace TodoCLI.Services;

public class InMemoryTodoRepository : ITodoRepository
{
    private readonly List<TodoItem> _items = new();
    private int _nextId = 1;

    public Task<int> AddAsync(string title)
    {
        var item = new TodoItem(title)
        {
            Id = Interlocked.Increment(ref _nextId)
        };
        _items.Add(item);
        return Task.FromResult(item.Id);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var removed = _items.RemoveAll(i => i.Id == id);
        return Task.FromResult(removed > 0);
    }

    public Task<IReadOnlyList<TodoItem>> GetAllAsync()
    {
        return Task.FromResult<IReadOnlyList<TodoItem>>(_items);
    }

    public Task<bool> MarkDoneAsync(int id)
    {
        var item = _items.Find(i => i.Id == id);
        if (item is null) return Task.FromResult(false);
        item.MarkDone();
        return Task.FromResult(true);
    }
}
