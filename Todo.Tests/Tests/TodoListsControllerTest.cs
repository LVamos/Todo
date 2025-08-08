using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

using NUnit.Framework;

using Todo.Tests.Mocks;
using Todo.ViewModels.ViewModels;

namespace Todo.Tests.Tests;
[TestFixture]
public class TodoListsControllerTest
{
    [Test]
    public async Task GetAllTodoListsTestValid()
    {
        TestData.CurrentScenario = ResultType.Valid;
        String uri = "api/TodoLists/GetAllTodoLists";
        await TestPlatform.Client.GetAsync(uri);
        TodoListsViewModel result = TestResults.GetAllTodoLists;
    Assert.That(result, Is.Not.Null);
        Assert.That(result.Lists, Is.Not.Null);
        Assert.That(result.Lists.Count, Is.GreaterThan(1));
        Assert.That(result.Lists[0].Name, Is.EqualTo("Shopping"));
        Assert.That(result.Lists.Any(l => l.Id == 2), Is.True);
    }

    [Test]
    public async Task GetAllTodoListsTestInvalid()
    {
        TestData.CurrentScenario = ResultType.Invalid;
        String uri = "api/TodoLists/GetAllTodoLists";
        await TestPlatform.Client.GetAsync(uri);
        TodoListsViewModel result = TestResults.GetAllTodoLists;
        Assert.That(result, Is.Null);
    }

}
