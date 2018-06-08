using Cake.Electron.Net.Contracts;

namespace Cake.Electron.Net
{
    public static class ElectroCakeContext
    {
        public static IProcessHelper ProcessHelper { get; set; } = new ProcessHelper();

        public static ICommandBuilder CommandBuilder { get; set; } = new CommandBuilder();
    }
}
