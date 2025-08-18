using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Todo.Contracts.Contracts.Responses;
using Todo.Entities.Entities;


namespace Todo.Tests.Mocks;
public static class TestResults
{
    public static bool? TodoListExists;
    public static List<TodoList> GetAllTodoLists;
    public static TodoList? GetTodoListById;
    public static bool AddTodoList;
    public static bool UpdateTodoList;
    public static bool DeleteTodoList;
    public static List<TodoItem> GetTodoItemsByListId;
    public static TodoItem? GetTodoItemById;
    public static bool AddTodoItem;
    public static bool UpdateTodoItem;
    public static Boolean DeleteTodoItem;
}
