using System.Runtime.InteropServices;
using EvilBaschdi.Core.AppHelpers;

Console.WriteLine("Evaluating Architecture...\n");
var architecture = Enum.GetName(RuntimeInformation.OSArchitecture) ?? throw new ApplicationException("Unknown architecture detected.");
Console.WriteLine($"OS Architecture is {architecture}...\n");

var currentDirectory = Environment.CurrentDirectory;
Console.WriteLine($"Current directory: {currentDirectory}");
var currentDirectoryName = Path.GetFileName(currentDirectory);
Console.WriteLine($"Application name: {currentDirectoryName}");

string? appPath = null;
if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
{
    Console.WriteLine("Detected platform: Windows");
    var exePath = Path.Combine(currentDirectory, architecture, $"{currentDirectoryName}.exe");
    Console.WriteLine($"Checking for Windows executable: {exePath}");
    if (File.Exists(exePath))
    {
        appPath = exePath;
        Console.WriteLine("Found Windows executable.");
    }
    else
    {
        Console.WriteLine("Windows executable not found.");
    }
}
else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
{
    var platform = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Linux" : "macOS";
    Console.WriteLine($"Detected platform: {platform}");
    var nativePath = Path.Combine(currentDirectory, architecture, currentDirectoryName);
    Console.WriteLine($"Checking for native binary: {nativePath}");
    if (File.Exists(nativePath))
    {
        appPath = nativePath;
        Console.WriteLine("Found native binary.");
    }
    else
    {
        Console.WriteLine("Native binary not found. Checking for framework-dependent DLL...");
        var dllPath = Path.Combine(currentDirectory, architecture, $"{currentDirectoryName}.dll");
        Console.WriteLine($"Checking for DLL: {dllPath}");
        if (File.Exists(dllPath))
        {
            appPath = dllPath;
            Console.WriteLine("Found DLL for dotnet execution.");
        }
        else
        {
            Console.WriteLine("DLL not found.");
        }
    }
}
else
{
    Console.WriteLine("Unsupported platform detected.");
}

if (appPath != null && File.Exists(appPath))
{
    Console.WriteLine($"Preparing to execute: {appPath}");
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || appPath == Path.Combine(currentDirectory, architecture, currentDirectoryName))
    {
        Console.WriteLine("Launching native executable...");
        new ProcessByPath().RunFor(appPath);
    }
    else if (appPath.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Launching DLL using dotnet...");
        new ProcessByPath().RunFor($"dotnet {appPath}");
    }
    Console.WriteLine("Application launch command executed.");
}
else
{
    Console.WriteLine("No Application found to execute! :-(");
}

#if DEBUG
Console.WriteLine("Press Enter to exit (DEBUG mode)...");
Console.ReadLine();
#endif