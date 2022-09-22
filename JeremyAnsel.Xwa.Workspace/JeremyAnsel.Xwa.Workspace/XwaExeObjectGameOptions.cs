namespace JeremyAnsel.Xwa.Workspace
{
    [Flags]
    public enum XwaExeObjectGameOptions : ushort
    {
        None = 0,

        Targetable = 1 << 0,

        AIControlled = 1 << 1,

        IsBuoy = 1 << 2,

        In3dModeOnly = 1 << 3,

        U5 = 1 << 4,

        IsBackdrop = 1 << 5,

        U7 = 1 << 6,

        InfiniteWaves = 1 << 7,

        IsAnimation = 1 << 8,

        AnimationLoop = 1 << 9,

        HasDatImageBorder = 1 << 10,

        HardpointsMirroring = 1 << 11,

        AxisAligned = 1 << 12,

        U14 = 1 << 13,

        UseImageColorKey = 1 << 14,

        UseImageAlpha = 1 << 15
    }
}
