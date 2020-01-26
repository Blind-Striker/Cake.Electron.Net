using Cake.Electron.Net.Contracts;
using Cake.Electron.Net.Utils;

using Moq;

namespace Cake.Electron.Net.Tests.Mocks
{
    public sealed class TestCakeContext : ElectronCakeContext
    {
        private readonly Mock<ICommandBuilder> _commandBuilderMock;
        private readonly Mock<IProcessHelper> _processHelperMock;

        public TestCakeContext(Mock<IProcessHelper> processHelperMock, Mock<ICommandBuilder> commandBuilderMock)
        {
            _processHelperMock = processHelperMock;
            _commandBuilderMock = commandBuilderMock;
        }

        public override IProcessHelper ProcessHelper => _processHelperMock.Object;

        public override ICommandBuilder CommandBuilder => _commandBuilderMock.Object;
    }
}