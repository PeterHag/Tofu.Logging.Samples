using System;
using System.Text;
using System.Threading;
using Tofu.Logging;
using Tofu.Logging.Monitors;

namespace TofuLogging06
{
	class Program
	{
		static void Main(string[] args)
		{
			// Start sample
			Console.WriteLine("Running TofuLogging06...");

			// Create a default manager but do not start yet!
			var logMgr = LogManager.CreateManager();

			// Change logging configuration by adding monitor that tracks memory consumption
			// Note: Monitors are basicall timers that track & log a single specific system value each time they elapse.
			//       LogMonitorProcess derived types track a specific value of the current Process.
			logMgr.Config.GlobalTrace = true;
			logMgr.Config.MonitorTypes.Add(new LogConfigMonitorType(typeof(LogMonitorMemoryWorkingSet), "enabled=true; interval=500"));

			/*
			// Code below also defines a monitor that will track same value as above, but now the monitor is defined by using its generic base class. 
			logMgr.Config.MonitorTypes.Add(new LogConfigMonitorType(
				typeof(LogMonitorProcess), 
				"enabled=true; interval=1000; memory=WorkingSet64"));
            */

			// Start logging session when config is initialized.
			logMgr.StartSession();

			// Create logger for messages
			var log = logMgr.GetLog("Buffer") as ILog4N;

			// Run a loop for 5 seconds to see  
			var buffer = new StringBuilder();
			var delta = string.Empty.PadRight(1024*1024, 'x');
			var start = DateTime.Now;
			while ((DateTime.Now.Ticks - start.Ticks) < 5 * TimeSpan.TicksPerSecond)
			{
				// Increment string buffer with 1MB and see if monitor tracking will show this memory increase
				buffer.Append(delta);
				log.DebugFormat("Buffer lenght: {0}", buffer.Length);

				// Dump progress on console
				Console.Write("|");

				// Throttle
				Thread.Sleep(100);
			}

			// Stop session (ie. stop the dedicated logging thread)
			logMgr.StopSession();

			// Show where logfiles are written
			Console.WriteLine();
			Console.WriteLine(string.Format("Log files: {0}", logMgr.SessionFolderFullName));
			Console.WriteLine("Press any key to stop...");
			Console.ReadKey();
		}
	}
}
