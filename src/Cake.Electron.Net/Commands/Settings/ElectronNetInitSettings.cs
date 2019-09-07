using Cake.Electron.Net.Contracts;

namespace Cake.Electron.Net.Commands.Settings
{
    public class ElectronNetInitSettings : ICommandSettings
    {
        public string WorkingDirectory { get; set; }

        public string Path { get; set; }
    }
}