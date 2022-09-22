namespace JeremyAnsel.Xwa.Workspace
{
    public sealed class XwaCraftGenderEntry
    {
        public string Key { get; set; }

        public XwaCraftGender CraftGender { get; set; }

        public string CraftName { get; set; }

        public override string ToString()
        {
            return this.Key + " " + this.CraftGender + " " + this.CraftName;
        }
    }
}
