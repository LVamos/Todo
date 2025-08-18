using NUnit.Framework;

using System;
using System.Linq;
using System.Threading.Tasks;

using Todo.Contracts.Contracts.Requests;
using Todo.Contracts.Contracts.Responses;
using Todo.Entities.Entities;
using Todo.Tests.Mocks;


namespace Todo.Tests.Tests;
[TestFixture]
public class TodoItemsControllerTests
{
    [Test]
    public async Task GetTodoItemByIdTestValid()
    {
        int id = 1;
        TodoItem result = await GetTodoItemByIdAsync(ResultType.Valid, id);
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo("Připravit prezentaci"));
        Assert.That(result.Id, Is.EqualTo(1));
    }

    [Test]
    public async Task GetTodoItemByIdTestLimit()
    {
        int id = int.MaxValue;
        TodoItem result = await GetTodoItemByIdAsync(ResultType.Limit, id);
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo("Testovací úkol s maximálním ID"));
        Assert.That(result.Id, Is.EqualTo(int.MaxValue));
    }

    [Test]
    public async Task GetTodoItemByIdTestInvalid()
    {
        int id = -1;
        TodoItem result = await GetTodoItemByIdAsync(ResultType.Invalid, id);
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetTodoItemsByListIdTestValid()
    {
        int id = 1;
        List<TodoItem> result = await GetTodoItemsByListIdAsync(ResultType.Valid, id);
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result.Any(i => i.Id == 1), Is.True);
    }

    [Test]
    public async Task GetTodoItemsByListIdTestLimit()
    {
        int id = 1;
        List<TodoItem> result = await GetTodoItemsByListIdAsync(ResultType.Limit, id);
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(1000));
        String firstTitle = result.First().Title;
        Assert.That(firstTitle, Is.EqualTo("Úkol 1"));
    }

    [Test]
    public async Task GetTodoItemsByListIdTestInvalid()
    {
        int id = -1;
        List<TodoItem> result = await GetTodoItemsByListIdAsync(ResultType.Invalid, id);
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task AddTodoItemTestValid()
    {
        AddTodoItemRequest item = new()
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
        AddTodoItemRequest item = new();
        bool result = await AddTodoItemAsync(ResultType.Invalid, item);
        Assert.That(result, Is.False);
    }

    [Test]
    public async Task UpdateTodoItemTestValid()
    {
        UpdateTodoItemRequest item = new()
        {
            Title = "Updated Item",
            TodoListId = 1
        };
        bool result = await UpdateTodoItemAsync(ResultType.Valid, item);
        Assert.That(result, Is.True);
    }

    [Test]
    public async Task UpdateTodoItemTestInvalid()
    {
        UpdateTodoItemRequest item = new();
        bool result = await UpdateTodoItemAsync(ResultType.Invalid, item);
        Assert.That(result, Is.False);
    }

    [Test]
    public async Task DeleteTodoItemTestValid()
    {
        IdRequest id = new() { Id = 1 };
        bool result = await DeleteTodoItemAsync(ResultType.Valid, id);
        Assert.That(result, Is.True);
    }

    [Test]
    public async Task DeleteTodoItemTestInvalid()
    {
        IdRequest id = new() { Id = -1 };
        bool result = await DeleteTodoItemAsync(ResultType.Invalid, id);
        Assert.That(result, Is.False);
    }

    private static async Task<TodoItem> GetTodoItemByIdAsync(ResultType scenario, int id)
    {
        TestResults.GetTodoItemById = null;
        TestData.CurrentScenario = scenario;
        string uri = $"api/TodoItems/GetTodoItemById/{id}";
        await TestPlatform.Client.GetAsync(uri);
        return TestResults.GetTodoItemById;
    }

    private static async Task<List<TodoItem>> GetTodoItemsByListIdAsync(ResultType scenario, int listId)
    {
        TestResults.GetTodoItemsByListId = null;
        TestData.CurrentScenario = scenario;
        string uri = $"api/TodoItems/GetTodoItemsByListId/{listId}";
        await TestPlatform.Client.GetAsync(uri);
        return TestResults.GetTodoItemsByListId;
    }

    private static async Task<bool> AddTodoItemAsync(ResultType scenario, AddTodoItemRequest item)
    {
        TestResults.AddTodoItem = false;
        TestData.CurrentScenario = scenario;
        string uri = "api/TodoItems/AddTodoItem";
        HttpResponseMessage response =await TestPlatform.PostAsync(item, uri);
        return TestResults.AddTodoItem;
    }

    private static async Task<bool> UpdateTodoItemAsync(ResultType scenario, UpdateTodoItemRequest item)
    {
        TestResults.UpdateTodoItem = false;
        TestData.CurrentScenario = scenario;
        string uri = "api/TodoItems/UpdateTodoItem";
        await TestPlatform.PutAsync(item, uri);
        return TestResults.UpdateTodoItem;
    }

    private static async Task<bool> DeleteTodoItemAsync(ResultType scenario, IdRequest id)
    {
        TestResults.DeleteTodoItem = false;
        TestData.CurrentScenario = scenario;
        string uri = $"api/TodoItems/DeleteTodoItem/{id.Id}";
        await TestPlatform.Client.DeleteAsync(uri);
        return TestResults.DeleteTodoItem;
    }
}