# Cake.Electron.Net

A Cake AddIn that extends Cake with [Electron.NET](https://github.com/ElectronNET/Electron.NET) command tools.

## Builds status
|       | Linux | OS X |
|-------|-------|----------|
| Build | [![Build Status](https://travis-ci-job-status.herokuapp.com/badge/armutcom/Nancy.Serialization.Hyperion/master/linux)](https://travis-ci.org/armutcom/Nancy.Serialization.Hyperion)      | [![Build status](https://ci.appveyor.com/api/projects/status/p3aa0e87s5r2ihom/branch/master?svg=true)](https://ci.appveyor.com/project/Blind-Striker/cake-electron-net/branch/master)      |

## Requirements to run
Cake.Electron.Net is depends on dotnet-electronize package ElectronNET.CLI NuGet package. This package must be referenced in the .csproj like this:

```xml
<ItemGroup>
        <DotNetCliToolReference Include="ElectronNET.CLI" Version="0.0.9" />
</ItemGroup>
```

## Including addin
Including addin in cake script is easy.

```
#addin "nuget:?package=Cake.Electron.Net"
```

## Usage
Please [Electron.NET](https://github.com/ElectronNET/Electron.NET) for commands usages.

### Commands supported
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