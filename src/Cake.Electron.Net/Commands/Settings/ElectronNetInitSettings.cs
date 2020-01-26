using Cake.Electron.Net.Contracts;

namespace Cake.Electron.Net.Commands.Settings
{
    public class ElectronNetInitSettings :  IElectronNetInitSettings
    {
        public string AspCoreProjectPath { get; set; }

        public string Manifest { get; set; }

        public string WorkingDirectory { get; set; }
    }
}