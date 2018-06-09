var target = Argument("target", "Default");

using System;
using System.Diagnostics;

// Variables
var configuration = "Release";
var fullFrameworkTarget = "net46";
var netCoreTarget = "netcoreapp2.0";

Task("Default")
    .IsDependentOn("Test");

Task("Compile")
    .Description("Builds all the projects in the solution")
    .Does(() =>
    {
        StartProcess("dotnet", new ProcessSettings {
            Arguments = "--info"
        });

        StartProcess("dotnet", new ProcessSettings {
            Arguments = "restore ./src/Cake.Electron.Net.sln"
        });

        StartProcess("dotnet", new ProcessSettings {
            Arguments = $"build ./src/Tests/Cake.Electron.Net.Tests/Cake.Electron.Net.Tests.csproj -c {configuration}"
        });
    });

Task("Test")
    .Description("Run Tests")
    .IsDependentOn("Compile")
    .Does(() =>
    {
        string appveyor = IsRunningOnUnix() ? string.Empty : " --test-adapter-path:. --logger:Appveyor";

         Information($"Running {fullFrameworkTarget.ToUpper()} Tests");

        if(!IsRunningOnUnix())
        {
            StartProcess("dotnet", new ProcessSettings {
                Arguments = $"test ./src/Tests/Cake.Electron.Net.Tests/Cake.Electron.Net.Tests.csproj -c {configuration} -f {fullFrameworkTarget}{appveyor}"
            });
        }
        else
        {
            StartProcess("nuget", new ProcessSettings {
                Arguments = "install xunit.runner.console -OutputDirectory testrunner"
            });

            StartProcess("mono", new ProcessSettings {
                Arguments = $"./testrunner/xunit.runner.console.*/tools/{fullFrameworkTarget}/xunit.console.exe ./src/Tests/Cake.Electron.Net.Tests/Cake.Electron.Net.Tests/bin/Release/{fullFrameworkTarget}/Cake.Electron.Net.Tests.dll"
            });
        }

        Information($"Running {netCoreTarget.ToUpper()} Tests");

        StartProcess("dotnet", new ProcessSettings {
            Arguments = $"test ./src/Tests/Cake.Electron.Net.Tests/Cake.Electron.Net.Tests.csproj -c {configuration} -f {netCoreTarget}{appveyor}"
        });
    });

Task("Init")
    .Description("Initialization")
    .Does(() => {
        StartProcess("git", new ProcessSettings {
            Arguments = "config --global core.autocrlf true"
        });
    });

RunTarget(target);