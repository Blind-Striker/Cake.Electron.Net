using Cake.Core;
using Cake.Core.Annotations;
using Cake.Electron.Net.Commands.Settings;
using Cake.Electron.Net.Utils;
using System;
using System.Text;

namespace Cake.Electron.Net.Commands
{
    public static class ElectronNetBuilder
    {
        private const string CmdBase = "electronize build";

        [CakeMethodAlias]
        public static int ElectronNetBuild(this ICakeContext context, ElectronNetBuildSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            return ElectronNetBuild(context,
                settings.WorkingDirectory,
                settings.ElectronTarget,
                settings.DotNetConfig,
                settings.RelativePath,
                settings.AbsolutePath,
                settings.PackageJson,
                settings.InstallModules,
                settings.ElectronParams);
        }

        [CakeMethodAlias]
        public static int ElectronNetBuild(
            this ICakeContext context,
            string workingDirectory,
            ElectronTarget electronTarget,
            DotNetConfig? dotNetConfig = null,
            string relativePath = null,
            string absolutePath = null,
            string packageJson = null,
            bool installModules = false,
            params string[] electronParams)
        {
            return ElectronNetBuild(
                context,
                workingDirectory,
                electronTarget?.Value,
                dotNetConfig?.ToString(),
                relativePath,
                absolutePath,
                packageJson,
                installModules,
                electronParams);
        }

        [CakeMethodAlias]
        public static int ElectronNetBuild(
            this ICakeContext context,
            string workingDirectory,
            string electronTarget,
            string dotNetConfig = null,
            string relativePath = null,
            string absolutePath = null,
            string packageJson = null,
            bool installModules = false,
            params string[] electronParams)
        {
            if (workingDirectory == null)
            {
                throw new ArgumentNullException(nameof(workingDirectory));
            }

            if (electronTarget == null)
            {
                throw new ArgumentNullException(nameof(electronTarget));
            }


            var cmdBuilder = new StringBuilder();
            cmdBuilder.Append($"{CmdBase} /target {electronTarget}");

            if (dotNetConfig != null)
            {
                cmdBuilder.Append($" /dotnet-configuration {dotNetConfig}");
            }

            if (relativePath != null)
            {
                cmdBuilder.Append($" /relative-path {relativePath}");
            }

            if (absolutePath != null)
            {
                cmdBuilder.Append($" /absolute-path {absolutePath}");
            }

            if (packageJson != null)
            {
                cmdBuilder.Append($" /package-json {packageJson}");
            }

            if (installModules)
            {
                cmdBuilder.Append(" /install-modules");
            }

            if (electronParams == null || electronParams.Length <= 0)
            {
                return ElectronCakeContext.Current.ProcessHelper.CmdExecute(cmdBuilder.ToString(), workingDirectory);
            }

            string switches = ElectronCakeContext.Current.CommandBuilder.SwitchHelper(electronParams);

            cmdBuilder.Append($" /electron-params \"{switches}\"");

            return ElectronCakeContext.Current.ProcessHelper.CmdExecute(cmdBuilder.ToString(), workingDirectory);
        }

        [CakeMethodAlias]
        public static int ElectronNetBuildCustom(this ICakeContext context, ElectronNetCustomBuildSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            return ElectronNetBuildCustom(context,
                settings.WorkingDirectory,
                settings.ElectronTargetCustom,
                settings.ElectronArch,
                settings.DotNetConfig,
                settings.RelativePath,
                settings.AbsolutePath,
                settings.PackageJson,
                settings.InstallModules,
                settings.ElectronParams);
        }

        [CakeMethodAlias]
        public static int ElectronNetBuildCustom(
            this ICakeContext context,
            string workingDirectory,
            ElectronTargetCustom electronTarget,
            string electronArch = null,
            DotNetConfig? dotNetConfig = null,
            string relativePath = null,
            string absolutePath = null,
            string packageJson = null,
            bool installModules = false,
            params string[] electronParams)
        {
            return ElectronNetBuildCustom(
                context,
                workingDirectory,
                electronTarget?.Value,
                electronArch,
                dotNetConfig?.ToString(),
                relativePath,
                absolutePath,
                packageJson,
                installModules,
                electronParams);
        }

        [CakeMethodAlias]
        public static int ElectronNetBuildCustom(
            this ICakeContext context,
            string workingDirectory,
            string electronTarget,
            string electronArch = null,
            string dotNetConfig = null,
            string relativePath = null,
            string absolutePath = null,
            string packageJson = null,
            bool installModules = false,
            params string[] electronParams)
        {
            if (workingDirectory == null)
            {
                throw new ArgumentNullException(nameof(workingDirectory));
            }

            if (electronTarget == null)
            {
                throw new ArgumentNullException(nameof(electronTarget));
            }

            var cmdBuilder = new StringBuilder();
            cmdBuilder.Append($"{CmdBase} /target custom {electronTarget}");

            if (dotNetConfig != null)
            {
                cmdBuilder.Append($" /dotnet-configuration {dotNetConfig}");
            }

            if (electronArch != null)
            {
                cmdBuilder.Append($" /electron-arch {electronArch}");
            }

            if (relativePath != null)
            {
                cmdBuilder.Append($" /relative-path {relativePath}");
            }

            if (absolutePath != null)
            {
                cmdBuilder.Append($" /absolute-path {absolutePath}");
            }

            if (packageJson != null)
            {
                cmdBuilder.Append($" /package-json {packageJson}");
            }

            if (installModules)
            {
                cmdBuilder.Append(" /install-modules");
            }

            if (electronParams == null || electronParams.Length <= 0)
            {
                return ElectronCakeContext.Current.ProcessHelper.CmdExecute(cmdBuilder.ToString(), workingDirectory);
            }

            string switches = ElectronCakeContext.Current.CommandBuilder.SwitchHelper(electronParams);

            cmdBuilder.Append($" /electron-params \"{switches}\"");

            return ElectronCakeContext.Current.ProcessHelper.CmdExecute(cmdBuilder.ToString(), workingDirectory);
        }
    }
}