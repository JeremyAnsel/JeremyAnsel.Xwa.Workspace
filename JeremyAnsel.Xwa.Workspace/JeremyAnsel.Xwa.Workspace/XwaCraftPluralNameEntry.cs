namespace JeremyAnsel.Xwa.Workspace
{
    public sealed class XwaCraftPluralNameEntry
    {
        public string Key { get; set; } = string.Empty;

        public string CraftPluralName { get; set; } = string.Empty;

        public override string ToString()
        {
            return this.Key + " " + this.CraftPluralName;
        }
    }
}
