using System;
using System.Collections.Generic;
using System.Text;

using Cake.Electron.Net.Contracts;

namespace Cake.Electron.Net.Commands.Settings
{
    public class ElectronNetAddSettings : ICommandSettings
    {
        public ElectronAdd Add { get; set; }

        public string WorkingDirectory { get; set; }
    }
}