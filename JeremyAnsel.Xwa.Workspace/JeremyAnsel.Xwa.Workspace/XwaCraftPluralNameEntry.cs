namespace JeremyAnsel.Xwa.Workspace
{
    public sealed class XwaCraftPluralNameEntry
    {
        public string Key { get; set; }

        public string CraftPluralName { get; set; }

        public override string ToString()
        {
            return this.Key + " " + this.CraftPluralName;
        }
    }
}
