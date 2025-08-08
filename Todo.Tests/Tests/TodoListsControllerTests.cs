using Azure.Core;

using Newtonsoft.Json;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

using Todo.Tests.Mocks;
using Todo.ViewModels.ViewModels;

namespace Todo.Tests.Tests;
[TestFixture]
public class TodoListsControllerTests
{
    [Test]
    public async Task AddTodoListTestLimit()
    {
        TodoListViewModel list = new()
        {
            Name = "slepice"
        };
        bool result = await AddTodoListAsync(ResultType.Limit, list);
        Assert.That(result, Is.True);
    }

    [Test]
    public async Task AddTodoListTestInvalid()
    {
        TodoListViewModel list = new();
        bool result = await AddTodoListAsync(ResultType.Invalid, list);
        Assert.That(result, Is.False);
    }

    [Test]
    public async Task AddTodoListTestValid()
    {
        TodoListViewModel list = new()
        {
            Name = "slepice"
        };
        bool result = await AddTodoListAsync(ResultType.Valid, list);
        Assert.That(result, Is.True);
    }


    [Test]
    public async Task GetAllTodoListsTestLimit()
    {
        TodoListsViewModel result = await GetAllTodoListsAsync(ResultType.Limit); 
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Lists, Is.Not.Null);
        Assert.That(result.Lists.Count, Is.EqualTo(1000));
        Assert.That(result.Lists[0].Name, Is.EqualTo("Seznam 1"));
        Assert.That(result.Lists.Any(l => l.Id == 2), Is.True);
    }

    private static async Task<TodoListsViewModel> GetAllTodoListsAsync(ResultType scenario)
    {
        TestData.CurrentScenario = scenario;
        String uri = "api/TodoLists/GetAllTodoLists";
        await TestPlatform.Client.GetAsync(uri);
        TodoListsViewModel result = TestResults.GetAllTodoLists;
        return result;
    }

    private static async Task<bool> AddTodoListAsync(ResultType scenario, TodoListViewModel list)
    {
        TestData.CurrentScenario = scenario;
        String uri = "api/TodoLists/AddTodoList";
        await TestPlatform.PostAsync(list, uri);
        return TestResults.AddTodoList;
    }

    [Test]
    public async Task GetAllTodoListsTestValid()
    {
        TodoListsViewModel result = await GetAllTodoListsAsync(ResultType.Valid);
        Assert.That(result.Lists, Is.Not.Null);
        Assert.That(result.Lists.Count, Is.GreaterThan(1));
        Assert.That(result.Lists[0].Name, Is.EqualTo("Shopping"));
        Assert.That(result.Lists.Any(l => l.Id == 2), Is.True);
    }

    [Test]
    public async Task GetAllTodoListsTestInvalid()
    {
        TodoListsViewModel result =await GetAllTodoListsAsync(ResultType.Invalid);
        Assert.That(result, Is.Null);
    }

}
