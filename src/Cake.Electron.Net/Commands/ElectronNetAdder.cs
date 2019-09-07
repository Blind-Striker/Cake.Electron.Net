using System;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Electron.Net.Commands.Settings;
using Cake.Electron.Net.Utils;

namespace Cake.Electron.Net.Commands
{
    public static class ElectronNetAdder
    {
        private const string CmdBase = "electronize add";

        [CakeMethodAlias]
        public static int ElectronNetAdd(this ICakeContext context, ElectronNetAddSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            return ElectronNetAdd(context, settings.WorkingDirectory, settings.Add.Value);
        }

        [CakeMethodAlias]
        public static int ElectronNetAdd(this ICakeContext context, string workingDirectory, string add)
        {
            if (workingDirectory == null)
            {
                throw new ArgumentNullException(nameof(workingDirectory));
            }

            if (string.IsNullOrEmpty(add))
            {
                throw new ArgumentNullException(nameof(add));
            }

            string cmd = $"{CmdBase} {add}";

            return ElectronCakeContext.Current.ProcessHelper.CmdExecute(cmd, workingDirectory);
        }
    }
}
