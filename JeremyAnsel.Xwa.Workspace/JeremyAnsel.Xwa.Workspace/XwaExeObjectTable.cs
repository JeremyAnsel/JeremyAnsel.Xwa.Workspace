namespace JeremyAnsel.Xwa.Workspace
{
    public sealed class XwaExeObjectTable
    {
        public const int EntryOffset = 0x1F9E40;

        public const int EntryCount = 557;

        public XwaExeObjectTable()
        {
        }

        public XwaExeObjectTable(string path)
        {
            XwaExeVersion.Match(path);

            using (var filestream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                filestream.Seek(EntryOffset, SeekOrigin.Begin);
                var entryBytes = new byte[XwaExeObjectEntry.EntryLength];

                for (int index = 0; index < EntryCount; index++)
                {
                    filestream.Read(entryBytes, 0, XwaExeObjectEntry.EntryLength);
                    var entry = new XwaExeObjectEntry(entryBytes);
                    Entries.Add(entry);
                }
            }
        }

        public List<XwaExeObjectEntry> Entries { get; } = new List<XwaExeObjectEntry>(EntryCount);

        public void Write(string path)
        {
            XwaExeVersion.Match(path);

            using (var filestream = new FileStream(path, FileMode.Open, FileAccess.Write))
            {
                filestream.Seek(EntryOffset, SeekOrigin.Begin);

                int entryCount = Math.Min(this.Entries.Count, EntryCount);

                for (int index = 0; index < entryCount; index++)
                {
                    filestream.Write(this.Entries[index].ToByteArray(), 0, XwaExeObjectEntry.EntryLength);
                }

                byte[] empty = new XwaExeObjectEntry().ToByteArray();

                for (int index = entryCount; index < EntryCount; index++)
                {
                    filestream.Write(empty, 0, XwaExeObjectEntry.EntryLength);
                }
            }
        }
    }
}
