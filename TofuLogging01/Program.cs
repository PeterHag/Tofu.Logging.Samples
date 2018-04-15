using System;
using Tofu.Logging;

namespace TofuLogging01
{
	class Program
	{
		static void Main(string[] args)
		{
			// Start sample
			Console.WriteLine("Running TofuLogging01...");

			// Create a default manager and start logging session (ie. start dedicated logging thread)
			var logMgr = LogManager.CreateManager().StartSession();

			// Create logger with preference for Log4Net interface 
			var log = logMgr.GetLog("MyLog") as ILog4N;

			// Dump some messages
			log.Debug(() => "Debug entries have lowest level and are typically used to give technical context");
			log.Info(() => "Info entries are next on logging level and are used to give functional context");
			log.Warn(() => "Warnings are for when things get interesting because some expectations could not be fulfilled");
			log.Error(() => "Errors are for serious issues although it's still possible that the application will survive");
			log.Fatal(() => "Fatals you do not want to see because basically things are now out of control");

			// Stop session (ie. stop the dedicated logging thread)
			logMgr.StopSession();

			// Show where logfiles are written
			Console.WriteLine(string.Format("Log files: {0}", logMgr.SessionFolderFullName));
			Console.WriteLine("Press any key to stop...");
			Console.ReadKey();
		}
	}
}
