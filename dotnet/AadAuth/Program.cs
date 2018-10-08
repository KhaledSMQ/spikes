using System;
using System.Configuration;
using System.Security;
using System.Windows.Forms;

namespace AadAuth
{
	class Program
	{
		private static string Username { get; set; }
		private static SecureString Password { get; set; }
        private static string EnvironmentUrl { get; set; }

		[STAThread]
		static void Main(string[] args)
		{
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            /*ChooseEnvironment();
			var auth = RunAuthentication();
		    if (string.IsNullOrEmpty(auth.AccessToken))
		    {
		        Console.WriteLine("Error during authentication.");
		    }
		    else
		    {
		        var header = "Authorization: Bearer " + auth.AccessToken;
		        Clipboard.SetText(header);
		        Console.WriteLine("Copied authorization header to the clipboard.");
		    }
		    Console.WriteLine("Press Enter to exit.");
			Console.ReadLine();*/
		}
	}
}
