using Todo.Dal.Context;

namespace Todo.Dal.Abstraction;

public interface IContextFactory
{
	TodoDbContext GetDbContext();
}
