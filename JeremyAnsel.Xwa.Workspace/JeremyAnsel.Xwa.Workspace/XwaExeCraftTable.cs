namespace JeremyAnsel.Xwa.Workspace
{
    public sealed class XwaExeCraftTable
    {
        public const int EntryOffset = 0x1BA080;

        public const int EntryCount = 265;

        public XwaExeCraftTable()
        {
        }

        public XwaExeCraftTable(string path)
        {
            XwaExeVersion.Match(path);

            using (var filestream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                filestream.Seek(EntryOffset, SeekOrigin.Begin);
                var entryBytes = new byte[XwaExeCraftEntry.EntryLength];

                for (int index = 0; index < EntryCount; index++)
                {
                    filestream.Read(entryBytes, 0, XwaExeCraftEntry.EntryLength);
                    var entry = new XwaExeCraftEntry(entryBytes);
                    Entries.Add(entry);
                }
            }
        }

        public List<XwaExeCraftEntry> Entries { get; } = new List<XwaExeCraftEntry>(EntryCount);

        public void Write(string path)
        {
            XwaExeVersion.Match(path);

            using (var filestream = new FileStream(path, FileMode.Open, FileAccess.Write))
            {
                filestream.Seek(EntryOffset, SeekOrigin.Begin);

                int entryCount = Math.Min(this.Entries.Count, EntryCount);

                for (int index = 0; index < entryCount; index++)
                {
                    filestream.Write(this.Entries[index].ToByteArray(), 0, XwaExeCraftEntry.EntryLength);
                }

                byte[] empty = new XwaExeCraftEntry().ToByteArray();

                for (int index = entryCount; index < EntryCount; index++)
                {
                    filestream.Write(empty, 0, XwaExeCraftEntry.EntryLength);
                }
            }
        }
    }
}
