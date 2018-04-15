using System.Reflection;

[assembly: AssemblyTitle("TofuLogging02")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyCompany("Peter Hagenaers")]
[assembly: AssemblyProduct("Tofu Logging")]
[assembly: AssemblyCopyright("Copyright © 2018 - Peter Hagenaers")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyVersion("1.0.2.0")]
[assembly: AssemblyFileVersion("1.0.2.0")]
[assembly: AssemblyInformationalVersion("1.0.2.0")]

#if DEBUG || Debug
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif
