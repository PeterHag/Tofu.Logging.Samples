using System;
using Tofu.Logging;

namespace TofuLogging02
{
	class Program
	{
		static void Main(string[] args)
		{
			// Start sample
			Console.WriteLine("Running TofuLogging02...");

			// Create a default manager and start logging session (ie. start dedicated logging thread)
			var logMgr = LogManager.CreateManager().StartSession();

			// Create logger for messages
			var log = logMgr.GetLog("Messages") as ILog4N;

			// Create log on the fly for details about the loop
			var count = 100000;
			logMgr.GetLog("Loop").AddParams(
				LogLevel.Debug,
				() => "Loop starts",
				() => new string[] { "count" },
				() => new object[] { count });

			// Dump a lot of messages to see how fast Tofu processes them
			var start = DateTime.Now;
			for (int i = 0; i < count; i++)
				log.Info(i.ToString());
			var end = DateTime.Now;

			// Indicate loop terminated
			logMgr.GetLog("Loop").AddText(LogLevel.Debug, () => "Loop ends");

			// Stop session (ie. stop the dedicated logging thread)
			logMgr.StopSession();

			// Show where logfiles are written
			Console.WriteLine(string.Format("Writing #{0} entries took {1}ms.", count, (end - start).TotalMilliseconds));
			Console.WriteLine(string.Format("Log files: {0}", logMgr.SessionFolderFullName));
			Console.WriteLine("Press any key to stop...");
			Console.ReadKey();
		}
	}
}
