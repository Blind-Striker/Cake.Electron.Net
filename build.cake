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

        DotNetCoreBuildSettings settings = new DotNetCoreBuildSettings();
        settings.Configuration = configuration;

        DotNetCoreRestore("./src/Cake.Electron.Net.sln");
        DotNetCoreBuild($"./src/Tests/Cake.Electron.Net.Tests/Cake.Electron.Net.Tests.csproj", settings);
    });

Task("Test")
    .Description("Run Tests")
    .IsDependentOn("Compile")
    .Does(() =>
    {
        string appveyor = IsRunningOnUnix() ? string.Empty : " --test-adapter-path:. --logger:Appveyor";
        string testProjectPath = "./src/Tests/Cake.Electron.Net.Tests/Cake.Electron.Net.Tests.csproj";

        DotNetCoreTestSettings settings = new DotNetCoreTestSettings();
        settings.Configuration = configuration;
        settings.Framework = netCoreTarget;
        settings.ArgumentCustomization  = args => args.Append(appveyor);

        Information($"Running {netCoreTarget.ToUpper()} Tests");

        DotNetCoreTest(testProjectPath, settings);

        Information($"Running {fullFrameworkTarget.ToUpper()} Tests");
        
        if(!IsRunningOnUnix()) //Appveyor
        {
            settings.Framework = fullFrameworkTarget;
            DotNetCoreTest(testProjectPath, settings);
        }
        else // Travis
        {
            StartProcess("mono", new ProcessSettings {
                Arguments = $"./tools/xunit.runner.console/{fullFrameworkTarget}/xunit.console.exe ./src/Tests/Cake.Electron.Net.Tests/bin/Release/{fullFrameworkTarget}/Cake.Electron.Net.Tests.dll"
            });
        }
    });

Task("Init")
    .Description("Initialization")
    .Does(() => {
        StartProcess("git", new ProcessSettings {
            Arguments = "config --global core.autocrlf true"
        });
    });

RunTarget(target);