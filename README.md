# Cake.Electron.Net

A Cake AddIn that extends Cake with [Electron.NET](https://github.com/ElectronNET/Electron.NET) command tools.

[![cakebuild.net](https://img.shields.io/badge/WWW-cakebuild.net-blue.svg)](http://cakebuild.net/)

| Stable                                                                                                              | Nightly                                                                                                                                                                        |
|---------------------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [![NuGet](https://img.shields.io/nuget/v/Cake.Electron.Net.svg)](https://www.nuget.org/packages/Cake.Electron.Net) | [![MyGet](https://img.shields.io/myget/cake-electron-net/v/Cake.Electron.Net.svg?label=myget)](https://www.myget.org/feed/cake-electron-net/package/nuget/Cake.Electron.Net) |

## Continuous integration

| Build server    	| Platform 	| Build status                                                                                                                                                                                                                                                                         	|
|-----------------	|----------	|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	|
| Azure Pipelines 	| Ubuntu   	| [![Build Status](https://denizirgindev.visualstudio.com/Cake.Electron.Net/_apis/build/status/Ubuntu?branchName=master)](https://denizirgindev.visualstudio.com/Cake.Electron.Net/_build/latest?definitionId=18&branchName=master)	|
| Azure Pipelines 	| macOs   	| [![Build Status](https://denizirgindev.visualstudio.com/Cake.Electron.Net/_apis/build/status/MacOs?branchName=master)](https://denizirgindev.visualstudio.com/Cake.Electron.Net/_build/latest?definitionId=19&branchName=master) 	|
| Azure Pipelines 	| Windows   	| [![Build Status](https://denizirgindev.visualstudio.com/Cake.Electron.Net/_apis/build/status/Windows?branchName=master)](https://denizirgindev.visualstudio.com/Cake.Electron.Net/_build/latest?definitionId=17&branchName=master)	|
| Travis 	| Ubuntu   	| [![Build Status](https://travis-ci.com/Blind-Striker/Cake.Electron.Net.svg?branch=master)](https://travis-ci.com/Blind-Striker/Cake.Electron.Net)	|
| Appveyor 	| Windows   	| [![Build status](https://ci.appveyor.com/api/projects/status/p3aa0e87s5r2ihom/branch/master?svg=true)](https://ci.appveyor.com/project/Blind-Striker/cake-electron-net/branch/master)	|

## Table of Contents

1. [Requirements To Run](#requirements-to-run)
2. [Including Add-in](#including-add-in)
3. [Usage](#usage)
    - [Commands Supported](#commands-supported)
    - [Example](#example)
4. [License](#license)
5. [Important Notes](#important-notes)
    - [ElectronNET.CLI Version 0.0.9](#electronnetcli-version-009)

## Requirements To Run

`Cake.Electron.Net` is depends on `ElectronNET.CLI` cli tool. Make sure you have installed the [ElectronNET.CLI](https://www.nuget.org/packages/ElectronNET.CLI/) packages as global tool:

```
    dotnet tool install ElectronNET.CLI -g
```

## Including Add-in

Including add-in in cake script is easy.

```
#addin "nuget:?package=Cake.Electron.Net"
```

## Usage

Please see [Electron.NET](https://github.com/ElectronNET/Electron.NET) for commands usages.

### Commands Supported

* ElectronNetBuild
* ElectronNetInit
* ElectronNetStart
* ElectronNetVersion

### Example

```csharp
using Cake.Electron.Net
using Cake.Electron.Net.Commands.Settings

Task("Build")
.Does(() => {
    ElectronNetVersion(workingDirectory);

    ElectronNetBuildSettings settings = new ElectronNetBuildSettings();
    settings.WorkingDirectory = workingDirectory;
    settings.ElectronTarget = ElectronTarget.Win;
    settings.DotNetConfig = DotNetConfig.Release;

    ElectronNetBuild(settings);
});
```

## Licenses
Licensed under MIT, see [LICENSE](LICENSE) for the full text.

## Important Notes

### ElectronNET.CLI Version 0.0.9

In the Version 0.0.9 the CLI was not a global tool and needed to be registred like this in the .csproj:

```
    <ItemGroup>
         <DotNetCliToolReference Include="ElectronNET.CLI" Version="0.0.9" />
    </ItemGroup>
```

If you still use this version you will need to install [Cake.Electron.Net v0.0.9](https://www.nuget.org/packages/Cake.Electron.Net/0.0.9)
