namespace JeremyAnsel.Xwa.Workspace
{
    public sealed class XwaExeSpeciesEntry
    {
        public const int EntryLength = 2;

        public XwaExeSpeciesEntry()
        {
        }

        public XwaExeSpeciesEntry(byte[]? bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            if (bytes.Length != EntryLength)
            {
                throw new ArgumentOutOfRangeException(nameof(bytes));
            }

            this.Value = BitConverter.ToInt16(bytes, 0);
        }

        public short Value { get; set; }

        public byte[] ToByteArray()
        {
            var bytes = new byte[EntryLength];
            BitConverter.GetBytes(this.Value).CopyTo(bytes, 0);
            return bytes;
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
