using Cake.Electron.Net.Contracts;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Cake.Electron.Net.Utils
{
    internal class ProcessHelper : IProcessHelper
    {
        public int CmdExecute(string command, string workingDirectoryPath, bool output = true, bool waitForExit = true)
        {
            using (var cmd = new Process())
            {
#if NETSTANDARD
                var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

                cmd.StartInfo.FileName = isWindows ? "cmd.exe" : "bash";
#else
                cmd.StartInfo.FileName = "cmd.exe";
#endif
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.RedirectStandardError = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.StartInfo.WorkingDirectory = workingDirectoryPath;

                var returnCode = 0;

                if (output)
                {
                    cmd.OutputDataReceived += (s, e) =>
                    {
                        if (e == null || string.IsNullOrWhiteSpace(e.Data))
                        {
                            return;
                        }

                        if (e.Data.ToLowerInvariant().Contains("error"))
                        {
                            returnCode = 1;
                        }

                        Console.WriteLine(e.Data);
                    };

                    cmd.ErrorDataReceived += (s, e) =>
                    {
                        if (e == null || string.IsNullOrWhiteSpace(e.Data))
                        {
                            return;
                        }

                        if (e.Data.ToLowerInvariant().Contains("error"))
                        {
                            returnCode = 1;
                        }

                        Console.WriteLine(e.Data);
                    };
                }

                cmd.Start();
                cmd.BeginOutputReadLine();
                cmd.BeginErrorReadLine();

                cmd.StandardInput.WriteLine(command);
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();

                if (waitForExit)
                {
                    cmd.WaitForExit();
                }

                return returnCode;
            }
        }
    }
}