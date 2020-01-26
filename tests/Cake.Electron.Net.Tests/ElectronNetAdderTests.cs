using System;

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
    public class ElectronNetAdderTests
    {
        private const string CmdBase = "electronize add";

        private readonly Mock<ICakeContext> _cakeContextMock;

        public ElectronNetAdderTests()
        {
            _cakeContextMock = new Mock<ICakeContext>(MockBehavior.Strict);
        }

        [Fact]
        public void ElectronNetAdd_Should_Throw_ArgumentNullException_If_WorkingDirectory_Is_Null()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            Assert.Throws<ArgumentNullException>(() => cakeContext.ElectronNetAdd(null, null));
        }

        [Fact]
        public void ElectronNetAdd_Should_Throw_ArgumentNullException_If_Add_Is_Null_Or_Empty()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            Assert.Throws<ArgumentNullException>(() => cakeContext.ElectronNetAdd(".", null));
            Assert.Throws<ArgumentNullException>(() => cakeContext.ElectronNetAdd(".", string.Empty));
        }

        [Fact]
        public void ElectronNetAdd_Should_Execute_Command_By_Given_WorkingDirectory_And_Add()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            const string workingDirectory = "./SomeDirectory";
            const string add = "hosthook";

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            ElectronCakeContext.Current = new TestCakeContext(processHelperMock, null);
            processHelperMock.Setup(helper => helper.CmdExecute(It.Is<string>(s => s == $"{CmdBase} {add}"), It.Is<string>(s => s == workingDirectory), It.IsAny<bool>(),
                                                                It.IsAny<bool>()))
                             .Returns(1);

            cakeContext.ElectronNetAdd(workingDirectory, add);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public void ElectronNetAdd_Should_Throw_ArgumentNullException_If_Settings_Is_Null()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            Assert.Throws<ArgumentNullException>(() => cakeContext.ElectronNetAdd(null));
        }

        [Fact]
        public void ElectronNetInit_Should_Execute_Command_By_Given_ElectronNetInitSettings()
        {
            ICakeContext cakeContext = _cakeContextMock.Object;

            var electronNetInitSettings = new ElectronNetAddSettings() {WorkingDirectory = "./SomeDirectory", Add = ElectronAdd.ElectronHostHook};

            var processHelperMock = new Mock<IProcessHelper>(MockBehavior.Strict);
            ElectronCakeContext.Current = new TestCakeContext(processHelperMock, null);
            processHelperMock.Setup(helper => helper.CmdExecute(It.Is<string>(s => s == $"{CmdBase} {electronNetInitSettings.Add}"),
                                                                It.Is<string>(s => s == electronNetInitSettings.WorkingDirectory), It.IsAny<bool>(), It.IsAny<bool>()))
                             .Returns(1);

            cakeContext.ElectronNetAdd(electronNetInitSettings);

            processHelperMock.Verify(helper => helper.CmdExecute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
        }
    }
}