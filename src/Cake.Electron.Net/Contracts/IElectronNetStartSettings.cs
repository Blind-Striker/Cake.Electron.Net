namespace Cake.Electron.Net.Contracts
{
    public interface IElectronNetStartSettings : ICommandSettings
    {
        string AspCoreProjectPath { get; set; }

        string Manifest { get; set; }

        string[] Arguments { get; set; }
    }
}