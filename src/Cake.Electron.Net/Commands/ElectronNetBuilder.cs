using System;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Electron.Net.Commands.Settings;

namespace Cake.Electron.Net.Commands
{
    public static class ElectronNetBuilder
    {
        private const string CmdBase = "dotnet electronize build";

        [CakeMethodAlias]
        public static int ElectronNetBuild(this ICakeContext context, ElectronNetBuildSettings settings)
        {
            return ElectronNetBuild(context, 
                settings.WorkingDirectory,
                settings.ElectronTarget, 
                settings.DotNetConfig,
                settings.ElectronParams);
        }

        [CakeMethodAlias]
        public static int ElectronNetBuild(this ICakeContext context, string workingDirectory, ElectronTarget electronTarget, DotNetConfig dotNetConfig = DotNetConfig.Release, params string[] electronParams)
        {
            return ElectronNetBuild(context, workingDirectory, electronTarget.Value, dotNetConfig.ToString(), electronParams);
        }

        [CakeMethodAlias]
        public static int ElectronNetBuild(this ICakeContext context, string workingDirectory, string electronTarget, string dotNetConfig = "Release", params string[] electronParams)
        {
            if (workingDirectory == null)
            {
                throw new ArgumentNullException(nameof(workingDirectory));
            }

            if (electronTarget == null)
            {
                throw new ArgumentNullException(nameof(electronTarget));
            }

            if (dotNetConfig == null)
            {
                throw new ArgumentNullException(nameof(dotNetConfig));
            }

            var cmd = $"{CmdBase} /target {electronTarget} /dotnet-configuration {dotNetConfig}";

            if (electronParams != null && electronParams.Length > 0)
            {
                string switchs = ElectroCakeContext.CommandBuilder.SwitchHelper(electronTarget);

                cmd = $"{cmd} /electron-params \"{switchs}\"";
            }

            return ElectroCakeContext.ProcessHelper.CmdExecute(cmd, workingDirectory);
        }

        [CakeMethodAlias]
        public static int ElectronNetBuild(this ICakeContext context, ElectronNetCustomBuildSettings settings)
        {
            return ElectronNetBuild(context, 
                settings.WorkingDirectory, 
                settings.ElectronTargetCustom,
                settings.ElectronArch, 
                settings.DotNetConfig, 
                settings.ElectronParams);
        }

        [CakeMethodAlias]
        public static int ElectronNetBuild(this ICakeContext context, string workingDirectory, ElectronTargetCustom electronTarget, string electronArch, DotNetConfig dotNetConfig = DotNetConfig.Release, params string[] electronParams)
        {
            return ElectronNetBuild(context, workingDirectory, electronTarget.Value, electronArch, dotNetConfig.ToString(), electronParams);
        }

        [CakeMethodAlias]
        public static int ElectronNetBuild(this ICakeContext context, string workingDirectory, string electronTarget, string electronArch, string dotNetConfig = "Release", params string[] electronParams)
        {
            if (workingDirectory == null)
            {
                throw new ArgumentNullException(nameof(workingDirectory));
            }

            if (electronTarget == null)
            {
                throw new ArgumentNullException(nameof(electronTarget));
            }

            if (electronArch == null)
            {
                throw new ArgumentNullException(nameof(electronArch));
            }

            if (dotNetConfig == null)
            {
                throw new ArgumentNullException(nameof(dotNetConfig));
            }

            var cmd = $"{CmdBase} /target custom {electronTarget} /dotnet-configuration {dotNetConfig} /electron-arch {electronArch}";

            if (electronParams != null && electronParams.Length > 0)
            {
                string switchs = ElectroCakeContext.CommandBuilder.SwitchHelper(electronTarget);

                cmd = $"{cmd} /electron-params \"{switchs}\"";
            }

            return ElectroCakeContext.ProcessHelper.CmdExecute(cmd, workingDirectory);
        }
    }
}
