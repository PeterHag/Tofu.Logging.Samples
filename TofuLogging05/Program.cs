using System;
using Tofu.Logging;

namespace TofuLogging05
{
	class Program
    {
        static void Main(string[] args)
        {
			// Start sample
			Console.WriteLine("Running TofuLogging05...");

			// Create a default manager but with configuration from resource file
			var config = LogManager.LoadConfig(Properties.Resources.TofuLogging05);
            var logMgr = LogManager.CreateManager(config);

            // When loaded config file has 'AutoStart' set the 'true' the loggingsession will already be started upon creation of the manager.
            // Use the 'SessionMode' property to see if session is already running.
            if (logMgr.SessionMode != SessionModes.Running)
                logMgr.StartSession();

            // ***
            // Note: a copy of the configuration file will automatically be created in the session folder when the session is started.
            // ***

            // Dump some text to running session
            var log = logMgr.GetLog("Messages") as ILog4N;
            log.Debug("This is some message A");

            // Suspending a session means NO LOG ACTIVITY => this message below will NOT be logged!
            logMgr.SuspendSession();
            log.Debug("This is some message B");

            // A suspended session can either be stopped or resumed
            logMgr.ResumeSession();
            log.Debug("This is some message C");

            // Stop session
            logMgr.StopSession();

			// Show where logfiles are written
			Console.WriteLine("Please see logs for impact on suspending a log session.");
			Console.WriteLine(string.Format("Log files: {0}", logMgr.SessionFolderFullName));
			Console.WriteLine("Press any key to stop...");
			Console.ReadKey();
		}
	}
}
