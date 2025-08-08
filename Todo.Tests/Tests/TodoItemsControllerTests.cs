using NUnit.Framework;

using System;
using System.Linq;
using System.Threading.Tasks;

using Todo.Tests.Mocks;
using Todo.ViewModels.ViewModels;

namespace Todo.Tests.Tests;
[TestFixture]
public class TodoItemsControllerTests
{
    [Test]
    public async Task GetTodoItemByIdTestValid()
    {
        TodoItemViewModel result = await GetTodoItemByIdAsync(ResultType.Valid, 1);
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo("Připravit prezentaci"));
        Assert.That(result.Id, Is.EqualTo(1));
    }

    [Test]
    public async Task GetTodoItemByIdTestLimit()
    {
        TodoItemViewModel result = await GetTodoItemByIdAsync(ResultType.Limit, int.MaxValue);
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo("Testovací úkol s maximálním ID"));
        Assert.That(result.Id, Is.EqualTo(int.MaxValue));
    }

    [Test]
    public async Task GetTodoItemByIdTestInvalid()
    {
        TodoItemViewModel result = await GetTodoItemByIdAsync(ResultType.Invalid, -1);
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetTodoItemsByListIdTestValid()
    {
        TodoItemsViewModel result = await GetTodoItemsByListIdAsync(ResultType.Valid, 1);
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Items, Is.Not.Null);
        Assert.That(result.Items.Count, Is.EqualTo(2));
        Assert.That(result.Items.Any(i => i.Id == 1), Is.True);
    }

    [Test]
    public async Task GetTodoItemsByListIdTestLimit()
    {
        TodoItemsViewModel result = await GetTodoItemsByListIdAsync(ResultType.Limit, 1);
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Items, Is.Not.Null);
        Assert.That(result.Items.Count, Is.EqualTo(1000));
        String firstTitle = result.Items.First().Title;
        Assert.That(firstTitle, Is.EqualTo("Úkol 1"));
    }

    [Test]
    public async Task GetTodoItemsByListIdTestInvalid()
    {
        TodoItemsViewModel result = await GetTodoItemsByListIdAsync(ResultType.Invalid, -1);
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task AddTodoItemTestValid()
    {
        TodoItemViewModel item = new()
        {
            Title = "New Item",
            TodoListId = 1
        };
        bool result = await AddTodoItemAsync(ResultType.Valid, item);
        Assert.That(result, Is.True);
    }

    [Test]
    public async Task AddTodoItemTestInvalid()
    {
        TodoItemViewModel item = new();
        bool result = await AddTodoItemAsync(ResultType.Invalid, item);
        Assert.That(result, Is.False);
    }

    [Test]
    public async Task UpdateTodoItemTestValid()
    {
        TodoItemViewModel item = new()
        {
            Id = 1,
            Title = "Updated Item",
            TodoListId = 1
        };
        bool result = await UpdateTodoItemAsync(ResultType.Valid, item);
        Assert.That(result, Is.True);
    }

    [Test]
    public async Task UpdateTodoItemTestInvalid()
    {
        TodoItemViewModel item = new();
        bool result = await UpdateTodoItemAsync(ResultType.Invalid, item);
        Assert.That(result, Is.False);
    }

    [Test]
    public async Task DeleteTodoItemTestValid()
    {
        bool result = await DeleteTodoItemAsync(ResultType.Valid, 1);
        Assert.That(result, Is.True);
    }

    [Test]
    public async Task DeleteTodoItemTestInvalid()
    {
        bool result = await DeleteTodoItemAsync(ResultType.Invalid, -1);
        Assert.That(result, Is.False);
    }

    private static async Task<TodoItemViewModel> GetTodoItemByIdAsync(ResultType scenario, int id)
    {
        TestData.CurrentScenario = scenario;
        string uri = $"api/TodoItems/GetTodoItemById/{id}";
        await TestPlatform.Client.GetAsync(uri);
        return TestResults.GetTodoItemById;
    }

    private static async Task<TodoItemsViewModel> GetTodoItemsByListIdAsync(ResultType scenario, int listId)
    {
        TestData.CurrentScenario = scenario;
        string uri = $"api/TodoItems/GetTodoItemsByListId/{listId}";
        await TestPlatform.Client.GetAsync(uri);
        return TestResults.GetTodoItemsByListId;
    }

    private static async Task<bool> AddTodoItemAsync(ResultType scenario, TodoItemViewModel item)
    {
        TestData.CurrentScenario = scenario;
        string uri = "api/TodoItems/AddTodoItem";
        await TestPlatform.PostAsync(item, uri);
        return TestResults.AddTodoItem;
    }

    private static async Task<bool> UpdateTodoItemAsync(ResultType scenario, TodoItemViewModel item)
    {
        TestData.CurrentScenario = scenario;
        string uri = "api/TodoItems/UpdateTodoItem";
        await TestPlatform.PutAsync(item, uri);
        return TestResults.UpdateTodoItem;
    }

    private static async Task<bool> DeleteTodoItemAsync(ResultType scenario, int id)
    {
        TestData.CurrentScenario = scenario;
        string uri = $"api/TodoItems/DeleteTodoItem/{id}";
        await TestPlatform.Client.GetAsync(uri);
        return TestResults.DeleteTodoItem;
    }
}