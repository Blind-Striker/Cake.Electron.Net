using System;
using System.Linq;
using Cake.Core;
using Cake.Electron.Net.Commands;
using Cake.Electron.Net.Commands.Settings;
using Cake.Electron.Net.Contracts;
using Cake.Electron.Net.Tests.Mocks;
using Cake.Electron.Net.Utils;
using Moq;
using Xunit;

namespace Cake.Electron.Net.Tests
{
    public class ElectronNetBuilderCustomTests
    {
        private const string CmdBase = "electronize build";

        private readonly Mock<ICakeContext> _cakeContextMock;

        public ElectronNetBuilderCustomTests()
        {
            _cakeContextMock = new Mock<ICakeContext>(MockBehavior.Strict);
        }

        [Fact]
        public void ElectronNetBuildCustom_Should_Throw_ArgumentNullException_If_WorkingDirectory_Is_Null()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            Assert.Throws<ArgumentNullException>(() => cakeContext.ElectronNetBuildCustom(null, "win", "ia32"));
        }

        [Fact]
        public void ElectronNetBuildCustom_Should_Throw_ArgumentNullException_If_ElectronTarget_Is_Null()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            Assert.Throws<ArgumentNullException>(() => cakeContext.ElectronNetBuildCustom("./Electron", null, "ia32", "Release"));
        }

        [Fact]
        public void ElectronNetBuildCustom_Should_Execute_Command_By_Given_WorkingDirectory_ElectronTarget_ElectronArch()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            string workingDirectory = "./SomeDirectory";
            string electronTarget = "win";
            string electronArch = "ia32";

            var expectedCommand = $"{CmdBase} /target custom {electronTarget} /electron-arch {electronArch}";

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            var commandBuilderMock = new Mock<ICommandBuilder>(MockBehavior.Strict);
            ElectronCakeContext.Current = new TestCakeContext(processHelperMock, commandBuilderMock);

            processHelperMock.Setup(helper => helper.CmdExecute(It.Is<string>(s => s == expectedCommand), It.Is<string>(s => s == workingDirectory), It.IsAny<bool>(), It.IsAny<bool>())).Returns(1);

            cakeContext.ElectronNetBuildCustom(workingDirectory, electronTarget, electronArch);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
            commandBuilderMock.Verify(builder => builder.SwitchHelper(It.IsAny<string[]>()), Times.Never);
        }

        [Fact]
        public void ElectronNetBuildCustom_Should_Execute_Command_By_Given_WorkingDirectory_ElectronTarget_ElectronArch_And_DotnetConfig()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            string workingDirectory = "./SomeDirectory";
            string electronTarget = "win";
            string electronArch = "ia32";
            string dotnetConfig = "Release";

            var expectedCommand = $"{CmdBase} /target custom {electronTarget} /dotnet-configuration {dotnetConfig} /electron-arch {electronArch}";

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            var commandBuilderMock = new Mock<ICommandBuilder>(MockBehavior.Strict);
            ElectronCakeContext.Current = new TestCakeContext(processHelperMock, commandBuilderMock);

            processHelperMock.Setup(helper => helper.CmdExecute(It.Is<string>(s => s == expectedCommand), It.Is<string>(s => s == workingDirectory), It.IsAny<bool>(), It.IsAny<bool>())).Returns(1);

            cakeContext.ElectronNetBuildCustom(workingDirectory, electronTarget, electronArch, dotnetConfig);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
            commandBuilderMock.Verify(builder => builder.SwitchHelper(It.IsAny<string[]>()), Times.Never);
        }

        [Fact]
        public void ElectronNetBuildCustom_Should_Execute_Command_By_Given_WorkingDirectory_ElectronTarget_ElectronArch_DotnetConfig_ElectronParams()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            string workingDirectory = "./SomeDirectory";
            string electronTarget = "win";
            string electronArch = "ia32";
            string dotnetConfig = "Release";
            string electronParams = "command=conquer";

            var expectedCommand = $"{CmdBase} /target custom {electronTarget} /dotnet-configuration {dotnetConfig} /electron-arch {electronArch} /electron-params \"--command=conquer\"";

            var commandBuilderMock = new Mock<ICommandBuilder>(MockBehavior.Strict);
            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            ElectronCakeContext.Current = new TestCakeContext(processHelperMock, commandBuilderMock);

            commandBuilderMock
                .Setup(builder => builder.SwitchHelper(It.Is<string[]>(strings => strings.Contains(electronParams))))
                .Returns(() => "--command=conquer");

            processHelperMock.Setup(helper => helper.CmdExecute(It.Is<string>(s => s == expectedCommand), It.Is<string>(s => s == workingDirectory), It.IsAny<bool>(), It.IsAny<bool>())).Returns(1);

            cakeContext.ElectronNetBuildCustom(workingDirectory, electronTarget, electronArch, dotnetConfig, electronParams);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
            commandBuilderMock.Verify(builder => builder.SwitchHelper(It.IsAny<string[]>()), Times.Once);
        }

        [Fact]
        public void ElectronNetBuildCustom_Should_Throw_ArgumentNullException_If_Settings_Is_Null()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            Assert.Throws<ArgumentNullException>(() => cakeContext.ElectronNetBuildCustom(null));
        }

        [Fact]
        public void ElectronNetBuildCustom_Should_Execute_Command_By_Given_ElectronNetCustomBuildSettings()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            ElectronNetCustomBuildSettings settings = new ElectronNetCustomBuildSettings()
            {
                WorkingDirectory = "./SomeDirectory",
                DotNetConfig = DotNetConfig.Release,
                ElectronTargetCustom = ElectronTargetCustom.MacOs1011X64,
                ElectronArch = "ia32",
                ElectronParams = new[] { "command=conquer" }
            };

            var expectedCommand = $"{CmdBase} /target custom {ElectronTargetCustom.MacOs1011X64.Value} /dotnet-configuration Release /electron-arch {settings.ElectronArch} /electron-params \"--command=conquer\"";

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            var commandBuilderMock = new Mock<ICommandBuilder>(MockBehavior.Strict);
            ElectronCakeContext.Current = new TestCakeContext(processHelperMock, commandBuilderMock);

            commandBuilderMock
                .Setup(builder => builder.SwitchHelper(It.Is<string[]>(strings => strings.All(s => settings.ElectronParams.Contains(s)))))
                .Returns(() => "--command=conquer");

            processHelperMock.Setup(helper => helper.CmdExecute(It.Is<string>(s => s == expectedCommand), It.Is<string>(s => s == settings.WorkingDirectory), It.IsAny<bool>(), It.IsAny<bool>())).Returns(1);

            cakeContext.ElectronNetBuildCustom(settings);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
            commandBuilderMock.Verify(builder => builder.SwitchHelper(It.IsAny<string[]>()), Times.Once);
        }
    }
}
