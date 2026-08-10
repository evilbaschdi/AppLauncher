using System.Runtime.InteropServices;
using System.Text;
using EvilBaschdi.Core.AppHelpers;
using Spectre.Console;

Console.OutputEncoding = Encoding.UTF8;

AnsiConsole.Write(new Rule("[yellow]AppLauncher[/]").LeftJustified());
AnsiConsole.WriteLine();

AnsiConsole.MarkupLine("[blue]ℹ[/]  Evaluating Architecture...");
var architecture = Enum.GetName(RuntimeInformation.OSArchitecture)?.ToLowerInvariant() ?? throw new ApplicationException("Unknown architecture detected.");
AnsiConsole.MarkupLine($"[blue]ℹ[/]  OS Architecture is [green]{architecture}[/].");

var currentDirectory = Environment.CurrentDirectory;
AnsiConsole.MarkupLine($"[blue]ℹ[/]  Current directory: [grey]{currentDirectory}[/]");
var currentDirectoryName = Path.GetFileName(currentDirectory);
AnsiConsole.MarkupLine($"[blue]ℹ[/]  Application name: [yellow]{currentDirectoryName}[/]");

string? appPath = null;
if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
{
    AnsiConsole.MarkupLine("[blue]ℹ[/]  Detected platform: [white]Windows[/]");
    var exePath = Path.Combine(currentDirectory, $"win-{architecture}", $"{currentDirectoryName}.exe");
    AnsiConsole.MarkupLine($"[blue]ℹ[/]  Checking for Windows executable: [grey]{exePath}[/]");
    if (File.Exists(exePath))
    {
        appPath = exePath;
        AnsiConsole.MarkupLine("[green]✔[/]  Found Windows executable.");
    }
    else
    {
        AnsiConsole.MarkupLine("[yellow]⚠[/]  Windows executable not found.");
    }
}
else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
{
    var platform = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Linux" : "macOS";
    AnsiConsole.MarkupLine($"[blue]ℹ[/]  Detected platform: [white]{platform}[/]");
    var nativePath = Path.Combine(currentDirectory, architecture, currentDirectoryName);
    AnsiConsole.MarkupLine($"[blue]ℹ[/]  Checking for native binary: [grey]{nativePath}[/]");
    if (File.Exists(nativePath))
    {
        appPath = nativePath;
        AnsiConsole.MarkupLine("[green]✔[/]  Found native binary.");
    }
    else
    {
        AnsiConsole.MarkupLine("[yellow]⚠[/]  Native binary not found. Checking for framework-dependent DLL...");
        var dllPath = Path.Combine(currentDirectory, architecture, $"{currentDirectoryName}.dll");
        AnsiConsole.MarkupLine($"[blue]ℹ[/]  Checking for DLL: [grey]{dllPath}[/]");
        if (File.Exists(dllPath))
        {
            appPath = dllPath;
            AnsiConsole.MarkupLine("[green]✔[/]  Found DLL for dotnet execution.");
        }
        else
        {
            AnsiConsole.MarkupLine("[red]✖[/]  DLL not found.");
        }
    }
}
else
{
    AnsiConsole.MarkupLine("[red]✖[/]  Unsupported platform detected.");
}

if (appPath != null && File.Exists(appPath))
{
    AnsiConsole.MarkupLine($"[blue]ℹ[/]  Preparing to execute: [grey]{appPath}[/]");
    var appDirectory = Path.GetDirectoryName(appPath) ?? currentDirectory;
    AnsiConsole.MarkupLine($"[blue]ℹ[/]  Setting working directory to: [grey]{appDirectory}[/]");

    var processByPath = new ProcessByPath();

    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || appPath == Path.Combine(currentDirectory, architecture, currentDirectoryName))
    {
        AnsiConsole.MarkupLine("[blue]ℹ[/]  Launching native executable...");

        var process = processByPath.ValueFor(appPath, appDirectory);
        process.Start();
    }
    else if (appPath.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
    {
        AnsiConsole.MarkupLine("[blue]ℹ[/]  Launching DLL using dotnet...");

        var process = processByPath.ValueFor($"dotnet {appPath}", appDirectory);
        process.Start();
    }

    AnsiConsole.MarkupLine("[green]✔[/]  Application launch command executed.");
}
else
{
    AnsiConsole.MarkupLine("[red]✖[/]  No Application found to execute! :-(");
}

#if DEBUG
AnsiConsole.WriteLine();
AnsiConsole.MarkupLine("[grey]Press Enter to exit (DEBUG mode)...[/]");
Console.ReadLine();
#endif