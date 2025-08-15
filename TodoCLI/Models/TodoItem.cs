namespace TodoCLI.Models;

public class TodoItem
{
    public int Id { get; init; }
    public string Title { get; private set; }
    public bool IsDone { get; private set; }
    public DateTime CreatedAt { get; init; }

    public TodoItem(string title)
    {
        Title = title;
        CreatedAt = DateTime.UtcNow;
    }

    public void MarkDone() => IsDone = true;
}
