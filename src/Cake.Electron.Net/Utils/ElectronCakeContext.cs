using Cake.Electron.Net.Contracts;
using System.Threading;

namespace Cake.Electron.Net.Utils
{
    public abstract class ElectronCakeContext
    {
        private static readonly ElectronCakeContext DefaultCakeContext = new DefaultElectronCakeContext();

        public static ElectronCakeContext Current
        {
            get
            {
                if (Thread.GetData(Thread.GetNamedDataSlot("ElectroCake")) is ElectronCakeContext electroCakeContext)
                {
                    return electroCakeContext;
                }

                electroCakeContext = DefaultCakeContext;
                Thread.SetData(Thread.GetNamedDataSlot("ElectroCake"), electroCakeContext);
                return electroCakeContext;
            }
            set => Thread.SetData(Thread.GetNamedDataSlot("ElectroCake"), value);
        }

        public abstract IProcessHelper ProcessHelper { get; }
        public abstract ICommandBuilder CommandBuilder { get; }
    }

    internal sealed class DefaultElectronCakeContext : ElectronCakeContext
    {
        public override IProcessHelper ProcessHelper => new ProcessHelper();
        public override ICommandBuilder CommandBuilder => new CommandBuilder();
    }
}