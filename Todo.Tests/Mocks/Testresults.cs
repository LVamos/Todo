using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Todo.Contracts.Contracts.Responses;


namespace Todo.Tests.Mocks;
public static class TestResults
{
    public static bool? TodoListExists;
    public static TodoListsResponse GetAllTodoLists;
    public static TodoListResponse? GetTodoListById;
    public static bool AddTodoList;
    public static bool UpdateTodoList;
    public static bool DeleteTodoList;
    public static TodoItemsResponse GetTodoItemsByListId;
    public static TodoItemResponse? GetTodoItemById;
    public static bool AddTodoItem;
    public static bool UpdateTodoItem;
    public static Boolean DeleteTodoItem;
}
