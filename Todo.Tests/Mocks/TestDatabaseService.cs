using Todo.Contracts.Contracts.Requests;
using Todo.Contracts.Contracts.Responses;
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

    public Task<TodoListsResponse> GetAllTodoListsAsync()
    {
        TodoListsResponse result = TestData.GetData(TestData.GetAllTodoListsResults);
        TestResults.GetAllTodoLists = result;
        return Task.FromResult(result);
    }

    public Task<TodoListResponse?> GetTodoListByIdAsync(IdRequest id)
    {
        TodoListResponse result = TestData.GetData(TestData.GetTodoListByIdResult);
        TestResults.GetTodoListById = result;
        if (result == null)
            throw new InvalidOperationException();
        return Task.FromResult<TodoListResponse?>(result);
    }

    public Task AddTodoListAsync(AddTodoListRequest todoList)
    {
        bool result = TestData.GetData(TestData.AddTodoListResult);
        TestResults.AddTodoList = result;
        if (!result)
            throw new InvalidOperationException();
        return Task.CompletedTask;
    }

    public Task UpdateTodoListAsync(UpdateTodoListRequest list)
    {
        bool result = TestData.GetData(TestData.UpdateTodoListResult);
        TestResults.UpdateTodoList = result;
        if (!result)
            throw new InvalidOperationException();
        return Task.CompletedTask;
    }

    public Task DeleteTodoListAsync(IdRequest id)
    {
        bool result = TestData.GetData(TestData.DeleteTodoListResult);
        TestResults.DeleteTodoList = result;
        if (!result)
            throw new InvalidOperationException();
        return Task.CompletedTask;
    }

    public Task<TodoItemsResponse> GetTodoItemsByListIdAsync(IdRequest listId)
    {
        TodoItemsResponse result = TestData.GetData(TestData.GetTodoItemsByListIdResult);
        TestResults.GetTodoItemsByListId = result;
        if (result == null)
            throw new ArgumentException(nameof(listId));
        return Task.FromResult(result);
    }

    public Task<TodoItemResponse?> GetTodoItemByIdAsync(IdRequest id)
    {
        TodoItemResponse result = TestData.GetData(TestData.GetTodoItemByIdResult);
        TestResults.GetTodoItemById = result;
        if (result == null)
            throw new ArgumentException(nameof(id));
        return Task.FromResult<TodoItemResponse?>(result);
    }

    public Task AddTodoItemAsync(AddTodoItemRequest item)
    {
        bool result = TestData.GetData(TestData.AddTodoItemResult);
        TestResults.AddTodoItem = result;
        if (!result)
            throw new ArgumentException(nameof(item));
        return Task.CompletedTask;
    }

    public Task UpdateTodoItemAsync(UpdateTodoItemRequest item)
    {
        bool result = TestData.GetData(TestData.UpdateTodoItemResult);
        TestResults.UpdateTodoItem = result;
        if (!result)
            throw new ArgumentException(nameof(item));
        return Task.CompletedTask;
    }

    public Task DeleteTodoItemAsync(IdRequest id)
    {
        bool result = TestData.GetData(TestData.DeleteTodoItemResult);
        TestResults.DeleteTodoItem = result;
        if (!result)
            throw new ArgumentException(nameof(id));
        return Task.CompletedTask;
    }
}
