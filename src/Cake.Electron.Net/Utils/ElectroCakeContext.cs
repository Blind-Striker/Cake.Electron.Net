using Cake.Electron.Net.Contracts;
using System.Threading;

namespace Cake.Electron.Net.Utils
{
    public abstract class ElectroCakeContext
    {
        private static readonly ElectroCakeContext DefaultElectroCakeContext = new DefaultElectroCakeContext();

        public static ElectroCakeContext Current
        {
            get
            {
                if (Thread.GetData(Thread.GetNamedDataSlot("ElectroCake")) is ElectroCakeContext electroCakeContext)
                {
                    return electroCakeContext;
                }

                electroCakeContext = DefaultElectroCakeContext;
                Thread.SetData(Thread.GetNamedDataSlot("ElectroCake"), electroCakeContext);
                return electroCakeContext;
            }
            set => Thread.SetData(Thread.GetNamedDataSlot("ElectroCake"), value);
        }

        public abstract IProcessHelper ProcessHelper { get; }
        public abstract ICommandBuilder CommandBuilder { get; }
    }

    internal sealed class DefaultElectroCakeContext : ElectroCakeContext
    {
        public override IProcessHelper ProcessHelper => new ProcessHelper();
        public override ICommandBuilder CommandBuilder => new CommandBuilder();
    }
}