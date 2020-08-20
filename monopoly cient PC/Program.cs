using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Threading;

namespace monopoly_cient_PC
{
	static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Queue<SendObject> jsonReader = new Queue<SendObject>();
			IPAddress serverAddress = IPAddress.Loopback;

			Connection serverConnection = new Connection(serverAddress);
			Thread dataReader = new Thread(() => serverConnection.DataReader(ref jsonReader))
			{
				Name = "DataReader"
			};
			Thread dataUpdater = new Thread(() => serverConnection.Updater(ref jsonReader))
			{
				Name = "DataUpdater"
			};
			dataReader.Start();
			dataUpdater.Start();

			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new OwnBalance(serverConnection));


		}
	}
}
