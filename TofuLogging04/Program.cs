using System;
using Tofu.Logging;

namespace TofuLogging04
{
	class Program
	{
		static void Main(string[] args)
		{
			// Start sample
			Console.WriteLine("Running TofuLogging04...");

			// Create a default manager but do not start yet!
			var logMgr = LogManager.CreateManager();

			// Configure logger avoid disc space creep by setting upper limit to number of log sessions, log files and messages per file
			logMgr.Config.CleanupSessions = 2;      // Only keep log files for last 2 sessions (= 1 current session + 1 previous session)
			logMgr.Config.CleanupMessages = 100;    // We want each log file to contain maximum 100 message entries
			logMgr.Config.CleanupLogs = 3;          // We only want to keep 3 log files per logger instance (3 x 100 = 300 messages kept per logger)
			logMgr.Config.CleanupResources = true;  // We also want resource files to be removed in case the log file that references them is removed

			// Additionally demonstrate that log messages can also be traced to output console
			logMgr.Config.GlobalTrace = true;

			// Start logging session when config is initialized.
			logMgr.StartSession();

			// Dump large amount of messages to demonstrate that only the last ones, as configured above, are kept
			var log = logMgr.GetLog("Messages") as ILog4N;
			for (int i = 0; i < 10000; i++)
				log.DebugFormat("This is message '{0}'", i);

			// Stop session
			logMgr.StopSession();

			// Show where logfiles are written
			Console.WriteLine("Please see output folder for available log files according cleanup settings.");
			Console.WriteLine(string.Format("Log files: {0}", logMgr.SessionFolderFullName));
			Console.WriteLine("Press any key to stop...");
			Console.ReadKey();
		}
	}
}
