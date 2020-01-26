using Cake.Electron.Net.Contracts;

namespace Cake.Electron.Net.Commands.Settings
{
    public class ElectronNetStartSettings : IElectronNetStartSettings
    {
        public string AspCoreProjectPath { get; set; }

        public string Manifest { get; set; }

        public string[] Arguments { get; set; }

        public string WorkingDirectory { get; set; }
    }
}