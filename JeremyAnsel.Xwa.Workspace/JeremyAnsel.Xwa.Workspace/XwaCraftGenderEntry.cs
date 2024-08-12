namespace JeremyAnsel.Xwa.Workspace
{
    public sealed class XwaCraftGenderEntry
    {
        public string Key { get; set; } = string.Empty;

        public XwaCraftGender CraftGender { get; set; }

        public string CraftName { get; set; } = string.Empty;

        public override string ToString()
        {
            return this.Key + " " + this.CraftGender + " " + this.CraftName;
        }
    }
}
