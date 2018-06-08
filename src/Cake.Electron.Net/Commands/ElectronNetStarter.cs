using System;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Electron.Net.Commands.Settings;

namespace Cake.Electron.Net.Commands
{
    public static class ElectronNetStarter
    {
        private const string CmdBase = "dotnet electronize start";

        [CakeMethodAlias]
        public static int ElectronNetStart(this ICakeContext context, ElectronNetStartSettings settings)
        {
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

            return ElectroCakeContext.ProcessHelper.CmdExecute(cmd, workingDirectory);
        }
    }
}