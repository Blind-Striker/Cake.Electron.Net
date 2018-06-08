namespace Cake.Electron.Net.Commands.Settings
{
    public class ElectronNetBuildSettings
    {
        public ElectronTarget ElectronTarget { get; set; } = ElectronTarget.Win;

        public DotNetConfig DotNetConfig { get; set; } = DotNetConfig.Release;

        public string[] ElectronParams { get; set; }

        public string WorkingDirectory { get; set; }
    }
}
