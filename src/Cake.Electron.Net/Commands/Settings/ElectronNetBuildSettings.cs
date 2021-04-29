using Cake.Electron.Net.Contracts;

namespace Cake.Electron.Net.Commands.Settings
{
    public class ElectronNetBuildSettings : IBuildCommandSettings
    {
        public ElectronTarget ElectronTarget { get; set; } = ElectronTarget.Win;

        public DotNetConfig DotNetConfig { get; set; } = DotNetConfig.Release;

		public bool PublishSingleFile { get; set; } = true;

		public bool PublishReadyToRun { get; set; } = true;

        public string RelativePath { get; set; }

        public string AbsolutePath { get; set; }

        public string PackageJson { get; set; }

        public bool InstallModules { get; set; } = false;

        public string Manifest { get; set; }

        public string[] ElectronParams { get; set; }

        public string WorkingDirectory { get; set; }
    }
}
