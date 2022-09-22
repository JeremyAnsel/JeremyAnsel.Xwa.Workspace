using System.Globalization;
using System.Text;

namespace JeremyAnsel.Xwa.Workspace
{
    public sealed class XwaShipListFile
    {
        public const int MaxEntryCount = 254;

        private static readonly Encoding _encoding = Encoding.GetEncoding("iso-8859-1");

        public XwaShipListFile()
        {
        }

        public XwaShipListFile(string path)
        {
            using (var file = new StreamReader(path, _encoding))
            {
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    string[] parts = line.Split('!', ',');

                    if (parts.Length < 15)
                    {
                        continue;
                    }

                    try
                    {
                        var entry = new XwaShipListEntry
                        {
                            CraftName = parts[2].Trim(),
                            CraftType = GetCraftType(parts[3].Trim()),
                            Flyable = GetFlyableOption(parts[4].Trim()),
                            Known = GetKnownOption(parts[5].Trim()),
                            Skirmish = GetSkirmishOption(parts[6].Trim()),
                            NB1 = int.Parse(parts[7].Trim(), CultureInfo.InvariantCulture),
                            NB2 = int.Parse(parts[8].Trim(), CultureInfo.InvariantCulture),
                            NB3 = int.Parse(parts[9].Trim(), CultureInfo.InvariantCulture),
                            NB4 = int.Parse(parts[10].Trim(), CultureInfo.InvariantCulture),
                            MapIconRectLeft = int.Parse(parts[11].Trim(), CultureInfo.InvariantCulture),
                            MapIconRectTop = int.Parse(parts[12].Trim(), CultureInfo.InvariantCulture),
                            MapIconRectRight = int.Parse(parts[13].Trim(), CultureInfo.InvariantCulture),
                            MapIconRectBottom = int.Parse(parts[14].Trim(), CultureInfo.InvariantCulture)
                        };

                        this.Entries.Add(entry);
                    }
                    catch (FormatException ex)
                    {
                        throw new FormatException("Invalid format found in:\n" + line, ex);
                    }
                }
            }
        }

        public List<XwaShipListEntry> Entries { get; } = new List<XwaShipListEntry>();

        public void Write(string path)
        {
            using (var file = new StreamWriter(path, false, _encoding))
            {
                int entryCount = Math.Min(this.Entries.Count, MaxEntryCount);

                for (int index = 0; index < entryCount; index++)
                {
                    int id = index + 1;
                    var entry = this.Entries[index];

                    file.Write(string.Format(CultureInfo.InvariantCulture, "!SPECIES_{0}!", id));
                    file.Write(string.Format(CultureInfo.InvariantCulture, "{0},", entry.CraftName));
                    file.Write(CraftTypeToString(entry.CraftType));
                    file.Write(",");
                    file.Write(FlyableToString(entry.Flyable));
                    file.Write(",");
                    file.Write(KnownToString(entry.Known));
                    file.Write(",");
                    file.Write(SkirmishToString(entry.Skirmish));
                    file.Write(",");
                    file.Write("\t\t\t\t\t");
                    file.Write(string.Format(CultureInfo.InvariantCulture, "{0}, {1}, {2}, {3},", entry.NB1, entry.NB2, entry.NB3, entry.NB4));
                    file.Write(string.Format(CultureInfo.InvariantCulture, "{0}, {1}, {2}, {3}", entry.MapIconRectLeft, entry.MapIconRectTop, entry.MapIconRectRight, entry.MapIconRectBottom));
                    file.WriteLine();
                }

                file.WriteLine();
            }
        }

        public static XwaShipListCraftType GetCraftType(string text)
        {
            XwaShipListCraftType craftType;

            if (string.Equals(text, "Fighter", StringComparison.InvariantCultureIgnoreCase))
            {
                craftType = XwaShipListCraftType.Fighter;
            }
            else if (string.Equals(text, "Shuttle/Light Transport", StringComparison.InvariantCultureIgnoreCase))
            {
                craftType = XwaShipListCraftType.LightTransport;
            }
            else if (string.Equals(text, "Utility Craft", StringComparison.InvariantCultureIgnoreCase))
            {
                craftType = XwaShipListCraftType.UtilityCraft;
            }
            else if (string.Equals(text, "Container", StringComparison.InvariantCultureIgnoreCase))
            {
                craftType = XwaShipListCraftType.Container;
            }
            else if (string.Equals(text, "Freighter/Heavy Transport", StringComparison.InvariantCultureIgnoreCase))
            {
                craftType = XwaShipListCraftType.HeavyTransport;
            }
            else if (string.Equals(text, "Starship", StringComparison.InvariantCultureIgnoreCase))
            {
                craftType = XwaShipListCraftType.Starship;
            }
            else if (string.Equals(text, "Station", StringComparison.InvariantCultureIgnoreCase))
            {
                craftType = XwaShipListCraftType.Station;
            }
            else if (string.Equals(text, "Weapon emplacement", StringComparison.InvariantCultureIgnoreCase))
            {
                craftType = XwaShipListCraftType.WeaponEmplacement;
            }
            else if (string.Equals(text, "Mine", StringComparison.InvariantCultureIgnoreCase))
            {
                craftType = XwaShipListCraftType.Mine;
            }
            else if (string.Equals(text, "Satellite/Buoy", StringComparison.InvariantCultureIgnoreCase))
            {
                craftType = XwaShipListCraftType.Satellite;
            }
            else if (string.Equals(text, "Droid", StringComparison.InvariantCultureIgnoreCase))
            {
                craftType = XwaShipListCraftType.Droid;
            }
            else if (string.Equals(text, "Planet/asteroid", StringComparison.InvariantCultureIgnoreCase))
            {
                craftType = XwaShipListCraftType.Planet;
            }
            else
            {
                craftType = XwaShipListCraftType.Unknown;
            }

            return craftType;
        }

        public static string CraftTypeToString(XwaShipListCraftType craftType)
        {
            return craftType switch
            {
                XwaShipListCraftType.Fighter => "Fighter",
                XwaShipListCraftType.LightTransport => "Shuttle/Light Transport",
                XwaShipListCraftType.UtilityCraft => "Utility Craft",
                XwaShipListCraftType.Container => "Container",
                XwaShipListCraftType.HeavyTransport => "Freighter/Heavy Transport",
                XwaShipListCraftType.Starship => "Starship",
                XwaShipListCraftType.Station => "Station",
                XwaShipListCraftType.WeaponEmplacement => "Weapon emplacement",
                XwaShipListCraftType.Mine => "Mine",
                XwaShipListCraftType.Satellite => "Satellite/Buoy",
                XwaShipListCraftType.Droid => "Droid",
                XwaShipListCraftType.Planet => "Planet/asteroid",
                _ => "Unknown",
            };
        }

        public static XwaShipListFlyableOption GetFlyableOption(string text)
        {
            XwaShipListFlyableOption flyable;

            if (string.Equals(text, "Flyable", StringComparison.InvariantCultureIgnoreCase))
            {
                flyable = XwaShipListFlyableOption.Flyable;
            }
            else if (string.Equals(text, "GunnerFlyable", StringComparison.InvariantCultureIgnoreCase))
            {
                flyable = XwaShipListFlyableOption.GunnerFlyable;
            }
            else
            {
                flyable = XwaShipListFlyableOption.Nonflyable;
            }

            return flyable;
        }

        public static string FlyableToString(XwaShipListFlyableOption flyable)
        {
            return flyable switch
            {
                XwaShipListFlyableOption.Flyable => "Flyable",
                XwaShipListFlyableOption.GunnerFlyable => "GunnerFlyable",
                _ => "Nonflyable",
            };
        }

        public static XwaShipListKnownOption GetKnownOption(string text)
        {
            XwaShipListKnownOption known;

            if (string.Equals(text, "Known", StringComparison.InvariantCultureIgnoreCase))
            {
                known = XwaShipListKnownOption.Known;
            }
            else
            {
                known = XwaShipListKnownOption.Unknown;
            }

            return known;
        }

        public static string KnownToString(XwaShipListKnownOption known)
        {
            return known switch
            {
                XwaShipListKnownOption.Known => "Known",
                _ => "Unknown",
            };
        }

        public static XwaShipListSkirmishOption GetSkirmishOption(string text)
        {
            XwaShipListSkirmishOption skirmish;

            if (string.Equals(text, "Skirmish", StringComparison.InvariantCultureIgnoreCase))
            {
                skirmish = XwaShipListSkirmishOption.Skirmish;
            }
            else
            {
                skirmish = XwaShipListSkirmishOption.NoSkirmish;
            }

            return skirmish;
        }

        public static string SkirmishToString(XwaShipListSkirmishOption skirmish)
        {
            return skirmish switch
            {
                XwaShipListSkirmishOption.Skirmish => "Skirmish",
                _ => "NoSkirmish",
            };
        }

        public XwaShipListEntry GetOrAddEntry(int index)
        {
            if (index < 0 || index >= MaxEntryCount)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            while (index >= this.Entries.Count)
            {
                this.Entries.Add(new XwaShipListEntry());
            }

            return this.Entries[index];
        }
    }
}
