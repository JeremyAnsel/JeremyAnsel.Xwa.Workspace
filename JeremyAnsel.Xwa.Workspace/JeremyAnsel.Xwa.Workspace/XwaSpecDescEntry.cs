namespace JeremyAnsel.Xwa.Workspace
{
    public sealed class XwaSpecDescEntry
    {
        public const int CraftLongNameMaxLength = 63;
        public const int ManufacturerMaxLength = 63;
        public const int SideMaxLength = 63;
        public const int DescriptionMaxLength = 255;
        public const int CrewMaxLength = 63;

        private string craftLongName;

        public string CraftLongName
        {
            get
            {
                return craftLongName;
            }

            set
            {
                craftLongName = value?.Substring(0, Math.Min(value.Length, CraftLongNameMaxLength));
            }
        }

        public string CraftAbbreviation
        {
            get
            {
                if (string.IsNullOrEmpty(this.CraftLongName))
                {
                    return string.Empty;
                }

                int index1 = this.CraftLongName.LastIndexOf('(');
                int index2 = this.CraftLongName.LastIndexOf(')');

                if (index1 == -1 || index2 == -1 || index1 >= index2)
                {
                    return string.Empty;
                }

                return this.CraftLongName.Substring(index1 + 1, index2 - index1 - 1);
            }
        }

        private string manufacturer;

        public string Manufacturer
        {
            get
            {
                return manufacturer;
            }

            set
            {
                manufacturer = value?.Substring(0, Math.Min(value.Length, ManufacturerMaxLength));
            }
        }

        private string side;

        public string Side
        {
            get
            {
                return side;
            }

            set
            {
                side = value?.Substring(0, Math.Min(value.Length, SideMaxLength));
            }
        }

        private string description;

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value?.Substring(0, Math.Min(value.Length, DescriptionMaxLength));
            }
        }

        private string crew;

        public string Crew
        {
            get
            {
                return crew;
            }

            set
            {
                crew = value?.Substring(0, Math.Min(value.Length, CrewMaxLength));
            }
        }
    }
}
