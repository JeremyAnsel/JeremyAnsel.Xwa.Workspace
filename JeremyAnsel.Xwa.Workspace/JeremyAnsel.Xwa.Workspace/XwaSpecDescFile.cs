using System.Globalization;
using System.Text;

namespace JeremyAnsel.Xwa.Workspace
{
    public sealed class XwaSpecDescFile
    {
        public const int MaxEntryCount = 254;

        private static readonly Encoding _encoding = Encoding.GetEncoding("iso-8859-1");

        public XwaSpecDescFile()
        {
        }

        public XwaSpecDescFile(string path)
        {
            using (var file = new StreamReader(path, _encoding))
            {
                string line;
                int lineIndex = -1;
                var entry = new XwaSpecDescEntry();

                while ((line = file.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line) || line.StartsWith("//", StringComparison.Ordinal))
                    {
                        continue;
                    }

                    int valueIndex = line.IndexOf('!', 1);
                    //int idIndex1 = line.IndexOf('_', 1, valueIndex - 1);
                    //int idIndex2 = line.IndexOf('_', idIndex1 + 1, valueIndex - idIndex1 - 1);

                    //int id = int.Parse(line.Substring(idIndex1 + 1, idIndex2 - idIndex1 - 1), CultureInfo.InvariantCulture);
                    string value = line.Substring(valueIndex + 1).Trim();

                    lineIndex++;

                    switch (lineIndex)
                    {
                        case 0:
                            entry = new XwaSpecDescEntry
                            {
                                CraftLongName = value
                            };

                            break;

                        case 1:
                            entry.Manufacturer = value;
                            break;

                        case 2:
                            entry.Side = value;
                            break;

                        case 3:
                            entry.Description = value;
                            break;

                        case 4:
                            entry.Crew = value;
                            this.Entries.Add(entry);
                            lineIndex = -1;
                            break;
                    }
                }
            }
        }

        public List<XwaSpecDescEntry> Entries { get; } = new List<XwaSpecDescEntry>();

        public void Write(string path)
        {
            using (var file = new StreamWriter(path, false, _encoding))
            {
                file.WriteLine("// Species CMD name - 64 character limit");
                file.WriteLine("// Manufacturer - 64 character limit");
                file.WriteLine("// In use by - 64 character limit");
                file.WriteLine("// special characteristics - 256 character limit");
                file.WriteLine("// crew - 64 character limit");

                int entryCount = Math.Min(this.Entries.Count, MaxEntryCount);

                for (int index = 0; index < entryCount; index++)
                {
                    int id = index + 1;
                    var entry = this.Entries[index];

                    file.WriteLine(string.Format(CultureInfo.InvariantCulture, "!SPEC_{0}_NAME!{1}", id, entry.CraftLongName));
                    file.WriteLine(string.Format(CultureInfo.InvariantCulture, "!SPEC_{0}_MANU!{1}", id, entry.Manufacturer));
                    file.WriteLine(string.Format(CultureInfo.InvariantCulture, "!SPEC_{0}_SIDE!{1}", id, entry.Side));
                    file.WriteLine(string.Format(CultureInfo.InvariantCulture, "!SPEC_{0}_DESC!{1}", id, entry.Description));
                    file.WriteLine(string.Format(CultureInfo.InvariantCulture, "!SPEC_{0}_CREW!{1}", id, entry.Crew));
                }

                file.WriteLine();
            }
        }

        public XwaSpecDescEntry GetOrAddEntry(int index)
        {
            if (index < 0 || index >= MaxEntryCount)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            while (index >= this.Entries.Count)
            {
                this.Entries.Add(new XwaSpecDescEntry());
            }

            return this.Entries[index];
        }
    }
}
