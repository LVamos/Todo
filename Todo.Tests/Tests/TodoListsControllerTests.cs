using Azure.Core;

using Newtonsoft.Json;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

using Todo.Contracts.Contracts.Requests;
using Todo.Contracts.Contracts.Responses;
using Todo.Tests.Mocks;


namespace Todo.Tests.Tests;
[TestFixture]
public class TodoListsControllerTests
{
    [Test]
    public async Task GetTodoListByIdTestValid()
    {
        IdRequest id = new() { Id = 1 }; 
        TodoListResponse result = await GetTodoListByIdAsync(ResultType.Valid, id);
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Pracovní úkoly"));
        Assert.That(result.Id, Is.EqualTo(1));
    }

    [Test]
    public async Task GetTodoListByIdTestLimit()
    {
        IdRequest id = new() { Id =int.MaxValue};
        TodoListResponse result = await GetTodoListByIdAsync(ResultType.Limit, id);
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Maximální ID seznam"));
        Assert.That(result.Id, Is.EqualTo(int.MaxValue));
    }

    [Test]
    public async Task GetTodoListByIdTestInvalid()
    {
        IdRequest id = new() { Id =-1};
        TodoListResponse result = await GetTodoListByIdAsync(ResultType.Invalid, id);
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task UpdateTodoListTestValid()
    {
        UpdateTodoListRequest list = new()
        {
            Id = 1,
            Name = "Updated List"
        };
        bool result = await UpdateTodoListAsync(ResultType.Valid, list);
        Assert.That(result, Is.True);
    }

    [Test]
    public async Task UpdateTodoListTestLimit()
    {
        UpdateTodoListRequest list = new()
        {
            Id = int.MaxValue,
            Name = "Limit List"
        };
        bool result = await UpdateTodoListAsync(ResultType.Limit, list);
        Assert.That(result, Is.True);
    }

    [Test]
    public async Task UpdateTodoListTestInvalid()
    {
        UpdateTodoListRequest list = new();
        bool result = await UpdateTodoListAsync(ResultType.Invalid, list);
        Assert.That(result, Is.False);
    }

    [Test]
    public async Task DeleteTodoListTestValid()
    {
        IdRequest id = new() { Id =1};
        bool result = await DeleteTodoListAsync(ResultType.Valid, id);
        Assert.That(result, Is.True);
    }

    [Test]
    public async Task DeleteTodoListTestLimit()
    {
        IdRequest id = new() { Id = int.MaxValue };
        bool result = await DeleteTodoListAsync(ResultType.Limit, id);
        Assert.That(result, Is.True);
    }

    [Test]
    public async Task DeleteTodoListTestInvalid()
    {
        IdRequest id = new() { Id = -1 };
        bool result = await DeleteTodoListAsync(ResultType.Invalid, id);
        Assert.That(result, Is.False);
    }

    private async Task<TodoListResponse> GetTodoListByIdAsync(ResultType scenario, IdRequest id)
    {
        TestResults.GetTodoListById = null;
        TestResults.GetTodoListById = null;
        TestData.CurrentScenario = scenario;
        String uri = $"api/TodoLists/GetTodoListById/{id.Id}";
        await TestPlatform.Client.GetAsync(uri);
        return TestResults.GetTodoListById;
    }

    private async Task<bool> UpdateTodoListAsync(ResultType scenario, UpdateTodoListRequest list)
    {
        TestResults.UpdateTodoList = false;
        TestData.CurrentScenario = scenario;
        String uri = "api/TodoLists/UpdateTodoList";
        await TestPlatform.PutAsync(list, uri);
        return TestResults.UpdateTodoList;
    }

    private async Task<bool> DeleteTodoListAsync(ResultType scenario, IdRequest id)
    {
        TestResults.DeleteTodoList = false;
        TestData.CurrentScenario = scenario;
        String uri = $"api/TodoLists/DeleteTodoList/{id.Id}";
        await TestPlatform.Client.GetAsync(uri);
        return TestResults.DeleteTodoList;
    }

    [Test]
    public async Task AddTodoListTestLimit()
    {
        AddTodoListRequest list = new()
        {
            Name = "slepice"
        };
        bool result = await AddTodoListAsync(ResultType.Limit, list);
        Assert.That(result, Is.True);
    }

    [Test]
    public async Task AddTodoListTestInvalid()
    {
        AddTodoListRequest list = new();
        bool result = await AddTodoListAsync(ResultType.Invalid, list);
        Assert.That(result, Is.False);
    }

    [Test]
    public async Task AddTodoListTestValid()
    {
        AddTodoListRequest list = new()
        {
            Name = "slepice"
        };
        bool result = await AddTodoListAsync(ResultType.Valid, list);
        Assert.That(result, Is.True);
    }


    [Test]
    public async Task GetAllTodoListsTestLimit()
    {
        TodoListsResponse result = await GetAllTodoListsAsync(ResultType.Limit); 
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Lists, Is.Not.Null);
        Assert.That(result.Lists.Count, Is.EqualTo(1000));
        Assert.That(result.Lists[0].Name, Is.EqualTo("Seznam 1"));
        Assert.That(result.Lists.Any(l => l.Id == 2), Is.True);
    }

    private static async Task<TodoListsResponse> GetAllTodoListsAsync(ResultType scenario)
    {
        TestResults.GetAllTodoLists = null;
        TestData.CurrentScenario = scenario;
        String uri = "api/TodoLists/GetAllTodoLists";
        await TestPlatform.Client.GetAsync(uri);
        return TestResults.GetAllTodoLists;
    }

    private static async Task<bool> AddTodoListAsync(ResultType scenario, AddTodoListRequest list)
    {
        TestResults.AddTodoList = false;
        TestData.CurrentScenario = scenario;
        String uri = "api/TodoLists/AddTodoList";
        await TestPlatform.PostAsync(list, uri);
        return TestResults.AddTodoList;
    }

    [Test]
    public async Task GetAllTodoListsTestValid()
    {
        TodoListsResponse result = await GetAllTodoListsAsync(ResultType.Valid);
        Assert.That(result.Lists, Is.Not.Null);
        Assert.That(result.Lists.Count, Is.GreaterThan(1));
        Assert.That(result.Lists[0].Name, Is.EqualTo("Shopping"));
        Assert.That(result.Lists.Any(l => l.Id == 2), Is.True);
    }

    [Test]
    public async Task GetAllTodoListsTestInvalid()
    {
        TodoListsResponse result =await GetAllTodoListsAsync(ResultType.Invalid);
        Assert.That(result, Is.Null);
    }

}
