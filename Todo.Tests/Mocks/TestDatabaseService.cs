using Microsoft.EntityFrameworkCore.Query.Internal;

using Todo.Contracts.Contracts.Requests;
using Todo.Contracts.Contracts.Responses;
using Todo.Services.Abstraction.DatabaseServices;


namespace Todo.Tests.Mocks;

public class TestDatabaseService : IDatabaseService
{
    public async Task<bool> TodoListExistsAsync(string listName)
    {
        bool? result = TestData.GetData(TestData.TodoListExistsResult);
        TestResults.TodoListExists = result;
        if (result == null)
            throw new ArgumentException(nameof(listName));
        else return result.Value;
    }

    public async Task<TodoListsResponse> GetAllTodoListsAsync()
    {
        TodoListsResponse result = TestData.GetData(TestData.GetAllTodoListsResults);
        TestResults.GetAllTodoLists = result;
        return result;
    }

    public async Task<TodoListResponse?> GetTodoListByIdAsync(IdRequest id)
    {
        TodoListResponse result = TestData.GetData(TestData.GetTodoListByIdResult);
        TestResults.GetTodoListById = result;
        return result ?? throw new InvalidOperationException();
    }

    public async Task AddTodoListAsync(AddTodoListRequest todoList)
    {
        bool result = TestData.GetData(TestData.AddTodoListResult);
        TestResults.AddTodoList = result;
        if (!result)
            throw new InvalidOperationException();
    }

    public async Task UpdateTodoListAsync(UpdateTodoListRequest list)
    {
        bool result = TestData.GetData(TestData.UpdateTodoListResult);
        TestResults.UpdateTodoList = result;
        if (!result)
            throw new InvalidOperationException();
    }

    public async Task DeleteTodoListAsync(IdRequest id)
    {
        bool result = TestData.GetData(TestData.DeleteTodoListResult);
        TestResults.DeleteTodoList = result;
        if (!result)
            throw new InvalidOperationException();
    }

    public async Task<TodoItemsResponse> GetTodoItemsByListIdAsync(IdRequest listId)
    {
        TodoItemsResponse result = TestData.GetData(TestData.GetTodoItemsByListIdResult);
        TestResults.GetTodoItemsByListId = result;
        return result ?? throw new ArgumentException(nameof(listId));
    }

    public async Task<TodoItemResponse?> GetTodoItemByIdAsync(IdRequest id)
    {
        TodoItemResponse result = TestData.GetData(TestData.GetTodoItemByIdResult);
        TestResults.GetTodoItemById = result;
        return result ?? throw new ArgumentException(nameof(id));
    }

    public async Task AddTodoItemAsync(AddTodoItemRequest item)
    {
        bool result = TestData.GetData(TestData.AddTodoItemResult);
        TestResults.AddTodoItem = result;
        if (!result)
            throw new ArgumentException(nameof(item));
    }

    public async Task UpdateTodoItemAsync(UpdateTodoItemRequest item)
    {
        bool result = TestData.GetData(TestData.UpdateTodoItemResult);
        TestResults.UpdateTodoItem = result;
        if (!result)
            throw new ArgumentException(nameof(item));
    }

    public async Task DeleteTodoItemAsync(IdRequest id)
    {
        bool result = TestData.GetData(TestData.DeleteTodoItemResult);
        TestResults.DeleteTodoItem = result;
        if (!result)
            throw new ArgumentException(nameof(id));
    }
}
