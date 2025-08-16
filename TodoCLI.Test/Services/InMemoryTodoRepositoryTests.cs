using FluentAssertions;
using TodoCLI.Services;

namespace TodoCLI.Test.Services;

public class InMemoryTodoRepositoryTests
{
    [Fact]
    public async Task AddAsync_ShouldReturnIncrementedId()
    {
        var repo = new InMemoryTodoRepository();

        var id1 = await repo.AddAsync("A");
        var id2 = await repo.AddAsync("B");

        id1.Should().Be(1);
        id2.Should().Be(2);
    }

    [Fact]
    public async Task MarkDoneAsync_ShouldReturnTrue_WhenExists()
    {
        var repo = new InMemoryTodoRepository();
        var id = await repo.AddAsync("Test");

        var ok = await repo.MarkDoneAsync(id);
        ok.Should().BeTrue();
    }

    [Fact]
    public async Task MarkDoneAsync_ShouldReturnFalse_WhenNotFound()
    {
        var repo = new InMemoryTodoRepository();
        var ok = await repo.MarkDoneAsync(999);
        ok.Should().BeFalse();
    }
}
