namespace JeremyAnsel.Xwa.Workspace
{
    public sealed class XwaExeCraftLaser
    {
        public short TypeId { get; set; }

        public byte StartRack { get; set; }

        public byte EndRack { get; set; }

        public byte LinkCode { get; set; }

        public byte Sequence { get; set; }

        public int Range { get; set; }

        public short FireRatio { get; set; }
    }
}
