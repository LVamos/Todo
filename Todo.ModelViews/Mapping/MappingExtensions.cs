using Todo.Contracts.Contracts.Requests;
using Todo.Contracts.Contracts.Responses;
using Todo.Entities.Entities;


namespace Todo.ViewModels.Mapping
{
	public static class MappingExtensions
	{
		/// <summary>
		/// Converts a list of TodoList entities to a list of TodoListViewModels.
		/// </summary>
		public static TodoListsResponse ToResponses(this List<TodoList> entities)
		{
			if (entities == null) return null;
			return new TodoListsResponse()
			{
				Lists = entities.Select(e => e.ToResponse()).ToList()
			};
		}

		/// <summary>
		/// Converts a list of TodoListViewModels to a list of TodoList entities.
		/// </summary>
		public static List<TodoList> ToEntities(this List<TodoListRequest> request)
		{
			if (request == null) return new List<TodoList>();
			return request.Select(vm => vm.ToEntity()).ToList();
		}

		/// <summary>
		/// Converts TodoItem entity to TodoItemViewModel.
		/// </summary>
		public static TodoItemResponse ToResponse(this TodoItem entity)
		{
			if (entity == null) return null;

			return new ()
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
		public static TodoItem ToEntity(this TodoItemRequest request)
		{
			if (request == null) return null;

			return new TodoItem
			{
				Id = request.Id,
				Title = request.Title,
				IsCompleted = request.IsCompleted,
				Priority = request.Priority,
				Deadline = request.Deadline,
				Comment = request.Comment,
				TodoListId = request.TodoListId
			};
		}

		/// <summary>
		/// Converts TodoList entity to TodoListViewModel.
		/// </summary>
		public static TodoListResponse ToResponse(this TodoList entity)
		{
			if (entity == null) return null;

            return new()
            {
                Id = entity.Id,
                Name = entity.Name,
                Items = entity.Items.Select(e => e.ToResponse())
                .ToList()
            };
		}

		/// <summary>
		/// Converts TodoListViewModel to TodoList entity.
		/// </summary>
		public static TodoList ToEntity(this TodoListRequest request)
		{
			if (request == null) return null;

			return new TodoList
			{
				Id = request.Id,
				Name = request.Name,
				Items = request.Items.ToEntities().ToList()
			};
		}

		/// <summary>
		/// Converts a collection of TodoItem entities to a list of TodoItemViewModels.
		/// </summary>
		public static TodoItemsResponse ToResponse(this IEnumerable<TodoItem> entities)
		{
			if (entities == null)
				return null;

            return new()
            {
                Items =
                entities.Select(e => e.ToResponse())
            };
		}

		/// <summary>
		/// Converts a collection of TodoItemViewModels to a list of TodoItem entities.
		/// </summary>
		public static IEnumerable<TodoItem> ToEntities(this IEnumerable<TodoItemRequest> requests)
		{
			if (requests == null) return Enumerable.Empty<TodoItem>();
			return requests.Select(vm => vm.ToEntity());
		}
	}
}
