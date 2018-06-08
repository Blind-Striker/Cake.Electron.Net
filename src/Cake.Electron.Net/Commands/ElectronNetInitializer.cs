using System;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Electron.Net.Commands.Settings;
using Cake.Electron.Net.Utils;

namespace Cake.Electron.Net.Commands
{
    public static class ElectronNetInitializer
    {
        private const string CmdBase = "dotnet electronize init";

        [CakeMethodAlias]
        public static int ElectronNetInit(this ICakeContext context, ElectronNetInitSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            return ElectronNetInit(context, settings.WorkingDirectory, settings.Path);
        }

        [CakeMethodAlias]
        public static int ElectronNetInit(this ICakeContext context, string workingDirectory, string path = null)
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
