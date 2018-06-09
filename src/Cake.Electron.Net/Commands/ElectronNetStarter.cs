using Cake.Core;
using Cake.Core.Annotations;
using Cake.Electron.Net.Commands.Settings;
using Cake.Electron.Net.Utils;
using System;

namespace Cake.Electron.Net.Commands
{
    public static class ElectronNetStarter
    {
        private const string CmdBase = "dotnet electronize start";

        [CakeMethodAlias]
        public static int ElectronNetStart(this ICakeContext context, ElectronNetStartSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            return ElectronNetStart(context, settings.WorkingDirectory, settings.Path);
        }

        [CakeMethodAlias]
        public static int ElectronNetStart(this ICakeContext context, string workingDirectory, string path = null)
        {
            if (workingDirectory == null)
            {
                throw new ArgumentNullException(nameof(workingDirectory));
            }

            string cmd = CmdBase;

            if (path != null)
            {
                cmd = $"{cmd} {path}";
            }

            return ElectroCakeContext.Current.ProcessHelper.CmdExecute(cmd, workingDirectory);
        }
    }
}