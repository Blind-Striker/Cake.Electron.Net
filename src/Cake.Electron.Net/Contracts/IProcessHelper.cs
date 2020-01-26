namespace Cake.Electron.Net.Contracts
{
    public interface IProcessHelper
    {
        int CmdExecute(string command, string workingDirectoryPath, bool output = true, bool waitForExit = true);
    }
}