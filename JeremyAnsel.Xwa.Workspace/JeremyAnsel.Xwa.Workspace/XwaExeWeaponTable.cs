namespace JeremyAnsel.Xwa.Workspace
{
    public sealed class XwaExeWeaponTable
    {
        public const int EntryCount = 28;

        public XwaExeWeaponTable()
        {
        }

        public XwaExeWeaponTable(string path)
        {
            XwaExeVersion.Match(path);

            using (var filestream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var bytes = new byte[4];

                for (int index = 0; index < EntryCount; index++)
                {
                    var entry = new XwaExeWeaponEntry();

                    filestream.Seek(0x1B50B8 + index * 4, SeekOrigin.Begin);
                    filestream.Read(bytes, 0, 4);
                    entry.Power = BitConverter.ToInt32(bytes, 0);

                    filestream.Seek(0x1B5128 + index * 2, SeekOrigin.Begin);
                    filestream.Read(bytes, 0, 2);
                    entry.Speed = BitConverter.ToInt16(bytes, 0);

                    filestream.Seek(0x1B5160 + index * 2, SeekOrigin.Begin);
                    filestream.Read(bytes, 0, 2);
                    entry.DurationIntegerPart = BitConverter.ToUInt16(bytes, 0);

                    filestream.Seek(0x1B5198 + index * 2, SeekOrigin.Begin);
                    filestream.Read(bytes, 0, 2);
                    entry.DurationDecimalPart = BitConverter.ToUInt16(bytes, 0);

                    filestream.Seek(0x1B51D0 + index * 2, SeekOrigin.Begin);
                    filestream.Read(bytes, 0, 2);
                    entry.HitboxSpan = BitConverter.ToInt16(bytes, 0);

                    filestream.Seek(0x1B5208 + index, SeekOrigin.Begin);
                    filestream.Read(bytes, 0, 1);
                    entry.Behavior = bytes[0];

                    filestream.Seek(0x1B5228 + index * 2, SeekOrigin.Begin);
                    filestream.Read(bytes, 0, 2);
                    entry.Score = BitConverter.ToInt16(bytes, 0);

                    filestream.Seek(0x1B5260 + index, SeekOrigin.Begin);
                    filestream.Read(bytes, 0, 1);
                    entry.Side = (sbyte)bytes[0];

                    filestream.Seek(0x1B5280 + index * 2, SeekOrigin.Begin);
                    filestream.Read(bytes, 0, 2);
                    entry.SideModel = BitConverter.ToInt16(bytes, 0);

                    Entries.Add(entry);
                }
            }
        }

        public List<XwaExeWeaponEntry> Entries { get; } = new List<XwaExeWeaponEntry>(EntryCount);

        public void Write(string path)
        {
            XwaExeVersion.Match(path);

            using (var filestream = new FileStream(path, FileMode.Open, FileAccess.Write))
            {
                int entryCount = Math.Min(this.Entries.Count, EntryCount);

                for (int index = 0; index < entryCount; index++)
                {
                    var entry = this.Entries[index];

                    filestream.Seek(0x1B50B8 + index * 4, SeekOrigin.Begin);
                    filestream.Write(BitConverter.GetBytes(entry.Power), 0, 4);

                    filestream.Seek(0x1B5128 + index * 2, SeekOrigin.Begin);
                    filestream.Write(BitConverter.GetBytes(entry.Speed), 0, 2);

                    filestream.Seek(0x1B5160 + index * 2, SeekOrigin.Begin);
                    filestream.Write(BitConverter.GetBytes(entry.DurationIntegerPart), 0, 2);

                    filestream.Seek(0x1B5198 + index * 2, SeekOrigin.Begin);
                    filestream.Write(BitConverter.GetBytes(entry.DurationDecimalPart), 0, 2);

                    filestream.Seek(0x1B51D0 + index * 2, SeekOrigin.Begin);
                    filestream.Write(BitConverter.GetBytes(entry.HitboxSpan), 0, 2);

                    filestream.Seek(0x1B5208 + index, SeekOrigin.Begin);
                    filestream.WriteByte(entry.Behavior);

                    filestream.Seek(0x1B5228 + index * 2, SeekOrigin.Begin);
                    filestream.Write(BitConverter.GetBytes(entry.Score), 0, 2);

                    filestream.Seek(0x1B5260 + index, SeekOrigin.Begin);
                    filestream.WriteByte((byte)entry.Side);

                    filestream.Seek(0x1B5280 + index * 2, SeekOrigin.Begin);
                    filestream.Write(BitConverter.GetBytes(entry.SideModel), 0, 2);
                }

                byte[] empty = new byte[4];

                for (int index = entryCount; index < EntryCount; index++)
                {
                    filestream.Seek(0x1B50B8 + index * 4, SeekOrigin.Begin);
                    filestream.Write(empty, 0, 4);

                    filestream.Seek(0x1B5128 + index * 2, SeekOrigin.Begin);
                    filestream.Write(empty, 0, 2);

                    filestream.Seek(0x1B5160 + index * 2, SeekOrigin.Begin);
                    filestream.Write(empty, 0, 2);

                    filestream.Seek(0x1B5198 + index * 2, SeekOrigin.Begin);
                    filestream.Write(empty, 0, 2);

                    filestream.Seek(0x1B51D0 + index * 2, SeekOrigin.Begin);
                    filestream.Write(empty, 0, 2);

                    filestream.Seek(0x1B5208 + index, SeekOrigin.Begin);
                    filestream.WriteByte(0);

                    filestream.Seek(0x1B5228 + index * 2, SeekOrigin.Begin);
                    filestream.Write(empty, 0, 2);

                    filestream.Seek(0x1B5260 + index, SeekOrigin.Begin);
                    filestream.WriteByte(0);

                    filestream.Seek(0x1B5280 + index * 2, SeekOrigin.Begin);
                    filestream.Write(empty, 0, 2);
                }
            }
        }
    }
}
