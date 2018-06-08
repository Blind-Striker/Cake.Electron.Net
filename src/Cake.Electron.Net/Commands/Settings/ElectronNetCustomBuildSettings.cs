namespace Cake.Electron.Net.Commands.Settings
{
    public class ElectronNetCustomBuildSettings
    {
        public ElectronTargetCustom ElectronTargetCustom { get; set; } = ElectronTargetCustom.Win10WinSrv2016X64;

        public string ElectronArch { get; set; }

        public DotNetConfig DotNetConfig { get; set; } = DotNetConfig.Release;

        public string[] ElectronParams { get; set; }

        public string WorkingDirectory { get; set; }
    }
}