namespace Todo.Tests.Mocks;
public class TestScenario<T>
{
	public T this[ResultType key]
	{
		get => _data.TryGetValue(key, out var val) ? val : default!;
		set => _data[key] = value;
	}

	private readonly Dictionary<ResultType, T> _data = new();

	public void Add(ResultType type, T data) => _data[type] = data;
	public T Get(ResultType type) => _data.TryGetValue(type, out var val) ? val : default!;
}