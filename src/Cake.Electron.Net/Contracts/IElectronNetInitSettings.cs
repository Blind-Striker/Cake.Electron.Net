namespace Cake.Electron.Net.Contracts
{
    public interface IElectronNetInitSettings: ICommandSettings
    {
        string AspCoreProjectPath { get; set; }

        string Manifest { get; set; }
    }
}