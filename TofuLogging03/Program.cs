using System;
using System.Drawing.Imaging;
using System.IO;
using Tofu.Logging;

namespace TofuLogging03
{
	class Program
	{
		static void Main(string[] args)
		{
			// Start sample
			Console.WriteLine("Running TofuLogging03...");

			// Create a default manager and start logging session (ie. start dedicated logging thread)
			var logMgr = LogManager.CreateManager().StartSession();

			// Create logger for messages
			var log = logMgr.GetLog("Features");

			// Demonstrate various logging functions
			log.AddText(LogLevel.Info, () => "Plain text logging is very straightforward");

			log.AddException(
				LogLevel.Info,
				() => "Exceptions can be logged as flat text in resource folder",
				new ArgumentException("Some exception to log as text"));

			log.AddObject(
				LogLevel.Info,
				() => "Complexe objects can be logged with a single instruction",
				System.Diagnostics.Process.GetCurrentProcess());

			log.AddParams(
				LogLevel.Info,
				() => "Multiple parameters can be logged in one go by providing name and value arrays",
				() => new string[] { "Name", "Age", "Date Of Birth" },
				() => new object[] { "Tofu", 20, new DateTime(2018, 2, 1) });

			log.AddReport(
				LogLevel.Info,
				() => "Difference between AddText and AddReport is that the reports are logged as separate file in resource folder",
				() => Properties.Resources.DummyReport,
				() => "txt");

			log.AddBytes(
				LogLevel.Info,
				() => "Images can also be logged which comes in handy for screenshots or graphs",
				() => {
					using (var memStream = new MemoryStream())
					{
						Properties.Resources.DummyImage.Save(memStream, ImageFormat.Png);
						return memStream.ToArray();
					}},
                () => "png" );

			// Stop session (ie. stop the dedicated logging thread)
			logMgr.StopSession();

			// Show where logfiles are written
			Console.WriteLine(string.Format("Log files: {0}", logMgr.SessionFolderFullName));
			Console.WriteLine("Press any key to stop...");
			Console.ReadKey();
		}
	}
}
