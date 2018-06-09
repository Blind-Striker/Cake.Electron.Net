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
    public class ElectronNetVersionPrinterTests
    {
        private const string CmdBase = "dotnet electronize version";
        private readonly Mock<ICakeContext> _cakeContextMock;

        public ElectronNetVersionPrinterTests()
        {
            _cakeContextMock = new Mock<ICakeContext>(MockBehavior.Strict);
        }

        [Fact]
        public void ElectronNetVersion_Should_Throw_ArgumentNullException_If_WorkingDirectory_Is_Null()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            Assert.Throws<ArgumentNullException>(() => cakeContext.ElectronNetVersion((string)null));
        }

        [Fact]
        public void ElectronNetVersion_Should_Execute_Command_By_Given_WorkingDirectory()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            string workingDirectory = "./SomeDirectory";

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            ElectroCakeContext.Current = new TestElectroCakeContext(processHelperMock, null);

            processHelperMock.Setup(helper => helper.CmdExecute(It.Is<string>(s => s == CmdBase), It.Is<string>(s => s == workingDirectory), It.IsAny<bool>(), It.IsAny<bool>())).Returns(1);

            cakeContext.ElectronNetVersion(workingDirectory);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()));
        }

        [Fact]
        public void ElectronNetVersion_Should_Throw_ArgumentNullException_If_Settings_Is_Null()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            Assert.Throws<ArgumentNullException>(() => cakeContext.ElectronNetVersion((ElectronNetVersionSettings)null));
        }

        [Fact]
        public void ElectronNetVersion_Should_Execute_Command_By_Given_ElectronNetInitSettings()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            ElectronNetVersionSettings electronNetInitSettings = new ElectronNetVersionSettings()
            {
                WorkingDirectory = "./SomeDirectory"
            };

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            ElectroCakeContext.Current = new TestElectroCakeContext(processHelperMock, null);

            processHelperMock.Setup(helper => helper.CmdExecute(It.Is<string>(s => s == CmdBase), It.Is<string>(s => s == electronNetInitSettings.WorkingDirectory), It.IsAny<bool>(), It.IsAny<bool>())).Returns(1);

            cakeContext.ElectronNetVersion(electronNetInitSettings);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
        }
    }
}