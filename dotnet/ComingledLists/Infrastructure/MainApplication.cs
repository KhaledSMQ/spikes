using System;

namespace Infrastructure
{
	public class MainApplication
	{
		public static readonly MainApplication Instance = new MainApplication();

		public IWorkspace Workspace { get; set; }

		public void Initialize()
		{
			// simulate loading modules from config
			var one = Activator.CreateInstance("ModuleOne", "ModuleOne.Module");
			var two = Activator.CreateInstance("ModuleTwo", "ModuleTwo.Module");
		}

		private MainApplication()
		{
		}
	}
}
