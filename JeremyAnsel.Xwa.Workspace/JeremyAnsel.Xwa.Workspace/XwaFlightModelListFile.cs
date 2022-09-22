using System.Text;

namespace JeremyAnsel.Xwa.Workspace
{
    public sealed class XwaFlightModelListFile
    {
        private static readonly Encoding _encoding = Encoding.GetEncoding("iso-8859-1");

        public XwaFlightModelListFile()
        {
        }

        public XwaFlightModelListFile(string path)
        {
            string[] lines = File.ReadAllLines(path, _encoding);

            foreach (string line in lines)
            {
                var entry = new XwaFlightModelListEntry
                {
                    Value = line
                };

                this.Entries.Add(entry);
            }
        }

        public List<XwaFlightModelListEntry> Entries { get; } = new List<XwaFlightModelListEntry>();

        public void Write(string path)
        {
            var lines = new string[this.Entries.Count];

            for (int index = 0; index < this.Entries.Count; index++)
            {
                var entry = this.Entries[index];
                lines[index] = entry.Value;
            }

            File.WriteAllLines(path, lines, _encoding);
        }

        public int GetOrAddEntry(string value)
        {
            int index = this.Entries.FindIndex(t => t.Value == value);

            if (index == -1)
            {
                this.Entries.Add(new XwaFlightModelListEntry
                {
                    Value = value
                });

                index = this.Entries.Count - 1;
            }

            return index;
        }
    }
}
