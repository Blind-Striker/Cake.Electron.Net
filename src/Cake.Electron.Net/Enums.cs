namespace Cake.Electron.Net
{
    public sealed class ElectronTarget
    {
        public static readonly ElectronTarget Win = new ElectronTarget("Windows", "win");
        public static readonly ElectronTarget Linux = new ElectronTarget("Linux", "linux");
        public static readonly ElectronTarget MacOs = new ElectronTarget("MacOs", "osx");

        private ElectronTarget(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public string Value { get; }

        public override string ToString()
        {
            return Value;
        }
    }

    public sealed class ElectronTargetCustom
    {
        public static readonly ElectronTargetCustom PortableWinX86 = new ElectronTargetCustom("PortableWinX86", "win-x86");
        public static readonly ElectronTargetCustom PortableWinX64 = new ElectronTargetCustom("PortableWinX64", "win-x64");
        public static readonly ElectronTargetCustom PortableWinArm = new ElectronTargetCustom("PortableWinArm", "win-arm");
        public static readonly ElectronTargetCustom PortableWinArm64 = new ElectronTargetCustom("PortableWinArm64", "win-arm64");

        public static readonly ElectronTargetCustom Win7WinSrv2008R2X86 = new ElectronTargetCustom("Win7WinSrv2008R2X86", "win7-x86");
        public static readonly ElectronTargetCustom Win7WinSrv2008R2X64 = new ElectronTargetCustom("Win7WinSrv2008R2X64", "win7-x64");

        public static readonly ElectronTargetCustom Win81WinSrv2012R2X86 = new ElectronTargetCustom("Win81WinSrv2012R2X86", "win81-x86");
        public static readonly ElectronTargetCustom Win81WinSrv2012R2X64 = new ElectronTargetCustom("Win81WinSrv2012R2X64", "win81-x64");
        public static readonly ElectronTargetCustom Win81WinSrv2012R2Arm = new ElectronTargetCustom("Win81WinSrv2012R2Arm", "win81-arm");

        public static readonly ElectronTargetCustom Win10WinSrv2016X86 = new ElectronTargetCustom("Win10WinSrv2016X86", "win10-x64");
        public static readonly ElectronTargetCustom Win10WinSrv2016X64 = new ElectronTargetCustom("Win10WinSrv2016X64", "win10-x86");
        public static readonly ElectronTargetCustom Win10WinSrv2016Arm = new ElectronTargetCustom("Win10WinSrv2016Arm", "win10-arm");
        public static readonly ElectronTargetCustom Win10WinSrv2016Arm64 = new ElectronTargetCustom("Win10WinSrv2016Arm64", "win10-arm64");

        public static readonly ElectronTargetCustom PortableLinuxX64 = new ElectronTargetCustom("PortableLinuxX64", "linux-x64");
        public static readonly ElectronTargetCustom PortableLinuxMuslX64 = new ElectronTargetCustom("PortableLinuxMuslX64", "linux-musl-x64");
        public static readonly ElectronTargetCustom PortableLinuxArm = new ElectronTargetCustom("PortableLinuxArm", "linux-arm");

        public static readonly ElectronTargetCustom RedHatX64 = new ElectronTargetCustom("RedHatX64", "rhel-x64");
        public static readonly ElectronTargetCustom RedHat6X64 = new ElectronTargetCustom("RedHat6X64", "rhel.6-x64");

        public static readonly ElectronTargetCustom Tizen = new ElectronTargetCustom("Tizen", "tizen");
        public static readonly ElectronTargetCustom Tizen4 = new ElectronTargetCustom("Tizen4", "tizen.4.0.0");
        public static readonly ElectronTargetCustom Tizen5 = new ElectronTargetCustom("Tizen5", "tizen.5.0.0");


        public static readonly ElectronTargetCustom MacOsX64 = new ElectronTargetCustom("MacOsX64", "osx-x64");
        public static readonly ElectronTargetCustom MacOs1010X64 = new ElectronTargetCustom("MacOs1010X64", "osx.10.10-x64");
        public static readonly ElectronTargetCustom MacOs1011X64 = new ElectronTargetCustom("MacOs1011X64", "osx.10.11-x64");
        public static readonly ElectronTargetCustom MacOs1012X64 = new ElectronTargetCustom("MacOs1012X64", "osx.10.12-x64");
        public static readonly ElectronTargetCustom MacOs1013X64 = new ElectronTargetCustom("MacOs1013X64", "osx.10.13-x64");
        public static readonly ElectronTargetCustom MacOs1014X64 = new ElectronTargetCustom("MacOs1014X64", "osx.10.14-x64");

        private ElectronTargetCustom(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public string Value { get; }

        public override string ToString()
        {
            return Value;
        }
    }

    public sealed class ElectronAdd
    {
        public static readonly ElectronAdd ElectronHostHook = new ElectronAdd("ElectronHostHook", "hosthook");

        private ElectronAdd(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public string Value { get; }

        public override string ToString()
        {
            return Value;
        }
    }

    public enum DotNetConfig
    {
        Debug,
        Release
    }
}