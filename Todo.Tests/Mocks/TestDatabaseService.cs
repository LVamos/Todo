using Microsoft.EntityFrameworkCore.Query.Internal;

using Todo.Services.Abstraction.DatabaseServices;
using Todo.ViewModels.ViewModels;

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

    public async Task<TodoListsViewModel> GetAllTodoListsAsync()
    {
        TodoListsViewModel result = TestData.GetData(TestData.GetAllTodoListsResults);
        TestResults.GetAllTodoLists = result;
        return result;
    }

    public async Task<TodoListViewModel?> GetTodoListByIdAsync(int id)
    {
        TodoListViewModel result = TestData.GetData(TestData.GetTodoListByIdResult);
        TestResults.GetTodoListById = result;
        return result ?? throw new InvalidOperationException();
    }

    public async Task AddTodoListAsync(TodoListViewModel todoList)
    {
        bool result = TestData.GetData(TestData.AddTodoListResult);
        TestResults.AddTodoList = result;
        if (!result)
            throw new InvalidOperationException();
    }

    public async Task UpdateTodoListAsync(TodoListViewModel list)
    {
        bool result = TestData.GetData(TestData.UpdateTodoListResult);
        TestResults.UpdateTodoList = result;
        if (!result)
            throw new InvalidOperationException();
    }

    public async Task DeleteTodoListAsync(int id)
    {
        bool result = TestData.GetData(TestData.DeleteTodoListResult);
        TestResults.DeleteTodoList = result;
        if (!result)
            throw new InvalidOperationException();
    }

    public async Task<TodoItemsViewModel> GetTodoItemsByListIdAsync(int listId)
    {
        TodoItemsViewModel result = TestData.GetData(TestData.GetTodoItemsByListIdResult);
        TestResults.GetTodoItemsByListId = result;
        return result ?? throw new ArgumentException(nameof(listId));
    }

    public async Task<TodoItemViewModel?> GetTodoItemByIdAsync(int id)
    {
        TodoItemViewModel result = TestData.GetData(TestData.GetTodoItemByIdResult);
        TestResults.GetTodoItemById = result;
        return result ?? throw new ArgumentException(nameof(id));
    }

    public async Task AddTodoItemAsync(TodoItemViewModel item)
    {
        bool result = TestData.GetData(TestData.AddTodoItemResult);
        TestResults.AddTodoItem = result;
        if (!result)
            throw new ArgumentException(nameof(item));
    }

    public async Task UpdateTodoItemAsync(TodoItemViewModel item)
    {
        bool result = TestData.GetData(TestData.UpdateTodoItemResult);
        TestResults.UpdateTodoItem = result;
        if (!result)
            throw new ArgumentException(nameof(item));
    }

    public async Task DeleteTodoItemAsync(int id)
    {
        bool result = TestData.GetData(TestData.DeleteTodoItemResult);
        TestResults.DeleteTodoItem = result;
        if (!result)
            throw new ArgumentException(nameof(id));
    }
}
