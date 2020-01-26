namespace Cake.Electron.Net.Contracts
{
    public interface ICommandBuilder
    {
        string SwitchHelper(params string[] args);
    }
}