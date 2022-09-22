using System.Globalization;
using System.Text;

namespace JeremyAnsel.Xwa.Workspace
{
    public static class XwaExeVersion
    {
        private const int VersionOffset = 0x200E19;

        public const string Version = @"X-Wing Alliance\V2.0";

        public static bool IsMatch(string path)
        {
            string version;

            using (var filestream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                if (filestream.Length < VersionOffset + Version.Length)
                {
                    return false;
                }

                filestream.Seek(VersionOffset, SeekOrigin.Begin);

                var bytes = new byte[Version.Length];
                filestream.Read(bytes, 0, bytes.Length);

                version = Encoding.ASCII.GetString(bytes);
            }

            return string.Equals(version, Version, StringComparison.Ordinal);
        }

        public static void Match(string path)
        {
            if (!IsMatch(path))
            {
                throw new ArgumentException(
                    string.Format(CultureInfo.InvariantCulture, "{0} was not found in {1}", Version, path),
                    nameof(path));
            }
        }
    }
}
