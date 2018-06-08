using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Cake.Electron.Net.Contracts;

namespace Cake.Electron.Net.Utils
{
    public abstract class ElectroCakeContext
    {
        private static readonly ElectroCakeContext DefaultTimeProviderContext = new DefaultElectroCakeContext();

        public static ElectroCakeContext Current
        {
            get
            {
                if (Thread.GetData(Thread.GetNamedDataSlot("ElectroCake")) is ElectroCakeContext timeProviderContext)
                {
                    return timeProviderContext;
                }

                timeProviderContext = DefaultTimeProviderContext;
                Thread.SetData(Thread.GetNamedDataSlot("ElectroCake"), timeProviderContext);
                return timeProviderContext;
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
