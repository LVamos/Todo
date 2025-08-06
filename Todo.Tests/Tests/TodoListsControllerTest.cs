using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using Todo.Tests.Mocks;
using Todo.ViewModels.ViewModels;

namespace Todo.Tests.Tests;
[TestFixture]
public class TodoListsControllerTest
{
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
