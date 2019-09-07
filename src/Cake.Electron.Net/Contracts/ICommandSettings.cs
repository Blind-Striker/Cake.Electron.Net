using System;
using System.Collections.Generic;
using System.Text;

namespace Cake.Electron.Net.Contracts
{
    public interface ICommandSettings
    {
        string WorkingDirectory { get; set; }
    }
}
