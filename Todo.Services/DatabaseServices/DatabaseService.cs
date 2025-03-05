using Todo.Dal.Abstraction;
using Todo.Services.Abstraction.DatabaseServices;

namespace Todo.Services.DatabaseServices;

public class DatabaseService : IDatabaseService
{
	public DatabaseService(IContextFactory contextFactory)
	{
		_contextFactory = contextFactory;
	}

	private IContextFactory _contextFactory;
}