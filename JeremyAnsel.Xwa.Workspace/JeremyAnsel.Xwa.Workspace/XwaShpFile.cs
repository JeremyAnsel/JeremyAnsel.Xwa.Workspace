using System.Globalization;
using System.IO;
using System.Text;

namespace JeremyAnsel.Xwa.Workspace
{
    public sealed class XwaShpFile
    {
        private static readonly Encoding _encoding = Encoding.GetEncoding("iso-8859-1");

        public XwaShpFile()
        {
        }

        public XwaShpFile(string? path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            using (var filestream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                this.Read(filestream);
            }
        }

        public XwaShpFile(Stream? stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            this.Read(stream);
        }

        public byte ObjectIndex { get; set; }

        public string OptFile { get; set; } = string.Empty;

        public string CraftPluralName { get; set; } = string.Empty;

        public string CraftName { get; set; } = string.Empty;

        public string CraftLongName { get; set; } = string.Empty;

        public string Manufacturer { get; set; } = string.Empty;

        public string Side { get; set; } = string.Empty;

        public string Crew { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public XwaShipCategory ShipCategory { get; set; }

        public XwaShipListCraftType CraftType
        {
            get
            {
                return this.ShipCategory switch
                {
                    XwaShipCategory.Starfighter => XwaShipListCraftType.Fighter,
                    XwaShipCategory.Transport => XwaShipListCraftType.LightTransport,
                    XwaShipCategory.UtilityVehicle => XwaShipListCraftType.UtilityCraft,
                    XwaShipCategory.Freighter => XwaShipListCraftType.HeavyTransport,
                    XwaShipCategory.Starship => XwaShipListCraftType.Starship,
                    XwaShipCategory.Platform => XwaShipListCraftType.Station,
                    XwaShipCategory.PlayerProjectile => XwaShipListCraftType.Unknown,
                    XwaShipCategory.OtherProjectile => XwaShipListCraftType.Unknown,
                    XwaShipCategory.Mine => XwaShipListCraftType.Mine,
                    XwaShipCategory.Satellite => XwaShipListCraftType.Satellite,
                    XwaShipCategory.NormalDebris => XwaShipListCraftType.Unknown,
                    XwaShipCategory.SmallDebris => XwaShipListCraftType.Unknown,
                    XwaShipCategory.Backdrop => XwaShipListCraftType.Planet,
                    XwaShipCategory.Explosion => XwaShipListCraftType.Unknown,
                    XwaShipCategory.Obstacle => XwaShipListCraftType.Unknown,
                    XwaShipCategory.DeathStarII => XwaShipListCraftType.Unknown,
                    XwaShipCategory.People => XwaShipListCraftType.Droid,
                    XwaShipCategory.Container => XwaShipListCraftType.Container,
                    XwaShipCategory.Droid => XwaShipListCraftType.Droid,
                    XwaShipCategory.Armament => XwaShipListCraftType.WeaponEmplacement,
                    XwaShipCategory.LargeDebris => XwaShipListCraftType.Unknown,
                    XwaShipCategory.SalvageYard => XwaShipListCraftType.Unknown,
                    _ => XwaShipListCraftType.Unknown,
                };
            }
        }

        public XwaObjectCategory ObjectCategory
        {
            get
            {
                return this.ShipCategory switch
                {
                    XwaShipCategory.Starfighter => XwaObjectCategory.Craft,
                    XwaShipCategory.Transport => XwaObjectCategory.Craft,
                    XwaShipCategory.UtilityVehicle => XwaObjectCategory.Craft,
                    XwaShipCategory.Freighter => XwaObjectCategory.Craft,
                    XwaShipCategory.Starship => XwaObjectCategory.Craft,
                    XwaShipCategory.Platform => XwaObjectCategory.Craft,
                    XwaShipCategory.PlayerProjectile => XwaObjectCategory.Weapon,
                    XwaShipCategory.OtherProjectile => XwaObjectCategory.Weapon,
                    XwaShipCategory.Mine => XwaObjectCategory.Weapon,
                    XwaShipCategory.Satellite => XwaObjectCategory.Satellite,
                    XwaShipCategory.NormalDebris => XwaObjectCategory.Debris,
                    XwaShipCategory.SmallDebris => XwaObjectCategory.Debris,
                    XwaShipCategory.Backdrop => XwaObjectCategory.Backdrop,
                    XwaShipCategory.Explosion => XwaObjectCategory.Explosion,
                    XwaShipCategory.Obstacle => XwaObjectCategory.Debris,
                    XwaShipCategory.DeathStarII => XwaObjectCategory.Debris,
                    XwaShipCategory.People => XwaObjectCategory.Craft,
                    XwaShipCategory.Container => XwaObjectCategory.Craft,
                    XwaShipCategory.Droid => XwaObjectCategory.Craft,
                    XwaShipCategory.Armament => XwaObjectCategory.Weapon,
                    XwaShipCategory.LargeDebris => XwaObjectCategory.Debris,
                    XwaShipCategory.SalvageYard => XwaObjectCategory.Debris,
                    _ => XwaObjectCategory.Craft,
                };
            }
        }

        public string CraftShortName { get; set; } = string.Empty;

        public XwaShipListFlyableOption Flyable { get; set; }

        public XwaExeObjectGameOptions ObjectGameOptions { get; set; }

        public XwaExeCraftEntry Craft { get; set; } = new();

        private void Read(Stream? stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

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

        public void Write(string? path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            using (var filestream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                this.Write(filestream);
            }
        }

        public void Write(Stream? stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

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
