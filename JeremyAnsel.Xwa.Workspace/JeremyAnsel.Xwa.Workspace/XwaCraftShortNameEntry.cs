namespace JeremyAnsel.Xwa.Workspace
{
    public sealed class XwaCraftShortNameEntry
    {
        public string Key { get; set; }

        public string CraftShortName { get; set; }

        public override string ToString()
        {
            return this.Key + " " + this.CraftShortName;
        }
    }
}
