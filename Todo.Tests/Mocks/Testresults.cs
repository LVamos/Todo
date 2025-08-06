using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Todo.ViewModels.ViewModels;

namespace Todo.Tests.Mocks;
public static class TestResults
{
    public static bool? TodoListExists;
    public static TodoListsViewModel GetAllTodoLists;
    public static TodoListViewModel? GetTodoListById;
    public static bool AddTodoList;
    public static bool UpdateTodoList;
    public static bool DeleteTodoList;
    public static TodoItemsViewModel GetTodoItemsByListId;
    public static TodoItemViewModel? GetTodoItemById;
    public static bool AddTodoItem;
    public static bool UpdateTodoItem;
    public static Boolean DeleteTodoItem;
}
