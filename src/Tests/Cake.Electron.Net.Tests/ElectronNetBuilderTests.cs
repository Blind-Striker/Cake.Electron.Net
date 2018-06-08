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
    public class ElectronNetBuilderTests
    {
        private const string CmdBase = "dotnet electronize build";

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

            Assert.Throws<ArgumentNullException>(() => cakeContext.ElectronNetBuild("./Electro", null, null));
        }

        [Fact]
        public void ElectronNetBuild_Should_Throw_ArgumentNullException_If_DotNetConfig_Is_Null()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            Assert.Throws<ArgumentNullException>(() => cakeContext.ElectronNetBuild("./Electro", "win", null));
        }

        [Fact]
        public void ElectronNetBuild_Should_Execute_Command_By_Given_WorkingDirectory_ElectronTarget()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            string workingDirectory = "./SomeDirectory";
            string electronTarget = "win";
            var expectedCommand = $"{CmdBase} /target {electronTarget} /dotnet-configuration Release";

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            var commandBuilderMock = new Mock<ICommandBuilder>(MockBehavior.Strict);
            ElectroCakeContext.Current = new TestElectroCakeContext(processHelperMock, commandBuilderMock);

            commandBuilderMock.Setup(builder => builder.SwitchHelper(It.IsAny<string[]>()));
            processHelperMock.Setup(helper => helper.CmdExecute(It.Is<string>(s => s == expectedCommand), It.Is<string>(s => s == workingDirectory), It.IsAny<bool>(), It.IsAny<bool>())).Returns(1);

            cakeContext.ElectronNetBuild(workingDirectory, electronTarget);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
            commandBuilderMock.Verify(builder => builder.SwitchHelper(It.IsAny<string[]>()), Times.Never());
        }

        [Fact]
        public void ElectronNetBuild_Should_Execute_Command_By_Given_WorkingDirectory_ElectronTarget_ElectronParams()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            string workingDirectory = "./SomeDirectory";
            string electronTarget = "win";
            string electronParams = "command=conquer";

            var expectedCommand = $"{CmdBase} /target {electronTarget} /dotnet-configuration Release /electron-params \"--command=conquer\"";

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            var commandBuilderMock = new Mock<ICommandBuilder>(MockBehavior.Strict);
            ElectroCakeContext.Current = new TestElectroCakeContext(processHelperMock, commandBuilderMock);

            commandBuilderMock
                .Setup(builder => builder.SwitchHelper(It.Is<string[]>(strings => strings.Contains(electronParams))))
                .Returns(() => "--command=conquer");

            processHelperMock.Setup(helper => helper.CmdExecute(It.Is<string>(s => s == expectedCommand), It.Is<string>(s => s == workingDirectory), It.IsAny<bool>(), It.IsAny<bool>())).Returns(1);

            cakeContext.ElectronNetBuild(workingDirectory: workingDirectory, electronTarget: electronTarget, dotNetConfig: "Release", electronParams: electronParams);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
            commandBuilderMock.Verify(builder => builder.SwitchHelper(It.IsAny<string[]>()), Times.Once);
        }

        [Fact]
        public void ElectronNetBuild_Should_Execute_Command_By_Given_ElectronNetBuildSettings()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            ElectronNetBuildSettings settings = new ElectronNetBuildSettings()
            {
                WorkingDirectory = "./SomeDirectory",
                DotNetConfig = DotNetConfig.Release,
                ElectronTarget = ElectronTarget.Linux,
                ElectronParams = new[] {"command=conquer"}
            };

            var expectedCommand = $"{CmdBase} /target {ElectronTarget.Linux.Value} /dotnet-configuration Release /electron-params \"--command=conquer\"";

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            var commandBuilderMock = new Mock<ICommandBuilder>(MockBehavior.Strict);
            ElectroCakeContext.Current = new TestElectroCakeContext(processHelperMock, commandBuilderMock);

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

            Assert.Throws<ArgumentNullException>(() => cakeContext.ElectronNetBuildCustom("./Electro", null, "ia32", "Release"));
        }

        [Fact]
        public void ElectronNetBuildCustom_Should_Throw_ArgumentNullException_If_ElectronArch_Is_Null()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            Assert.Throws<ArgumentNullException>(() => cakeContext.ElectronNetBuildCustom("./Electro", "win", null, "Debug"));
        }

        [Fact]
        public void ElectronNetBuildCustom_Should_Throw_ArgumentNullException_If_DotNetConfig_Is_Null()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            Assert.Throws<ArgumentNullException>(() => cakeContext.ElectronNetBuildCustom("./Electro", "win", "ia32", null));
        }

        [Fact]
        public void ElectronNetBuildCustom_Should_Execute_Command_By_Given_WorkingDirectory_ElectronTarget_ElectronArch()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            string workingDirectory = "./SomeDirectory";
            string electronTarget = "win";
            string electronArch = "ia32";

            var expectedCommand = $"{CmdBase} /target custom {electronTarget} /dotnet-configuration Release /electron-arch {electronArch}";

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            var commandBuilderMock = new Mock<ICommandBuilder>(MockBehavior.Strict);
            ElectroCakeContext.Current = new TestElectroCakeContext(processHelperMock, commandBuilderMock);

            processHelperMock.Setup(helper => helper.CmdExecute(It.Is<string>(s => s == expectedCommand), It.Is<string>(s => s == workingDirectory), It.IsAny<bool>(), It.IsAny<bool>())).Returns(1);

            cakeContext.ElectronNetBuildCustom(workingDirectory, electronTarget, electronArch);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
            commandBuilderMock.Verify(builder => builder.SwitchHelper(It.IsAny<string[]>()), Times.Never);
        }

        [Fact]
        public void ElectronNetBuildCustom_Should_Execute_Command_By_Given_WorkingDirectory_ElectronTarget_ElectronArch_ElectronParams()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            string workingDirectory = "./SomeDirectory";
            string electronTarget = "win";
            string electronArch = "ia32";
            string electronParams = "command=conquer";

            var expectedCommand = $"{CmdBase} /target custom {electronTarget} /dotnet-configuration Release /electron-arch {electronArch} /electron-params \"--command=conquer\"";

            var commandBuilderMock = new Mock<ICommandBuilder>(MockBehavior.Strict);
            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            ElectroCakeContext.Current = new TestElectroCakeContext(processHelperMock, commandBuilderMock);

            commandBuilderMock
                .Setup(builder => builder.SwitchHelper(It.Is<string[]>(strings => strings.Contains(electronParams))))
                .Returns(() => "--command=conquer");

            processHelperMock.Setup(helper => helper.CmdExecute(It.Is<string>(s => s == expectedCommand), It.Is<string>(s => s == workingDirectory), It.IsAny<bool>(), It.IsAny<bool>())).Returns(1);

            cakeContext.ElectronNetBuildCustom(workingDirectory, electronTarget, electronArch, "Release", electronParams);

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
            ElectroCakeContext.Current = new TestElectroCakeContext(processHelperMock, commandBuilderMock);

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
