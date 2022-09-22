using System.Globalization;
using System.Text;

namespace JeremyAnsel.Xwa.Workspace
{
    public sealed class XwaCraftPluralNameFile
    {
        public const int EntryCount = 221;

        private static readonly Encoding _encoding = Encoding.GetEncoding("iso-8859-1");

        private const int StartLineIndex = 2292;

        public XwaCraftPluralNameFile()
        {
        }

        public XwaCraftPluralNameFile(string path)
            : this(File.ReadAllLines(path, _encoding))
        {
        }

        public XwaCraftPluralNameFile(string[] lines)
        {
            for (int index = StartLineIndex; index < StartLineIndex + EntryCount; index++)
            {
                string line = lines[index];
                string key = line.Substring(1, line.IndexOf('!', 1) - 1);
                string name = line.Substring(key.Length + 2);

                if (key.EndsWith("_PLURAL", StringComparison.OrdinalIgnoreCase))
                {
                    key = key.Substring(0, key.Length - "_PLURAL".Length);
                }

                var entry = new XwaCraftPluralNameEntry
                {
                    Key = key,
                    CraftPluralName = name
                };

                this.Entries.Add(entry);
            }
        }

        public List<XwaCraftPluralNameEntry> Entries { get; } = new List<XwaCraftPluralNameEntry>();

        public void Write(string path)
        {
            string[] lines = File.ReadAllLines(path, _encoding);
            this.Write(lines);
            File.WriteAllLines(path, lines, _encoding);
        }

        public void Write(string[] lines)
        {
            int entryCount = Math.Min(this.Entries.Count, EntryCount);

            for (int index = 0; index < entryCount; index++)
            {
                var entry = this.Entries[index];

                if (string.IsNullOrEmpty(entry.Key))
                {
                    entry.Key = "KSPEC" + (index + 1).ToString(CultureInfo.InvariantCulture);
                }

                lines[StartLineIndex + index] = string.Format(CultureInfo.InvariantCulture, "!{0}_PLURAL!{1}", entry.Key, entry.CraftPluralName);
            }

            for (int index = entryCount; index < EntryCount; index++)
            {
                lines[StartLineIndex + index] = string.Format(CultureInfo.InvariantCulture, "!KSPEC{0}_PLURAL!", index + 1);
            }
        }

        public XwaCraftPluralNameEntry GetOrAddEntry(int index)
        {
            if (index < 0 || index >= EntryCount)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            while (index >= this.Entries.Count)
            {
                this.Entries.Add(new XwaCraftPluralNameEntry());
            }

            return this.Entries[index];
        }
    }
}
