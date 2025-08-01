using Todo.Entities.Entities;
using Todo.ViewModels.ViewModels;

namespace Todo.ViewModels.Mapping
{
	public static class MappingExtensions
	{
		/// <summary>
		/// Converts a list of TodoList entities to a list of TodoListViewModels.
		/// </summary>
		public static List<TodoListViewModel> ToViewModels(this List<TodoList> entities)
		{
			if (entities == null) return new List<TodoListViewModel>();
			return entities.Select(e => e.ToViewModel()).ToList();
		}

		/// <summary>
		/// Converts a list of TodoListViewModels to a list of TodoList entities.
		/// </summary>
		public static List<TodoList> ToEntities(this List<TodoListViewModel> viewModels)
		{
			if (viewModels == null) return new List<TodoList>();
			return viewModels.Select(vm => vm.ToEntity()).ToList();
		}

		/// <summary>
		/// Converts TodoItem entity to TodoItemViewModel.
		/// </summary>
		public static TodoItemViewModel ToViewModel(this TodoItem entity)
		{
			if (entity == null) return null;

			return new TodoItemViewModel
			{
				Id = entity.Id,
				Title = entity.Title,
				IsCompleted = entity.IsCompleted,
				Priority = entity.Priority,
				Deadline = entity.Deadline,
				Comment = entity.Comment,
				TodoListId = entity.TodoListId
			};
		}

		/// <summary>
		/// Converts TodoItemViewModel to TodoItem entity.
		/// </summary>
		public static TodoItem ToEntity(this TodoItemViewModel viewModel)
		{
			if (viewModel == null) return null;

			return new TodoItem
			{
				Id = viewModel.Id,
				Title = viewModel.Title,
				IsCompleted = viewModel.IsCompleted,
				Priority = viewModel.Priority,
				Deadline = viewModel.Deadline,
				Comment = viewModel.Comment,
				TodoListId = viewModel.TodoListId
			};
		}

		/// <summary>
		/// Converts TodoList entity to TodoListViewModel.
		/// </summary>
		public static TodoListViewModel ToViewModel(this TodoList entity)
		{
			if (entity == null) return null;

			return new TodoListViewModel
			{
				Id = entity.Id,
				Name = entity.Name,
				Items = entity.Items.ToViewModels()
				.ToList()
			};
		}

		/// <summary>
		/// Converts TodoListViewModel to TodoList entity.
		/// </summary>
		public static TodoList ToEntity(this TodoListViewModel viewModel)
		{
			if (viewModel == null) return null;

			return new TodoList
			{
				Id = viewModel.Id,
				Name = viewModel.Name,
				Items = viewModel.Items.ToEntities().ToList()
			};
		}

		/// <summary>
		/// Converts a collection of TodoItem entities to a list of TodoItemViewModels.
		/// </summary>
		public static IEnumerable<TodoItemViewModel> ToViewModels(this IEnumerable<TodoItem> entities)
		{
			if (entities == null)
				return Enumerable.Empty<TodoItemViewModel>();

			return entities.Select(e => e.ToViewModel());
		}

		/// <summary>
		/// Converts a collection of TodoItem entities to a list of TodoItemViewModels.
		/// </summary>
		public static TodoItemsViewModel ToViewModel(this IEnumerable<TodoItem> entities)
		{
			if (entities == null)
				return null;
			return new()
			{
				Items = entities.Select(e => e.ToViewModel())
			};
		}

		/// <summary>
		/// Converts a collection of TodoItemViewModels to a list of TodoItem entities.
		/// </summary>
		public static IEnumerable<TodoItem> ToEntities(this IEnumerable<TodoItemViewModel> viewModels)
		{
			if (viewModels == null) return Enumerable.Empty<TodoItem>();
			return viewModels.Select(vm => vm.ToEntity());
		}
	}
}
