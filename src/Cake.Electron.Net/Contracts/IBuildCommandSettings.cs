namespace Cake.Electron.Net.Contracts
{
    public interface IBuildCommandSettings : ICommandSettings
    {
        DotNetConfig DotNetConfig { get; set; }

        string RelativePath { get; set; }

        string AbsolutePath { get; set; }

        string PackageJson { get; set; }

        bool InstallModules { get; set; }

        string[] ElectronParams { get; set; }
    }
}