using System.Text;

namespace JeremyAnsel.Xwa.Workspace
{
    public sealed class XwaWorkspace
    {
        public const string GameName = "X-Wing Alliance";

        public const string ExeName = "XWingAlliance.exe";

        public const string SpecDescFileName = "specdesc.txt";

        public const string ShipListFileName = "shiplist.txt";

        public const string StringsFileName = "strings.txt";

        public const string FlightModelDirectory = "FlightModels";

        public const string FlightModelSpacecraftFileName = "FlightModels\\Spacecraft0.lst";

        public const string FlightModelEquipmentFileName = "FlightModels\\Equipment0.lst";

        private static readonly Encoding _encoding = Encoding.GetEncoding("iso-8859-1");

        private readonly string[] _stringsFileLines;

        public XwaWorkspace(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            string exePath = Path.Combine(path, ExeName);
            XwaExeVersion.Match(exePath);
            this.WorkingDirectory = path;
            this.SpeciesTable = new XwaExeSpeciesTable(exePath);
            this.ObjectTable = new XwaExeObjectTable(exePath);
            this.CraftTable = new XwaExeCraftTable(exePath);
            this.WeaponTable = new XwaExeWeaponTable(exePath);
            this.SpecDescFile = new XwaSpecDescFile(Path.Combine(path, SpecDescFileName));
            this.ShipListFile = new XwaShipListFile(Path.Combine(path, ShipListFileName));
            this._stringsFileLines = File.ReadAllLines(Path.Combine(path, StringsFileName), _encoding);
            this.CraftGenderFile = new XwaCraftGenderFile(_stringsFileLines);
            this.CraftPluralNameFile = new XwaCraftPluralNameFile(_stringsFileLines);
            this.CraftShortNameFile = new XwaCraftShortNameFile(_stringsFileLines);
            this.FlightModelSpacecraftFile = new XwaFlightModelListFile(Path.Combine(path, FlightModelSpacecraftFileName));
            this.FlightModelEquipmentFile = new XwaFlightModelListFile(Path.Combine(path, FlightModelEquipmentFileName));
        }

        public string WorkingDirectory { get; private set; }

        public XwaExeSpeciesTable SpeciesTable { get; set; }

        public XwaExeObjectTable ObjectTable { get; set; }

        public XwaExeCraftTable CraftTable { get; set; }

        public XwaExeWeaponTable WeaponTable { get; set; }

        public XwaSpecDescFile SpecDescFile { get; set; }

        public XwaShipListFile ShipListFile { get; set; }

        public XwaCraftGenderFile CraftGenderFile { get; set; }

        public XwaCraftPluralNameFile CraftPluralNameFile { get; set; }

        public XwaCraftShortNameFile CraftShortNameFile { get; set; }

        public XwaFlightModelListFile FlightModelSpacecraftFile { get; set; }

        public XwaFlightModelListFile FlightModelEquipmentFile { get; set; }

        public void Write(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            string exePath = Path.Combine(path, ExeName);
            XwaExeVersion.Match(exePath);
            Directory.CreateDirectory(Path.Combine(path, FlightModelDirectory));
            this.SpeciesTable.Write(exePath);
            this.ObjectTable.Write(exePath);
            this.CraftTable.Write(exePath);
            this.WeaponTable.Write(exePath);
            this.SpecDescFile.Write(Path.Combine(path, SpecDescFileName));
            this.ShipListFile.Write(Path.Combine(path, ShipListFileName));
            this.CraftGenderFile.Write(_stringsFileLines);
            this.CraftPluralNameFile.Write(_stringsFileLines);
            this.CraftShortNameFile.Write(_stringsFileLines);
            File.WriteAllLines(Path.Combine(path, StringsFileName), _stringsFileLines, _encoding);
            this.FlightModelSpacecraftFile.Write(Path.Combine(path, FlightModelSpacecraftFileName));
            this.FlightModelEquipmentFile.Write(Path.Combine(path, FlightModelEquipmentFileName));
        }

        public static void MakeBackup(string workingDirectory, string destinationDirectory)
        {
            XwaExeVersion.Match(Path.Combine(workingDirectory, ExeName));
            Directory.CreateDirectory(destinationDirectory);
            Directory.CreateDirectory(Path.Combine(destinationDirectory, FlightModelDirectory));
            File.Copy(Path.Combine(workingDirectory, ExeName), Path.Combine(destinationDirectory, ExeName), true);
            File.Copy(Path.Combine(workingDirectory, SpecDescFileName), Path.Combine(destinationDirectory, SpecDescFileName), true);
            File.Copy(Path.Combine(workingDirectory, ShipListFileName), Path.Combine(destinationDirectory, ShipListFileName), true);
            File.Copy(Path.Combine(workingDirectory, StringsFileName), Path.Combine(destinationDirectory, StringsFileName), true);
            File.Copy(Path.Combine(workingDirectory, FlightModelSpacecraftFileName), Path.Combine(destinationDirectory, FlightModelSpacecraftFileName), true);
            File.Copy(Path.Combine(workingDirectory, FlightModelEquipmentFileName), Path.Combine(destinationDirectory, FlightModelEquipmentFileName), true);
        }

        public string GetModelName(int modelIndex)
        {
            var obj = ObjectTable.Entries.ElementAtOrDefault(modelIndex);

            if (obj == null)
            {
                return string.Empty;
            }

            return this.GetModelName(obj.DataIndex1, obj.DataIndex2);
        }

        public string GetModelName(short dataIndex1, short dataIndex2)
        {
            if (dataIndex1 == -1)
            {
                return string.Empty;
            }

            XwaFlightModelListEntry? entry = dataIndex1 switch
            {
                0 => FlightModelSpacecraftFile.Entries.ElementAtOrDefault(dataIndex2),
                1 => FlightModelEquipmentFile.Entries.ElementAtOrDefault(dataIndex2),
                _ => null,
            };

            return entry != null ? Path.GetFileNameWithoutExtension(entry.Value) : (dataIndex1 + ", " + dataIndex2);
        }

        public void InstallShpFile(XwaShpFile? shp, int speciesIndex, int objectIndex, int craftIndex)
        {
            if (shp == null)
            {
                throw new ArgumentNullException(nameof(shp));
            }

            if (speciesIndex < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(speciesIndex));
            }

            if (objectIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(objectIndex));
            }

            if (craftIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(craftIndex));
            }

            var genderEntry = this.CraftGenderFile.GetOrAddEntry(craftIndex);
            genderEntry.CraftGender = XwaCraftGender.Masculine;
            genderEntry.CraftName = shp.CraftName;

            var pluralNameEntry = this.CraftPluralNameFile.GetOrAddEntry(craftIndex);
            pluralNameEntry.CraftPluralName = shp.CraftPluralName;

            var shortNameEntry = this.CraftShortNameFile.GetOrAddEntry(craftIndex);
            shortNameEntry.CraftShortName = shp.CraftShortName;

            var specDescEntry = this.SpecDescFile.GetOrAddEntry(speciesIndex - 1);
            specDescEntry.CraftLongName = shp.CraftLongName;
            specDescEntry.Manufacturer = shp.Manufacturer;
            specDescEntry.Side = shp.Side;
            specDescEntry.Description = shp.Description;
            specDescEntry.Crew = shp.Crew;

            var shipListEntry = this.ShipListFile.GetOrAddEntry(speciesIndex - 1);
            shipListEntry.CraftName = shp.CraftName;
            shipListEntry.CraftType = shp.CraftType;
            shipListEntry.Flyable = shp.Flyable;
            shipListEntry.Known = XwaShipListKnownOption.Known;
            shipListEntry.Skirmish = XwaShipListSkirmishOption.Skirmish;

            var objectEntry = this.ObjectTable.Entries[objectIndex];
            objectEntry.EnableOptions = XwaExeObjectEnableOptions.U1 | XwaExeObjectEnableOptions.IsEnabled;
            objectEntry.RessourceOptions = XwaExeObjectRessourceOptions.HasOptModel;
            objectEntry.ObjectCategory = shp.ObjectCategory;
            objectEntry.ShipCategory = shp.ShipCategory;
            objectEntry.ObjectSize = 0;
            objectEntry.GameOptions = shp.ObjectGameOptions | XwaExeObjectGameOptions.Targetable | XwaExeObjectGameOptions.AIControlled | XwaExeObjectGameOptions.U7;
            objectEntry.CraftIndex = (short)craftIndex;
            objectEntry.DataIndex1 = 0;
            objectEntry.DataIndex2 = (short)this.FlightModelSpacecraftFile.GetOrAddEntry(shp.OptFile);

            this.CraftTable.Entries[craftIndex] = shp.Craft;

            this.SpeciesTable.Entries[speciesIndex].Value = (short)objectIndex;
        }
    }
}
