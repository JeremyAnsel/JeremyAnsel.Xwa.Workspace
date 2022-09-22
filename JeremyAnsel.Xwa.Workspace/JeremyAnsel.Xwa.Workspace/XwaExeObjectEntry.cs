namespace JeremyAnsel.Xwa.Workspace
{
    public sealed class XwaExeObjectEntry
    {
        public const int EntryLength = 24;

        public XwaExeObjectEntry()
        {
            this.DataIndex1 = -1;
        }

        public XwaExeObjectEntry(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            if (bytes.Length != EntryLength)
            {
                throw new ArgumentOutOfRangeException(nameof(bytes));
            }

            this.EnableOptions = (XwaExeObjectEnableOptions)bytes[0];
            this.RessourceOptions = (XwaExeObjectRessourceOptions)bytes[1];
            this.ObjectCategory = (XwaObjectCategory)bytes[2];
            this.ShipCategory = (XwaShipCategory)bytes[3];
            this.ObjectSize = BitConverter.ToUInt32(bytes, 4);
            this.GameOptions = (XwaExeObjectGameOptions)BitConverter.ToUInt16(bytes, 16);
            this.CraftIndex = BitConverter.ToInt16(bytes, 18);
            this.DataIndex1 = BitConverter.ToInt16(bytes, 20);
            this.DataIndex2 = BitConverter.ToInt16(bytes, 22);
        }

        public XwaExeObjectEnableOptions EnableOptions { get; set; }

        public XwaExeObjectRessourceOptions RessourceOptions { get; set; }

        public XwaObjectCategory ObjectCategory { get; set; }

        public XwaShipCategory ShipCategory { get; set; }

        public uint ObjectSize { get; set; }

        public XwaExeObjectGameOptions GameOptions { get; set; }

        public short CraftIndex { get; set; }

        public short DataIndex1 { get; set; }

        public short DataIndex2 { get; set; }

        public byte[] ToByteArray()
        {
            var bytes = new byte[EntryLength];

            bytes[0] = (byte)this.EnableOptions;
            bytes[1] = (byte)this.RessourceOptions;
            bytes[2] = (byte)this.ObjectCategory;
            bytes[3] = (byte)this.ShipCategory;
            BitConverter.GetBytes(this.ObjectSize).CopyTo(bytes, 4);

            for (int i = 8; i < 16; i++)
            {
                bytes[i] = 0;
            }

            BitConverter.GetBytes((ushort)this.GameOptions).CopyTo(bytes, 16);
            BitConverter.GetBytes(this.CraftIndex).CopyTo(bytes, 18);
            BitConverter.GetBytes(this.DataIndex1).CopyTo(bytes, 20);
            BitConverter.GetBytes(this.DataIndex2).CopyTo(bytes, 22);

            return bytes;
        }
    }
}
