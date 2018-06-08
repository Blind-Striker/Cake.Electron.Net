using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cake.Electron.Net.Tests
{
    public class CommandBuilderTests
    {
        [Fact]
        public void SwitchHelper_Should_Return_Empty_String_If_Args_Is_Null_Or_Empty()
        {
            CommandBuilder commandBuilder = new CommandBuilder();

            string cmd = commandBuilder.SwitchHelper(null);

            Assert.Empty(cmd);
        }

        [Fact]
        public void SwitchHelper_Should_Return_Formatted_String_If_Args_Is_Filled_As_Key_Value()
        {
            CommandBuilder commandBuilder = new CommandBuilder();

            string[] args = {"foo=bar", "command=conquer"};

            string cmd = commandBuilder.SwitchHelper(args);

            Assert.NotNull(cmd);
            Assert.Contains($"--{args[0]}", cmd);
            Assert.Contains($"--{args[1]}", cmd);
        }
    }
}
