using Todo.Entities.Entities;
using Todo.ViewModels.ViewModels;

namespace Todo.Tests.Mocks;
public static class TestData
{
    public static TestScenario<bool> DeleteTodoItemResult { get; } = new()
    {
        [ResultType.Valid] = true,
        [ResultType.Limit] = true,
        [ResultType.Invalid] = false
    };


    public static TestScenario<bool> UpdateTodoItemResult { get; } = new()
    {
        [ResultType.Valid] = true,
        [ResultType.Limit] = true,
        [ResultType.Invalid] = false
    };


    public static TestScenario<bool> AddTodoItemResult { get; } = new()
    {
        [ResultType.Valid] = true,
        [ResultType.Limit] = true,
        [ResultType.Invalid] = false
    };


    public static TestScenario<TodoItemViewModel?> GetTodoItemByIdResult { get; } = new()
    {
        [ResultType.Valid] = new TodoItemViewModel
        {
            Id = 1,
            Title = "Připravit prezentaci",
            IsCompleted = false,
            Priority = Priority.High,
            Deadline = DateTime.Today.AddDays(1),
            Comment = "Zaměřit se na důležité body",
            TodoListId = 1
        },
        [ResultType.Limit] = new TodoItemViewModel
        {
            Id = int.MaxValue,
            Title = "Testovací úkol s maximálním ID",
            IsCompleted = false,
            Priority = Priority.Medium,
            Deadline = null,
            Comment = "Hraniční hodnota ID",
            TodoListId = 999
        },
        [ResultType.Invalid] = null
    };


    public static TestScenario<TodoItemsViewModel> GetTodoItemsByListIdResult { get; } = new()
	{
		[ResultType.Valid] = new TodoItemsViewModel
		{
			Items = new List<TodoItemViewModel>
		{
			new()
			{
				Id = 1,
				Title = "Napsat testy",
				IsCompleted = false,
				Priority = Priority.Medium,
				Deadline = DateTime.Today.AddDays(3),
				Comment = "Zaměřit se na scénáře s výjimkami",
				TodoListId = 1
			},
			new()
			{
				Id = 2,
				Title = "Zkontrolovat UI",
				IsCompleted = true,
				Priority = Priority.Low,
				Deadline = null,
				Comment = "",
				TodoListId = 1
			}
		}
		},
		[ResultType.Limit] = new TodoItemsViewModel
		{
			Items = Enumerable.Range(1, 1000).Select(i => new TodoItemViewModel
			{
				Id = i,
				Title = $"Úkol {i}",
				IsCompleted = i % 2 == 0,
				Priority = Priority.Low,
				Deadline = null,
				Comment = $"Komentář k úkolu {i}",
				TodoListId = 1
			}).ToList()
		},
		[ResultType.Invalid] = null
	};


	public static TestScenario<bool> DeleteTodoListResult { get; } = new()
	{
		[ResultType.Valid] = true,
		[ResultType.Limit] = true,
		[ResultType.Invalid] = false
	};


	public static TestScenario<bool> UpdateTodoListResult { get; } = new()
	{
		[ResultType.Valid] = true,
		[ResultType.Limit] = true,
		[ResultType.Invalid] = false
	};


	public static TestScenario<bool> AddTodoListResult { get; } = new()
	{
		[ResultType.Valid] = true,
		[ResultType.Limit] = true,
		[ResultType.Invalid] = false
	};


	public static TestScenario<TodoListViewModel?> GetTodoListByIdResult { get; } = new()
	{
		[ResultType.Valid] = new TodoListViewModel
		{
			Id = 1,
			Name = "Pracovní úkoly"
		},

		[ResultType.Limit] = new TodoListViewModel
		{
			Id = int.MaxValue,
			Name = "Maximální ID seznam"
		},

		[ResultType.Invalid] = null
	};

	public static TestScenario<TodoListsViewModel> GetAllTodoListsResults { get; } = new()
	{
		[ResultType.Valid] = new TodoListsViewModel
		{
			Lists = new List<TodoListViewModel>
			{
				new TodoListViewModel { Id = 1, Name = "Shopping" },
				new TodoListViewModel { Id = 2, Name = "Work" }
			}
		},
		[ResultType.Limit] = new TodoListsViewModel
		{
			Lists = Enumerable.Range(1, 1000).Select(i => new TodoListViewModel
			{
				Id = i,
				Name = $"Seznam {i}"
			}).ToList()
		},
		[ResultType.Invalid] = null
	};

	public static TestScenario<bool?> TodoListExistsResult { get; } = new()
	{
		[ResultType.Valid] = true,
		[ResultType.Limit] = true,
		[ResultType.Invalid] = null
	};

	public static ResultType CurrentScenario = ResultType.Valid;
	public static T GetData<T>(TestScenario<T> scenario) => scenario.Get(CurrentScenario);
}
