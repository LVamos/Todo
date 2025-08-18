using Todo.Contracts.Contracts.Requests;
using Todo.Contracts.Contracts.Responses;
using Todo.Entities.Entities;
using Todo.Services.Abstraction.DatabaseServices;


namespace Todo.Tests.Mocks;

public class TestDatabaseService : IDatabaseService
{
    public Task<bool> TodoListExistsAsync(string listName)
    {
        bool? result = TestData.GetData(TestData.TodoListExistsResult);
        TestResults.TodoListExists = result;
        if (result == null)
            throw new ArgumentException(nameof(listName));
        return Task.FromResult(result.Value);
    }

    public Task<List<TodoList>> GetAllTodoListsAsync()
    {
        List<TodoList> result = TestData.GetData(TestData.GetAllTodoListsResults);
        TestResults.GetAllTodoLists = result;
        return Task.FromResult(result);
    }

    public Task<TodoList?> GetTodoListByIdAsync(int id)
    {
        TodoList result = TestData.GetData(TestData.GetTodoListByIdResult);
        TestResults.GetTodoListById = result;
        if (result == null)
            throw new InvalidOperationException();
        return Task.FromResult<TodoList?>(result);
    }

    public Task AddTodoListAsync(TodoList todoList)
    {
        bool result = TestData.GetData(TestData.AddTodoListResult);
        TestResults.AddTodoList = result;
        if (!result)
            throw new InvalidOperationException();
        return Task.CompletedTask;
    }

    public Task UpdateTodoListAsync(TodoList list)
    {
        bool result = TestData.GetData(TestData.UpdateTodoListResult);
        TestResults.UpdateTodoList = result;
        if (!result)
            throw new InvalidOperationException();
        return Task.CompletedTask;
    }

    public Task DeleteTodoListAsync(int id)
    {
        bool result = TestData.GetData(TestData.DeleteTodoListResult);
        TestResults.DeleteTodoList = result;
        if (!result)
            throw new InvalidOperationException();
        return Task.CompletedTask;
    }

    public Task<List<TodoItem>> GetTodoItemsByListIdAsync(int listId)
    {
        List<TodoItem> result = TestData.GetData(TestData.GetTodoItemsByListIdResult);
        TestResults.GetTodoItemsByListId = result;
        if (result == null)
            throw new ArgumentException(nameof(listId));
        return Task.FromResult(result);
    }

    public Task<TodoItem?> GetTodoItemByIdAsync(int id)
    {
        TodoItem result = TestData.GetData(TestData.GetTodoItemByIdResult);
        TestResults.GetTodoItemById = result;
        if (result == null)
            throw new ArgumentException(nameof(id));
        return Task.FromResult<TodoItem?>(result);
    }

    public Task AddTodoItemAsync(TodoItem item)
    {
        bool result = TestData.GetData(TestData.AddTodoItemResult);
        TestResults.AddTodoItem = result;
        if (!result)
            throw new ArgumentException(nameof(item));
        return Task.CompletedTask;
    }

    public Task UpdateTodoItemAsync(TodoItem item)
    {
        bool result = TestData.GetData(TestData.UpdateTodoItemResult);
        TestResults.UpdateTodoItem = result;
        if (!result)
            throw new ArgumentException(nameof(item));
        return Task.CompletedTask;
    }

    public Task DeleteTodoItemAsync(int id)
    {
        bool result = TestData.GetData(TestData.DeleteTodoItemResult);
        TestResults.DeleteTodoItem = result;
        if (!result)
            throw new ArgumentException(nameof(id));
        return Task.CompletedTask;
    }
}
