namespace JeremyAnsel.Xwa.Workspace
{
    public sealed class XwaExeWeaponEntry
    {
        public int Power { get; set; }

        public short Speed { get; set; }

        public ushort DurationIntegerPart { get; set; }

        public ushort DurationDecimalPart { get; set; }

        public short HitboxSpan { get; set; }

        public byte Behavior { get; set; }

        public short Score { get; set; }

        public sbyte Side { get; set; }

        public short SideModel { get; set; }
    }
}
