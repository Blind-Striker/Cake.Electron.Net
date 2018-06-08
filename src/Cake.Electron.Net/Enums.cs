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
        public static readonly ElectronTargetCustom Win7WinSrv2008R2X86 = new ElectronTargetCustom("Win7WinSrv2008R2X86", "win7-x86");
        public static readonly ElectronTargetCustom Win7WinSrv2008R2X64 = new ElectronTargetCustom("Win7WinSrv2008R2X64", "win7-x64");
        public static readonly ElectronTargetCustom Win8WinSrv2012X86 = new ElectronTargetCustom("Win8WinSrv2012X86", "win8-x86");
        public static readonly ElectronTargetCustom Win8WinSrv2012X64 = new ElectronTargetCustom("Win8WinSrv2012X64", "win8-x64");
        public static readonly ElectronTargetCustom Win8WinSrv2012Arm = new ElectronTargetCustom("Win8WinSrv2012Arm", "win8-arm");
        public static readonly ElectronTargetCustom Win81WinSrv2012R2X86 = new ElectronTargetCustom("Win81WinSrv2012R2X86", "win81-x86");
        public static readonly ElectronTargetCustom Win81WinSrv2012R2X64 = new ElectronTargetCustom("Win81WinSrv2012R2X64", "win81-x64");
        public static readonly ElectronTargetCustom Win81WinSrv2012R2Arm = new ElectronTargetCustom("Win81WinSrv2012R2Arm", "win81-arm");
        public static readonly ElectronTargetCustom Win10WinSrv2016X86 = new ElectronTargetCustom("Win10WinSrv2016X86", "win10-x64");
        public static readonly ElectronTargetCustom Win10WinSrv2016X64 = new ElectronTargetCustom("Win10WinSrv2016X64", "win10-x86");
        public static readonly ElectronTargetCustom Win10WinSrv2016Arm = new ElectronTargetCustom("Win10WinSrv2016Arm", "win10-arm");
        public static readonly ElectronTargetCustom Win10WinSrv2016Arm64 = new ElectronTargetCustom("Win10WinSrv2016Arm64", "win10-arm64");

        public static readonly ElectronTargetCustom PortableLinuxX64 = new ElectronTargetCustom("PortableLinuxX64", "linux-x64");
        public static readonly ElectronTargetCustom CentOsX64 = new ElectronTargetCustom("CentOsX64", "centos-x64");
        public static readonly ElectronTargetCustom CentOs7X64 = new ElectronTargetCustom("CentOs7X64", "centos.7-x64");
        public static readonly ElectronTargetCustom DebianX64 = new ElectronTargetCustom("DebianX64", "debian-x64");
        public static readonly ElectronTargetCustom Debian8X64 = new ElectronTargetCustom("Debian8X64", "debian.8-x64");
        public static readonly ElectronTargetCustom FedoraX64 = new ElectronTargetCustom("FedoraX64", "fedora-x64");
        public static readonly ElectronTargetCustom Fedora24X64 = new ElectronTargetCustom("Fedora24X64", "fedora.24-x64");
        public static readonly ElectronTargetCustom Fedora25X64 = new ElectronTargetCustom("Fedora25X64", "fedora.25-x64");
        public static readonly ElectronTargetCustom Fedora26X64 = new ElectronTargetCustom("Fedora26X64", "fedora.26-x64");
        public static readonly ElectronTargetCustom GentooX64 = new ElectronTargetCustom("GentooX64", "gentoo-x64");
        public static readonly ElectronTargetCustom OpenSuseX64 = new ElectronTargetCustom("OpenSuseX64", "opensuse-x64");
        public static readonly ElectronTargetCustom OpenSuse42X64 = new ElectronTargetCustom("OpenSuse42X64", "opensuse.42.1-x64");
        public static readonly ElectronTargetCustom OracleLinuxX64 = new ElectronTargetCustom("OracleLinuxX64", "ol-x64");
        public static readonly ElectronTargetCustom OracleLinux7X64 = new ElectronTargetCustom("OracleLinux7X64", "ol.7-x64");
        public static readonly ElectronTargetCustom OracleLinux70X64 = new ElectronTargetCustom("OracleLinux70X64", "ol.7.0-x64");
        public static readonly ElectronTargetCustom OracleLinux71X64 = new ElectronTargetCustom("OracleLinux71X64", "ol.7.1-x64");
        public static readonly ElectronTargetCustom OracleLinux72X64 = new ElectronTargetCustom("OracleLinux72X64", "ol.7.2-x64");
        public static readonly ElectronTargetCustom RedHatX64 = new ElectronTargetCustom("RedHatX64", "rhel-x64");
        public static readonly ElectronTargetCustom RedHat6X64 = new ElectronTargetCustom("RedHat6X64", "rhel.6-x64");
        public static readonly ElectronTargetCustom RedHat7X64 = new ElectronTargetCustom("RedHat7X64", "rhel.7-x64");
        public static readonly ElectronTargetCustom RedHat71X64 = new ElectronTargetCustom("RedHat71X64", "rhel.7.1-x64");
        public static readonly ElectronTargetCustom RedHat72X64 = new ElectronTargetCustom("RedHat72X64", "rhel.7.2-x64");
        public static readonly ElectronTargetCustom RedHat73X64 = new ElectronTargetCustom("RedHat73X64", "rhel.7.3-x64");
        public static readonly ElectronTargetCustom RedHat74X64 = new ElectronTargetCustom("RedHat74X64", "rhel.7.4-x64");
        public static readonly ElectronTargetCustom Tizen = new ElectronTargetCustom("Tizen", "tizen");
        public static readonly ElectronTargetCustom UbuntuX64 = new ElectronTargetCustom("UbuntuX64", "ubuntu-x64");
        public static readonly ElectronTargetCustom Ubuntu1404X64 = new ElectronTargetCustom("Ubuntu1404X64", "ubuntu.14.04-x64");
        public static readonly ElectronTargetCustom Ubuntu1410X64 = new ElectronTargetCustom("Ubuntu1410X64", "ubuntu.14.10-x64");
        public static readonly ElectronTargetCustom Ubuntu1504X64 = new ElectronTargetCustom("Ubuntu1504X64", "ubuntu.15.04-x64");
        public static readonly ElectronTargetCustom Ubuntu1510X64 = new ElectronTargetCustom("Ubuntu1510X64", "ubuntu.15.10-x64");
        public static readonly ElectronTargetCustom Ubuntu1604X64 = new ElectronTargetCustom("Ubuntu1604X64", "ubuntu.16.04-x64");
        public static readonly ElectronTargetCustom Ubuntu1610X64 = new ElectronTargetCustom("Ubuntu1610X64", "ubuntu.16.10-x64");
        public static readonly ElectronTargetCustom LinuxMint17X64 = new ElectronTargetCustom("LinuxMint17X64", "linuxmint.17-x64");
        public static readonly ElectronTargetCustom LinuxMint171X64 = new ElectronTargetCustom("LinuxMint171X64", "linuxmint.17.1-x64");
        public static readonly ElectronTargetCustom LinuxMint172X64 = new ElectronTargetCustom("LinuxMint172X64", "linuxmint.17.2-x64");
        public static readonly ElectronTargetCustom LinuxMint173X64 = new ElectronTargetCustom("LinuxMint173X64", "linuxmint.17.3-x64");
        public static readonly ElectronTargetCustom LinuxMint18X64 = new ElectronTargetCustom("LinuxMint18X64", "linuxmint.18-x64");
        public static readonly ElectronTargetCustom LinuxMint181X64 = new ElectronTargetCustom("LinuxMint181X64", "linuxmint.18.1-x64");

        public static readonly ElectronTargetCustom MacOsX64 = new ElectronTargetCustom("MacOsX64", "osx-x64");
        public static readonly ElectronTargetCustom MacOs1010X64 = new ElectronTargetCustom("MacOs1010X64", "osx.10.10-x64");
        public static readonly ElectronTargetCustom MacOs1011X64 = new ElectronTargetCustom("MacOs1011X64", "osx.10.11-x64");
        public static readonly ElectronTargetCustom MacOs1012X64 = new ElectronTargetCustom("MacOs1012X64", "osx.10.12-x64");
        public static readonly ElectronTargetCustom MacOs1013X64 = new ElectronTargetCustom("MacOs1013X64", "osx.10.13-x64");

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

    public enum DotNetConfig
    {
        Debug,
        Release
    }
}
