// See https://aka.ms/new-console-template for more information

using System.Runtime.InteropServices;
using EvilBaschdi.Core.AppHelpers;

Console.WriteLine("Evaluating Architecture...");
Console.WriteLine();
var architecture = Enum.GetName(RuntimeInformation.OSArchitecture) ?? throw new ApplicationException("Someone has invented a new architecture ;-)");

Console.WriteLine($"OS Architecture is {architecture}...");
Console.WriteLine();
var currentDirectory = Environment.CurrentDirectory;
Console.WriteLine($"Looking for Application in {currentDirectory}...");
Console.WriteLine();

var currentDirectoryName = new DirectoryInfo(currentDirectory).Name;
var concatAppPath = Path.Combine(currentDirectory.Trim(), architecture.Trim(), $"{currentDirectoryName.Trim()}.exe");

if (File.Exists(concatAppPath))
{
    Console.WriteLine($"Executing '{concatAppPath}'...");
    IProcessByPath processByPath = new ProcessByPath();
    processByPath.RunFor(concatAppPath);
}
else
{
    Console.WriteLine("No Application found! :-(");
}

#if DEBUG
Console.ReadLine();
#endif