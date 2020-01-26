using Cake.Core;
using Cake.Core.Annotations;
using Cake.Electron.Net.Commands.Settings;
using Cake.Electron.Net.Utils;

using System;
using System.Text;

namespace Cake.Electron.Net.Commands
{
    public static class ElectronNetInitializer
    {
        private const string CmdBase = "electronize init";

        [CakeMethodAlias]
        public static int ElectronNetInit(this ICakeContext context, ElectronNetInitSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            return ElectronNetInit(context, settings.WorkingDirectory, settings.AspCoreProjectPath, settings.Manifest);
        }

        [CakeMethodAlias]
        public static int ElectronNetInit(this ICakeContext context, string workingDirectory, string aspCoreProjectPath = null, string manifest = null)
        {
            if (workingDirectory == null)
            {
                throw new ArgumentNullException(nameof(workingDirectory));
            }

            var cmdBuilder = new StringBuilder();
            cmdBuilder.Append($"{CmdBase}");

            if (aspCoreProjectPath != null)
            {
                cmdBuilder.Append($" /project-path {aspCoreProjectPath}");
            }

            if (manifest != null)
            {
                cmdBuilder.Append($" /manifest {manifest}");
            }

            return ElectronCakeContext.Current.ProcessHelper.CmdExecute(cmdBuilder.ToString(), workingDirectory);
        }
    }
}