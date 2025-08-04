namespace Todo.Tests.Mocks;
public static class Testresults
{
	static TestResults()
	{

	}

	public static Dictionary<string, ResultType> DataSets { get; set; } = new();
	public static readonly Dictionary<(string, ResultType), object> PredefinedData = new();
	public static Dictionary<string, object> Results = new();


}
