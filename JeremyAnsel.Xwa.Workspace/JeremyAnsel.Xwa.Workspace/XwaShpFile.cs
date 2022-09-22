using System.Globalization;
using System.Text;

namespace JeremyAnsel.Xwa.Workspace
{
    public sealed class XwaShpFile
    {
        private static readonly Encoding _encoding = Encoding.GetEncoding("iso-8859-1");

        public XwaShpFile()
        {
        }

        public XwaShpFile(string path)
        {
            using (var filestream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                this.Read(filestream);
            }
        }

        public XwaShpFile(Stream stream)
        {
            this.Read(stream);
        }

        public byte ObjectIndex { get; set; }

        public string OptFile { get; set; }

        public string CraftPluralName { get; set; }

        public string CraftName { get; set; }

        public string CraftLongName { get; set; }

        public string Manufacturer { get; set; }

        public string Side { get; set; }

        public string Crew { get; set; }

        public string Description { get; set; }

        public XwaShipCategory ShipCategory { get; set; }

        public XwaShipListCraftType CraftType
        {
            get
            {
                switch (this.ShipCategory)
                {
                    case XwaShipCategory.Starfighter:
                        return XwaShipListCraftType.Fighter;

                    case XwaShipCategory.Transport:
                        return XwaShipListCraftType.LightTransport;

                    case XwaShipCategory.UtilityVehicle:
                        return XwaShipListCraftType.UtilityCraft;

                    case XwaShipCategory.Freighter:
                        return XwaShipListCraftType.HeavyTransport;

                    case XwaShipCategory.Starship:
                        return XwaShipListCraftType.Starship;

                    case XwaShipCategory.Platform:
                        return XwaShipListCraftType.Station;

                    case XwaShipCategory.PlayerProjectile:
                        return XwaShipListCraftType.Unknown;

                    case XwaShipCategory.OtherProjectile:
                        return XwaShipListCraftType.Unknown;

                    case XwaShipCategory.Mine:
                        return XwaShipListCraftType.Mine;

                    case XwaShipCategory.Satellite:
                        return XwaShipListCraftType.Satellite;

                    case XwaShipCategory.NormalDebris:
                        return XwaShipListCraftType.Unknown;

                    case XwaShipCategory.SmallDebris:
                        return XwaShipListCraftType.Unknown;

                    case XwaShipCategory.Backdrop:
                        return XwaShipListCraftType.Planet;

                    case XwaShipCategory.Explosion:
                        return XwaShipListCraftType.Unknown;

                    case XwaShipCategory.Obstacle:
                        return XwaShipListCraftType.Unknown;

                    case XwaShipCategory.DeathStarII:
                        return XwaShipListCraftType.Unknown;

                    case XwaShipCategory.People:
                        return XwaShipListCraftType.Droid;

                    case XwaShipCategory.Container:
                        return XwaShipListCraftType.Container;

                    case XwaShipCategory.Droid:
                        return XwaShipListCraftType.Droid;

                    case XwaShipCategory.Armament:
                        return XwaShipListCraftType.WeaponEmplacement;

                    case XwaShipCategory.LargeDebris:
                        return XwaShipListCraftType.Unknown;

                    case XwaShipCategory.SalvageYard:
                        return XwaShipListCraftType.Unknown;

                    default:
                        return XwaShipListCraftType.Unknown;
                }
            }
        }

        public XwaObjectCategory ObjectCategory
        {
            get
            {
                switch (this.ShipCategory)
                {
                    case XwaShipCategory.Starfighter:
                        return XwaObjectCategory.Craft;

                    case XwaShipCategory.Transport:
                        return XwaObjectCategory.Craft;

                    case XwaShipCategory.UtilityVehicle:
                        return XwaObjectCategory.Craft;

                    case XwaShipCategory.Freighter:
                        return XwaObjectCategory.Craft;

                    case XwaShipCategory.Starship:
                        return XwaObjectCategory.Craft;

                    case XwaShipCategory.Platform:
                        return XwaObjectCategory.Craft;

                    case XwaShipCategory.PlayerProjectile:
                        return XwaObjectCategory.Weapon;

                    case XwaShipCategory.OtherProjectile:
                        return XwaObjectCategory.Weapon;

                    case XwaShipCategory.Mine:
                        return XwaObjectCategory.Weapon;

                    case XwaShipCategory.Satellite:
                        return XwaObjectCategory.Satellite;

                    case XwaShipCategory.NormalDebris:
                        return XwaObjectCategory.Debris;

                    case XwaShipCategory.SmallDebris:
                        return XwaObjectCategory.Debris;

                    case XwaShipCategory.Backdrop:
                        return XwaObjectCategory.Backdrop;

                    case XwaShipCategory.Explosion:
                        return XwaObjectCategory.Explosion;

                    case XwaShipCategory.Obstacle:
                        return XwaObjectCategory.Debris;

                    case XwaShipCategory.DeathStarII:
                        return XwaObjectCategory.Debris;

                    case XwaShipCategory.People:
                        return XwaObjectCategory.Craft;

                    case XwaShipCategory.Container:
                        return XwaObjectCategory.Craft;

                    case XwaShipCategory.Droid:
                        return XwaObjectCategory.Craft;

                    case XwaShipCategory.Armament:
                        return XwaObjectCategory.Weapon;

                    case XwaShipCategory.LargeDebris:
                        return XwaObjectCategory.Debris;

                    case XwaShipCategory.SalvageYard:
                        return XwaObjectCategory.Debris;

                    default:
                        return XwaObjectCategory.Craft;
                }
            }
        }

        public string CraftShortName { get; set; }

        public XwaShipListFlyableOption Flyable { get; set; }

        public XwaExeObjectGameOptions ObjectGameOptions { get; set; }

        public XwaExeCraftEntry Craft { get; set; }

        private void Read(Stream stream)
        {
            if (ReadLine(stream) != "[Specs]")
            {
                throw new InvalidDataException("\"[Specs]\" not found");
            }

            if (ReadLine(stream) != "MXvTED craft file V1.7")
            {
                throw new InvalidDataException("\"MXvTED craft file V1.7\" not found");
            }

            this.ObjectIndex = (byte)stream.ReadByte();

            ReadLine(stream);
            ReadLine(stream);

            this.OptFile = ReadLine(stream);
            this.CraftPluralName = ReadLine(stream);
            this.CraftName = ReadLine(stream);
            this.CraftLongName = ReadLine(stream);
            this.Manufacturer = ReadLine(stream);
            this.Side = ReadLine(stream);
            this.Crew = ReadLine(stream);
            this.Description = ReadLine(stream);

            if (!ReadLine(stream).StartsWith("X-Wing Alliance", StringComparison.Ordinal))
            {
                throw new InvalidDataException("\"X-Wing Alliance\" version not found");
            }

            this.ShipCategory = (XwaShipCategory)byte.Parse(ReadLine(stream).Substring(0, 2), CultureInfo.InvariantCulture);
            this.CraftShortName = ReadLine(stream);
            this.Flyable = (XwaShipListFlyableOption)Enum.Parse(typeof(XwaShipListFlyableOption), ReadLine(stream));
            this.ObjectGameOptions = (XwaExeObjectGameOptions)byte.Parse(ReadLine(stream), CultureInfo.InvariantCulture);

            if (ReadLine(stream) != "[Bin]")
            {
                throw new InvalidDataException("\"[Bin]\" not found");
            }

            var craftBytes = new byte[XwaExeCraftEntry.EntryLength];
            stream.Read(craftBytes, 8, XwaExeCraftEntry.EntryLength - 8);
            this.Craft = new XwaExeCraftEntry(craftBytes);
        }

        public void Write(string path)
        {
            using (var filestream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                this.Write(filestream);
            }
        }

        public void Write(Stream stream)
        {
            WriteLine(stream, "[Specs]");
            WriteLine(stream, "MXvTED craft file V1.7");
            stream.WriteByte(this.ObjectIndex);
            WriteNewLine(stream);
            WriteNewLine(stream);
            WriteLine(stream, this.OptFile);
            WriteLine(stream, this.CraftPluralName);
            WriteLine(stream, this.CraftName);
            WriteLine(stream, this.CraftLongName);
            WriteLine(stream, this.Manufacturer);
            WriteLine(stream, this.Side);
            WriteLine(stream, this.Crew);
            WriteLine(stream, this.Description);
            WriteLine(stream, "X-Wing Alliance v2.02");
            WriteLine(stream, ((byte)this.ShipCategory).ToString(CultureInfo.InvariantCulture).PadRight(2) + " " + this.ShipCategory.ToString());
            WriteLine(stream, this.CraftShortName);
            WriteLine(stream, this.Flyable.ToString());
            WriteLine(stream, ((byte)this.ObjectGameOptions).ToString(CultureInfo.InvariantCulture));
            WriteLine(stream, "[Bin]");
            var craftBytes = this.Craft.ToByteArray();
            stream.Write(craftBytes, 0, XwaExeCraftEntry.EntryLength);
        }

        private static string ReadLine(Stream stream)
        {
            var bytes = new List<byte>();

            while (true)
            {
                int b = stream.ReadByte();

                if (b == -1)
                {
                    break;
                }

                // '\n'
                if (b == 0x0A)
                {
                    break;
                }

                // '\r'
                if (b == 0x0D)
                {
                    continue;
                }

                bytes.Add((byte)b);
            }

            return _encoding.GetString(bytes.ToArray());
        }

        private static void WriteNewLine(Stream stream)
        {
            stream.WriteByte(0x0D);
            stream.WriteByte(0x0A);
        }

        private static void WriteLine(Stream stream, string line)
        {
            byte[] bytes = _encoding.GetBytes(line);
            stream.Write(bytes, 0, bytes.Length);
            WriteNewLine(stream);
        }
    }
}
