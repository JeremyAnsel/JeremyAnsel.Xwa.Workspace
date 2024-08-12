namespace JeremyAnsel.Xwa.Workspace
{
    public sealed class XwaCraftShortNameEntry
    {
        public string Key { get; set; } = string.Empty;

        public string CraftShortName { get; set; } = string.Empty;

        public override string ToString()
        {
            return this.Key + " " + this.CraftShortName;
        }
    }
}
