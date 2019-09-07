using Cake.Core;
using Cake.Electron.Net.Commands;
using Cake.Electron.Net.Commands.Settings;
using Cake.Electron.Net.Contracts;
using Cake.Electron.Net.Tests.Mocks;
using Cake.Electron.Net.Utils;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace Cake.Electron.Net.Tests
{
    public class ElectronNetBuilderTests
    {
        private const string CmdBase = "electronize build";

        private readonly Mock<ICakeContext> _cakeContextMock;

        public ElectronNetBuilderTests()
        {
            _cakeContextMock = new Mock<ICakeContext>(MockBehavior.Strict);
        }

        [Fact]
        public void ElectronNetBuild_Should_Throw_ArgumentNullException_If_WorkingDirectory_Is_Null()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            Assert.Throws<ArgumentNullException>(() => cakeContext.ElectronNetBuild(null, "win"));
        }

        [Fact]
        public void ElectronNetBuild_Should_Throw_ArgumentNullException_If_ElectronTarget_Is_Null()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            Assert.Throws<ArgumentNullException>(() => cakeContext.ElectronNetBuild("./Electron", (ElectronTarget)null));
            Assert.Throws<ArgumentNullException>(() => cakeContext.ElectronNetBuild("./Electron", (string)null));
        }

        [Fact]
        public void ElectronNetBuild_Should_Execute_Command_By_Given_WorkingDirectory_ElectronTarget()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            const string workingDirectory = "./SomeDirectory";
            const string electronTarget = "win";
            string expectedCommand = $"{CmdBase} /target {electronTarget}";

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            var commandBuilderMock = new Mock<ICommandBuilder>(MockBehavior.Strict);
            ElectronCakeContext.Current = new TestCakeContext(processHelperMock, commandBuilderMock);

            commandBuilderMock.Setup(builder => builder.SwitchHelper(It.IsAny<string[]>()));
            processHelperMock.Setup(helper => helper.CmdExecute(It.Is<string>(s => s == expectedCommand), It.Is<string>(s => s == workingDirectory), It.IsAny<bool>(), It.IsAny<bool>())).Returns(1);

            cakeContext.ElectronNetBuild(workingDirectory, electronTarget);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
            commandBuilderMock.Verify(builder => builder.SwitchHelper(It.IsAny<string[]>()), Times.Never());
        }

        [Fact]
        public void ElectronNetBuild_Should_Execute_Command_By_Given_WorkingDirectory_ElectronTarget_And_DotnetConfig()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            const string workingDirectory = "./SomeDirectory";
            const string electronTarget = "win";
            const string dotnetConfig = "Release";
            string expectedCommand = $"{CmdBase} /target {electronTarget} /dotnet-configuration {dotnetConfig}";

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            var commandBuilderMock = new Mock<ICommandBuilder>(MockBehavior.Strict);
            ElectronCakeContext.Current = new TestCakeContext(processHelperMock, commandBuilderMock);

            commandBuilderMock.Setup(builder => builder.SwitchHelper(It.IsAny<string[]>()));
            processHelperMock.Setup(helper => helper.CmdExecute(It.Is<string>(s => s == expectedCommand), It.Is<string>(s => s == workingDirectory), It.IsAny<bool>(), It.IsAny<bool>())).Returns(1);

            cakeContext.ElectronNetBuild(workingDirectory, electronTarget, dotnetConfig);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
            commandBuilderMock.Verify(builder => builder.SwitchHelper(It.IsAny<string[]>()), Times.Never());
        }

        [Fact]
        public void ElectronNetBuild_Should_Execute_Command_By_Given_WorkingDirectory_ElectronTarget_And_DotnetConfig_And_RelativePath()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            const string workingDirectory = "./SomeDirectory";
            const string electronTarget = "win";
            const string dotnetConfig = "Release";
            const string relativePath = "./path/under";
            string expectedCommand = $"{CmdBase} /target {electronTarget} /dotnet-configuration {dotnetConfig} /relative-path {relativePath}";

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            var commandBuilderMock = new Mock<ICommandBuilder>(MockBehavior.Strict);
            ElectronCakeContext.Current = new TestCakeContext(processHelperMock, commandBuilderMock);

            commandBuilderMock.Setup(builder => builder.SwitchHelper(It.IsAny<string[]>()));
            processHelperMock.Setup(helper => helper.CmdExecute(It.Is<string>(s => s == expectedCommand), It.Is<string>(s => s == workingDirectory), It.IsAny<bool>(), It.IsAny<bool>())).Returns(1);

            cakeContext.ElectronNetBuild(workingDirectory, electronTarget, dotnetConfig, relativePath);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
            commandBuilderMock.Verify(builder => builder.SwitchHelper(It.IsAny<string[]>()), Times.Never());
        }

        [Fact]
        public void ElectronNetBuild_Should_Execute_Command_By_Given_WorkingDirectory_ElectronTarget_And_DotnetConfig_And_AbsolutePath()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            const string workingDirectory = "./SomeDirectory";
            const string electronTarget = "win";
            const string dotnetConfig = "Release";
            const string absolutePath = "./path/under";
            string expectedCommand = $"{CmdBase} /target {electronTarget} /dotnet-configuration {dotnetConfig} /absolute-path {absolutePath}";

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            var commandBuilderMock = new Mock<ICommandBuilder>(MockBehavior.Strict);
            ElectronCakeContext.Current = new TestCakeContext(processHelperMock, commandBuilderMock);

            commandBuilderMock.Setup(builder => builder.SwitchHelper(It.IsAny<string[]>()));
            processHelperMock.Setup(helper => helper.CmdExecute(It.Is<string>(s => s == expectedCommand), It.Is<string>(s => s == workingDirectory), It.IsAny<bool>(), It.IsAny<bool>())).Returns(1);

            cakeContext.ElectronNetBuild(workingDirectory, electronTarget, dotnetConfig, null, absolutePath);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
            commandBuilderMock.Verify(builder => builder.SwitchHelper(It.IsAny<string[]>()), Times.Never());
        }
    
        [Fact]
        public void ElectronNetBuild_Should_Execute_Command_By_Given_WorkingDirectory_ElectronTarget_And_DotnetConfig_And_AbsolutePath_And_PackageJson()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            const string workingDirectory = "./SomeDirectory";
            const string electronTarget = "win";
            const string dotnetConfig = "Release";
            const string absolutePath = "./path/under";
            const string packageJson = "package.json";
            string expectedCommand = $"{CmdBase} /target {electronTarget} /dotnet-configuration {dotnetConfig} /absolute-path {absolutePath} /package-json {packageJson}";

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            var commandBuilderMock = new Mock<ICommandBuilder>(MockBehavior.Strict);
            ElectronCakeContext.Current = new TestCakeContext(processHelperMock, commandBuilderMock);

            commandBuilderMock.Setup(builder => builder.SwitchHelper(It.IsAny<string[]>()));
            processHelperMock.Setup(helper => helper.CmdExecute(It.Is<string>(s => s == expectedCommand), It.Is<string>(s => s == workingDirectory), It.IsAny<bool>(), It.IsAny<bool>())).Returns(1);

            cakeContext.ElectronNetBuild(workingDirectory, electronTarget, dotnetConfig, null, absolutePath, packageJson);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
            commandBuilderMock.Verify(builder => builder.SwitchHelper(It.IsAny<string[]>()), Times.Never());
        }

        [Fact]
        public void ElectronNetBuild_Should_Execute_Command_By_Given_WorkingDirectory_ElectronTarget_And_DotnetConfig_And_AbsolutePath_And_PackageJson_And_InstallModules()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            const string workingDirectory = "./SomeDirectory";
            const string electronTarget = "win";
            const string dotnetConfig = "Release";
            const string absolutePath = "./path/under";
            const string packageJson = "package.json";
            const bool installModules = true;
            string expectedCommand = $"{CmdBase} /target {electronTarget} /dotnet-configuration {dotnetConfig} /absolute-path {absolutePath} /package-json {packageJson} /install-modules";

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            var commandBuilderMock = new Mock<ICommandBuilder>(MockBehavior.Strict);
            ElectronCakeContext.Current = new TestCakeContext(processHelperMock, commandBuilderMock);

            commandBuilderMock.Setup(builder => builder.SwitchHelper(It.IsAny<string[]>()));
            processHelperMock.Setup(helper => helper.CmdExecute(It.Is<string>(s => s == expectedCommand), It.Is<string>(s => s == workingDirectory), It.IsAny<bool>(), It.IsAny<bool>())).Returns(1);

            cakeContext.ElectronNetBuild(workingDirectory, electronTarget, dotnetConfig, null, absolutePath, packageJson, installModules);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
            commandBuilderMock.Verify(builder => builder.SwitchHelper(It.IsAny<string[]>()), Times.Never());
        }

        [Fact]
        public void ElectronNetBuild_Should_Execute_Command_By_Given_WorkingDirectory_ElectronTarget_DotnetConfig_And_AbsolutePath_And_PackageJson_And_InstallModules_ElectronParams()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            const string workingDirectory = "./SomeDirectory";
            const string electronTarget = "win";
            const string dotnetConfig = "Release";
            const string absolutePath = "./path/under";
            const string packageJson = "package.json";
            const bool installModules = true;
            const string electronParams = "command=conquer";

            string expectedCommand = $"{CmdBase} /target {electronTarget} /dotnet-configuration {dotnetConfig} /absolute-path {absolutePath} /package-json {packageJson} /install-modules /electron-params \"--command=conquer\"";

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            var commandBuilderMock = new Mock<ICommandBuilder>(MockBehavior.Strict);
            ElectronCakeContext.Current = new TestCakeContext(processHelperMock, commandBuilderMock);

            commandBuilderMock
                .Setup(builder => builder.SwitchHelper(It.Is<string[]>(strings => strings.Contains(electronParams))))
                .Returns(() => "--command=conquer");

            processHelperMock.Setup(helper => helper.CmdExecute(It.Is<string>(s => s == expectedCommand), It.Is<string>(s => s == workingDirectory), It.IsAny<bool>(), It.IsAny<bool>())).Returns(1);

            cakeContext.ElectronNetBuild(workingDirectory, electronTarget, dotnetConfig, null, absolutePath, packageJson, installModules, electronParams);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
            commandBuilderMock.Verify(builder => builder.SwitchHelper(It.IsAny<string[]>()), Times.Once);
        }

        [Fact]
        public void ElectronNetBuild_Should_Execute_Command_By_Given_ElectronNetBuildSettings()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            var settings = new ElectronNetBuildSettings()
            {
                WorkingDirectory = "./SomeDirectory",
                DotNetConfig = DotNetConfig.Release,
                ElectronTarget = ElectronTarget.Linux,
                ElectronParams = new[] { "command=conquer" }
            };

            string expectedCommand = $"{CmdBase} /target {ElectronTarget.Linux.Value} /dotnet-configuration Release /electron-params \"--command=conquer\"";

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            var commandBuilderMock = new Mock<ICommandBuilder>(MockBehavior.Strict);
            ElectronCakeContext.Current = new TestCakeContext(processHelperMock, commandBuilderMock);

            commandBuilderMock
                .Setup(builder => builder.SwitchHelper(It.Is<string[]>(strings => strings.All(s => settings.ElectronParams.Contains(s)))))
                .Returns(() => "--command=conquer");

            processHelperMock.Setup(helper => helper.CmdExecute(It.Is<string>(s => s == expectedCommand), It.Is<string>(s => s == settings.WorkingDirectory), It.IsAny<bool>(), It.IsAny<bool>())).Returns(1);

            cakeContext.ElectronNetBuild(settings);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
            commandBuilderMock.Verify(builder => builder.SwitchHelper(It.IsAny<string[]>()), Times.Once);
        }

        [Fact]
        public void ElectronNetBuild_Should_Throw_ArgumentNullException_If_Settings_Is_Null()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            Assert.Throws<ArgumentNullException>(() => cakeContext.ElectronNetBuild(null));
        }
    }
}