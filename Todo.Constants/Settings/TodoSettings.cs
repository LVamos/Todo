namespace Todo.Constants.Settings;

public class TodoSettings
{
    public string ApiKey { get; set; }

	/// <summary>
	///     Name of corresponding section in appsettings.json.
	/// </summary>
	public const string SectionName = "TodoSettings";
}
