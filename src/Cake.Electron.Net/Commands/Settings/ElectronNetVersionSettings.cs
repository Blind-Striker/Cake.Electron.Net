using Cake.Electron.Net.Contracts;

namespace Cake.Electron.Net.Commands.Settings
{
    public class ElectronNetVersionSettings : ICommandSettings
    {
        public string WorkingDirectory { get; set; }
    }
}