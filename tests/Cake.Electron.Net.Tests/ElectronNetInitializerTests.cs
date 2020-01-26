using Cake.Core;
using Cake.Electron.Net.Commands;
using Cake.Electron.Net.Commands.Settings;
using Cake.Electron.Net.Contracts;
using Cake.Electron.Net.Tests.Mocks;
using Cake.Electron.Net.Utils;

using Moq;

using System;

using Xunit;

namespace Cake.Electron.Net.Tests
{
    public class ElectronNetInitializerTests
    {
        private const string CmdBase = "electronize init";

        private readonly Mock<ICakeContext> _cakeContextMock;

        public ElectronNetInitializerTests()
        {
            _cakeContextMock = new Mock<ICakeContext>(MockBehavior.Strict);
        }

        [Fact]
        public void ElectronNetInit_Should_Throw_ArgumentNullException_If_WorkingDirectory_Is_Null()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            Assert.Throws<ArgumentNullException>(() => cakeContext.ElectronNetInit(null, null));
        }

        [Fact]
        public void ElectronNetInit_Should_Execute_Command_By_Given_WorkingDirectory()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            var workingDirectory = "./SomeDirectory";

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            ElectronCakeContext.Current = new TestCakeContext(processHelperMock, null);

            processHelperMock
                .Setup(helper => helper.CmdExecute(It.Is<string>(s => s == CmdBase), It.Is<string>(s => s == workingDirectory), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(1);

            cakeContext.ElectronNetInit(workingDirectory);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()));
        }

        [Fact]
        public void ElectronNetInit_Should_Execute_Command_By_Given_WorkingDirectory_And_AspNetCorePath()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            var workingDirectory = "./SomeDirectory";
            var aspCoreProjectPath = "./OtherPath";

            string expectedCommand = $"{CmdBase} /project-path {aspCoreProjectPath}";

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            ElectronCakeContext.Current = new TestCakeContext(processHelperMock, null);
            processHelperMock.Setup(helper => helper.CmdExecute(It.Is<string>(s => s == expectedCommand), It.Is<string>(s => s == workingDirectory), It.IsAny<bool>(),
                                                                It.IsAny<bool>()))
                             .Returns(1);

            cakeContext.ElectronNetInit(workingDirectory, aspCoreProjectPath);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public void ElectronNetInit_Should_Execute_Command_By_Given_WorkingDirectory_And_AspNetCorePath_And_Manifest()
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

            cakeContext.ElectronNetInit(workingDirectory, aspCoreProjectPath, manifest);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public void ElectronNetInit_Should_Throw_ArgumentNullException_If_Settings_Is_Null()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            Assert.Throws<ArgumentNullException>(() => cakeContext.ElectronNetInit(null));
        }

        [Fact]
        public void ElectronNetInit_Should_Execute_Command_By_Given_ElectronNetInitSettings()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            var electronNetInitSettings = new ElectronNetInitSettings() {WorkingDirectory = "./SomeDirectory", AspCoreProjectPath = "./OtherPath", Manifest = "test"};

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            ElectronCakeContext.Current = new TestCakeContext(processHelperMock, null);
            processHelperMock
                .Setup(helper => helper.CmdExecute(
                           It.Is<string>(s => s == $"{CmdBase} /project-path {electronNetInitSettings.AspCoreProjectPath} /manifest {electronNetInitSettings.Manifest}"),
                           It.Is<string>(s => s == electronNetInitSettings.WorkingDirectory), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(1);

            cakeContext.ElectronNetInit(electronNetInitSettings);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
        }
    }
}