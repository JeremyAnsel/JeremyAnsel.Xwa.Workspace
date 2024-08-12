using System.Globalization;

namespace JeremyAnsel.Xwa.Workspace
{
    public sealed class XwaShipListEntry
    {
        public const int CraftNameMaxLength = 63;

        private string craftName = string.Empty;

        public string CraftName
        {
            get
            {
                return craftName;
            }

            set
            {
                craftName = value[..Math.Min(value.Length, CraftNameMaxLength)];
            }
        }

        public XwaShipListCraftType CraftType { get; set; }

        public XwaShipListFlyableOption Flyable { get; set; }

        public XwaShipListKnownOption Known { get; set; }

        public XwaShipListSkirmishOption Skirmish { get; set; }

        public int NB1 { get; set; }

        public int NB2 { get; set; }

        public int NB3 { get; set; }

        public int NB4 { get; set; }

        public int MapIconRectLeft { get; set; }

        public int MapIconRectTop { get; set; }

        public int MapIconRectRight { get; set; }

        public int MapIconRectBottom { get; set; }

        public string MapIconRect
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "{0}, {1}, {2}, {3}",
                    this.MapIconRectLeft,
                    this.MapIconRectTop,
                    this.MapIconRectRight,
                    this.MapIconRectBottom);
            }
        }
    }
}
