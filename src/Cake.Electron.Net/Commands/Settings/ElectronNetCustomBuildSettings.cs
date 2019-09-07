using Cake.Electron.Net.Contracts;

namespace Cake.Electron.Net.Commands.Settings
{
    public class ElectronNetCustomBuildSettings : IBuildCommandSettings
    {
        public ElectronTargetCustom ElectronTargetCustom { get; set; } = ElectronTargetCustom.Win10WinSrv2016X64;

        public string ElectronArch { get; set; }

        public DotNetConfig DotNetConfig { get; set; } = DotNetConfig.Release;

        public string RelativePath { get; set; }

        public string AbsolutePath { get; set; }

        public string PackageJson { get; set; }

        public bool InstallModules { get; set; } = false;

        public string[] ElectronParams { get; set; }

        public string WorkingDirectory { get; set; }
    }
}