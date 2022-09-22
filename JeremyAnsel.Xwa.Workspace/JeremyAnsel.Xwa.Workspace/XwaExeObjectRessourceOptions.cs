namespace JeremyAnsel.Xwa.Workspace
{
    [Flags]
    public enum XwaExeObjectRessourceOptions : byte
    {
        None = 0,

        HasOptModel = 1 << 0,

        LoadImage = 1 << 1,

        HasImage = 1 << 2,

        InAllMissions = 1 << 3,

        InCurrentMission = 1 << 4,

        InDeathStar = 1 << 5,

        InSalvageYard = 1 << 6,

        NotInDeathStar = 1 << 7
    }
}
