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
    public class ElectronNetStarterTests
    {
        private const string CmdBase = "electronize start";

        private readonly Mock<ICakeContext> _cakeContextMock;

        public ElectronNetStarterTests()
        {
            _cakeContextMock = new Mock<ICakeContext>(MockBehavior.Strict);
        }

        [Fact]
        public void ElectronNetStart_Should_Throw_ArgumentNullException_If_WorkingDirectory_Is_Null()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            Assert.Throws<ArgumentNullException>(() => cakeContext.ElectronNetStart(null, null));
        }

        [Fact]
        public void ElectronNetStart_Should_Execute_Command_By_Given_WorkingDirectory()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            const string workingDirectory = "./SomeDirectory";

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            ElectronCakeContext.Current = new TestCakeContext(processHelperMock, null);

            processHelperMock
                .Setup(helper => helper.CmdExecute(It.Is<string>(s => s == CmdBase), It.Is<string>(s => s == workingDirectory), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(1);

            cakeContext.ElectronNetStart(workingDirectory);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public void ElectronNetStart_Should_Execute_Command_By_Given_WorkingDirectory_And_AspNetCorePath()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            const string workingDirectory = "./SomeDirectory";
            const string aspCoreProjectPath = "./OtherPath";

            string expectedCommand = $"{CmdBase} /project-path {aspCoreProjectPath}";

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            ElectronCakeContext.Current = new TestCakeContext(processHelperMock, null);

            processHelperMock.Setup(helper => helper.CmdExecute(It.Is<string>(s => s == expectedCommand), It.Is<string>(s => s == workingDirectory), It.IsAny<bool>(),
                                                                It.IsAny<bool>()))
                             .Returns(1);

            cakeContext.ElectronNetStart(workingDirectory, aspCoreProjectPath);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public void ElectronNetStart_Should_Execute_Command_By_Given_WorkingDirectory_And_AspNetCorePath_Manifest()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            const string workingDirectory = "./SomeDirectory";
            const string aspCoreProjectPath = "./OtherPath";
            const string manifest = "test";

            string expectedCommand = $"{CmdBase} /project-path {aspCoreProjectPath} /manifest {manifest}";

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            ElectronCakeContext.Current = new TestCakeContext(processHelperMock, null);

            processHelperMock.Setup(helper => helper.CmdExecute(It.Is<string>(s => s == expectedCommand), It.Is<string>(s => s == workingDirectory), It.IsAny<bool>(),
                                                                It.IsAny<bool>()))
                             .Returns(1);

            cakeContext.ElectronNetStart(workingDirectory, aspCoreProjectPath, manifest);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public void ElectronNetStart_Should_Execute_Command_By_Given_WorkingDirectory_And_AspNetCorePath_Manifest_And_Arguments()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            const string workingDirectory = "./SomeDirectory";
            const string aspCoreProjectPath = "./OtherPath";
            const string manifest = "test";
            const string arg1 = "test=true";
            const string arg2 = "dog=woof";

            string expectedCommand = $"{CmdBase} /project-path {aspCoreProjectPath} /manifest {manifest} /args --{arg1} --{arg2}";

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            var commandBuilderMock = new Mock<ICommandBuilder>(MockBehavior.Strict);
            ElectronCakeContext.Current = new TestCakeContext(processHelperMock, commandBuilderMock);

            commandBuilderMock.Setup(builder => builder.SwitchHelper(It.Is<string[]>(strings => strings.Contains(arg1) && strings.Contains(arg2))))
                              .Returns(() => $"--{arg1} --{arg2}");

            processHelperMock.Setup(helper => helper.CmdExecute(It.Is<string>(s => s == expectedCommand), It.Is<string>(s => s == workingDirectory), It.IsAny<bool>(),
                                                                It.IsAny<bool>()))
                             .Returns(1);

            cakeContext.ElectronNetStart(workingDirectory, aspCoreProjectPath, manifest, arg1, arg2);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public void ElectronNetStart_Should_Throw_ArgumentNullException_If_Settings_Is_Null()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            Assert.Throws<ArgumentNullException>(() => cakeContext.ElectronNetStart(null));
        }

        [Fact]
        public void ElectronNetStart_Should_Execute_Command_By_Given_ElectronNetStartSettings()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            var electronNetInitSettings = new ElectronNetStartSettings()
            {
                WorkingDirectory = "./SomeDirectory", AspCoreProjectPath = "./OtherPath", Manifest = "test", Arguments = new[] {"test=true", "dog=woof"}
            };

            string expectedCommand =
                $"{CmdBase} /project-path {electronNetInitSettings.AspCoreProjectPath} /manifest {electronNetInitSettings.Manifest} /args --{electronNetInitSettings.Arguments[0]} --{electronNetInitSettings.Arguments[1]}";

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            var commandBuilderMock = new Mock<ICommandBuilder>(MockBehavior.Strict);
            ElectronCakeContext.Current = new TestCakeContext(processHelperMock, commandBuilderMock);

            commandBuilderMock
                .Setup(builder => builder.SwitchHelper(
                           It.Is<string[]>(strings => strings.Contains(electronNetInitSettings.Arguments[0]) && strings.Contains(electronNetInitSettings.Arguments[1]))))
                .Returns(() => $"--{electronNetInitSettings.Arguments[0]} --{electronNetInitSettings.Arguments[1]}");

            processHelperMock.Setup(helper => helper.CmdExecute(It.Is<string>(s => s == expectedCommand),
                                                                It.Is<string>(s => s == electronNetInitSettings.WorkingDirectory), It.IsAny<bool>(), It.IsAny<bool>()))
                             .Returns(1);

            cakeContext.ElectronNetStart(electronNetInitSettings);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
        }
    }
}