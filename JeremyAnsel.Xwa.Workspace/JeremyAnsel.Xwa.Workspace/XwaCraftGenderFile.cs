using System.Globalization;
using System.Text;

namespace JeremyAnsel.Xwa.Workspace
{
    public sealed class XwaCraftGenderFile
    {
        public const int EntryCount = 221;

        private static readonly Encoding _encoding = Encoding.GetEncoding("iso-8859-1");

        private const int StartLineIndex = 2064;

        public XwaCraftGenderFile()
        {
        }

        public XwaCraftGenderFile(string path)
            : this(File.ReadAllLines(path, _encoding))
        {
        }

        public XwaCraftGenderFile(string[] lines)
        {
            for (int index = StartLineIndex; index < StartLineIndex + EntryCount; index++)
            {
                string line = lines[index];
                string key = line.Substring(1, line.IndexOf('!', 1) - 1);

                var entry = new XwaCraftGenderEntry
                {
                    Key = key
                };

                char gender = char.ToUpperInvariant(line[key.Length + 2]);

                entry.CraftGender = gender switch
                {
                    'M' => XwaCraftGender.Masculine,
                    'F' => XwaCraftGender.Feminine,
                    'N' => XwaCraftGender.Neutral,
                    _ => XwaCraftGender.Masculine,
                };

                entry.CraftName = line.Substring(key.Length + 4);

                this.Entries.Add(entry);
            }
        }

        public List<XwaCraftGenderEntry> Entries { get; } = new List<XwaCraftGenderEntry>();

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

                var gender = entry.CraftGender switch
                {
                    XwaCraftGender.Masculine => 'm',
                    XwaCraftGender.Feminine => 'f',
                    XwaCraftGender.Neutral => 'n',
                    _ => 'm',
                };

                lines[StartLineIndex + index] = string.Format(CultureInfo.InvariantCulture, "!{0}!{1}:{2}", entry.Key, gender, entry.CraftName);
            }

            for (int index = entryCount; index < EntryCount; index++)
            {
                lines[StartLineIndex + index] = string.Format(CultureInfo.InvariantCulture, "!KSPEC{0}!m:", index + 1);
            }
        }

        public XwaCraftGenderEntry GetOrAddEntry(int index)
        {
            if (index < 0 || index >= EntryCount)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            while (index >= this.Entries.Count)
            {
                this.Entries.Add(new XwaCraftGenderEntry());
            }

            return this.Entries[index];
        }
    }
}
