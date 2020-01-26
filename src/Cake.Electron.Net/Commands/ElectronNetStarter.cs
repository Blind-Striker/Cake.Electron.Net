using Cake.Core;
using Cake.Core.Annotations;
using Cake.Electron.Net.Commands.Settings;
using Cake.Electron.Net.Utils;
using System;
using System.Text;

namespace Cake.Electron.Net.Commands
{
    public static class ElectronNetStarter
    {
        private const string CmdBase = "electronize start";

        [CakeMethodAlias]
        public static int ElectronNetStart(this ICakeContext context, ElectronNetStartSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            return ElectronNetStart(context, settings.WorkingDirectory, settings.AspCoreProjectPath, settings.Manifest, settings.Arguments);
        }

        [CakeMethodAlias]
        public static int ElectronNetStart(
            this ICakeContext context, 
            string workingDirectory, 
            string aspCoreProjectPath = null,
            string manifest = null,
            params string[] arguments)
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

            if (arguments == null || arguments.Length <= 0)
            {
                return ElectronCakeContext.Current.ProcessHelper.CmdExecute(cmdBuilder.ToString(), workingDirectory);
            }

            string switches = ElectronCakeContext.Current.CommandBuilder.SwitchHelper(arguments);

            cmdBuilder.Append($" /args {switches}");

            return ElectronCakeContext.Current.ProcessHelper.CmdExecute(cmdBuilder.ToString(), workingDirectory);
        }
    }
}