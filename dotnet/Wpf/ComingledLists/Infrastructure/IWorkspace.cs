namespace Infrastructure
{
	public interface IWorkspace
	{
		void AddItem(object item);
		void AddResource(string key, object resource);
		void OutputMessage(string message);
	}
}
