namespace JeremyAnsel.Xwa.Workspace
{
    public sealed class XwaExeSpeciesTable
    {
        public const int EntryOffset = 0x1AFB70;

        public const int EntryCount = 233;

        public XwaExeSpeciesTable()
        {
        }

        public XwaExeSpeciesTable(string path)
        {
            XwaExeVersion.Match(path);

            using (var filestream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                filestream.Seek(EntryOffset, SeekOrigin.Begin);
                var entryBytes = new byte[XwaExeSpeciesEntry.EntryLength];

                for (int index = 0; index < EntryCount; index++)
                {
                    filestream.Read(entryBytes, 0, XwaExeSpeciesEntry.EntryLength);
                    var entry = new XwaExeSpeciesEntry(entryBytes);
                    Entries.Add(entry);
                }
            }
        }

        public List<XwaExeSpeciesEntry> Entries { get; } = new List<XwaExeSpeciesEntry>(EntryCount);

        public void Write(string path)
        {
            XwaExeVersion.Match(path);

            using (var filestream = new FileStream(path, FileMode.Open, FileAccess.Write))
            {
                filestream.Seek(EntryOffset, SeekOrigin.Begin);

                int entryCount = Math.Min(this.Entries.Count, EntryCount);

                for (int index = 0; index < entryCount; index++)
                {
                    filestream.Write(this.Entries[index].ToByteArray(), 0, XwaExeSpeciesEntry.EntryLength);
                }

                byte[] empty = new XwaExeSpeciesEntry().ToByteArray();

                for (int index = entryCount; index < EntryCount; index++)
                {
                    filestream.Write(empty, 0, XwaExeSpeciesEntry.EntryLength);
                }
            }
        }
    }
}
