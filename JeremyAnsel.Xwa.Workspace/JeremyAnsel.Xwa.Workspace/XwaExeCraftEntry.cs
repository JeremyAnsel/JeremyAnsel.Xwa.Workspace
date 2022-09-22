using System.Text;

namespace JeremyAnsel.Xwa.Workspace
{
    public sealed class XwaExeCraftEntry
    {
        public const int EntryLength = 987;

        private string _cockpitFile = string.Empty;

        public XwaExeCraftEntry()
        {
        }

        public XwaExeCraftEntry(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            if (bytes.Length != EntryLength)
            {
                throw new ArgumentOutOfRangeException(nameof(bytes));
            }

            this.Score = BitConverter.ToInt16(bytes, 8);
            this.PromoPoints = BitConverter.ToInt16(bytes, 10);
            this.HasHyperdrive = bytes[12] != 0;
            this.GunConvergence = (XwaGunConvergence)bytes[13];
            this.HasShieldGenerator = bytes[14] != 0;
            this.ShieldStrength = BitConverter.ToUInt32(bytes, 15);
            this.AIHitsTakenToEvade = bytes[19];
            this.HullStrength = BitConverter.ToUInt32(bytes, 21);
            this.CriticalDamageThreshold = BitConverter.ToUInt32(bytes, 25);
            this.SystemStrength = BitConverter.ToUInt16(bytes, 29);
            this.EngineThrottle = bytes[31];
            this.Speed = BitConverter.ToInt16(bytes, 32);
            this.Acceleration = BitConverter.ToInt16(bytes, 34);
            this.Deceleration = BitConverter.ToInt16(bytes, 36);
            this.Yaw = BitConverter.ToInt16(bytes, 38);
            this.YawPercent = BitConverter.ToUInt16(bytes, 40);
            this.Roll = BitConverter.ToInt16(bytes, 42);
            this.Pitch = BitConverter.ToInt16(bytes, 44);
            this.DestroyRotation = BitConverter.ToInt16(bytes, 46);
            this.DriftSpeed = BitConverter.ToInt16(bytes, 48);

            string cockpitFile = Encoding.ASCII.GetString(bytes, 50, 255);
            this.CockpitFile = cockpitFile.Substring(0, cockpitFile.IndexOf('\0'));

            for (int i = 0; i < 3; i++)
            {
                this.Lasers[i].TypeId = BitConverter.ToInt16(bytes, 306 + i * 2);
                this.Lasers[i].StartRack = bytes[312 + i];
                this.Lasers[i].EndRack = bytes[315 + i];
                this.Lasers[i].LinkCode = bytes[318 + i];
                this.Lasers[i].Sequence = bytes[321 + i];
                this.Lasers[i].Range = BitConverter.ToInt32(bytes, 324 + i * 4);
                this.Lasers[i].FireRatio = BitConverter.ToInt16(bytes, 336 + i * 2);
            }

            for (int i = 0; i < 2; i++)
            {
                this.Warheads[i].TypeId = BitConverter.ToInt16(bytes, 342 + i * 2);
                this.Warheads[i].StartRack = bytes[346 + i];
                this.Warheads[i].EndRack = bytes[348 + i];
                this.Warheads[i].LinkCode = bytes[350 + i];
                this.Warheads[i].Capacity = bytes[352 + i];
            }

            this.CounterMeasuresCount = bytes[563];
            this.CockpitDeltaY = BitConverter.ToInt32(bytes, 564);
            this.CockpitPositionY = BitConverter.ToInt16(bytes, 568);
            this.CockpitPositionZ = BitConverter.ToInt16(bytes, 570);
            this.CockpitPositionX = BitConverter.ToInt16(bytes, 572);

            for (int i = 0; i < 2; i++)
            {
                this.Turrets[i].PositionX = BitConverter.ToInt16(bytes, 574 + i * 2);
                this.Turrets[i].PositionZ = BitConverter.ToInt16(bytes, 578 + i * 2);
                this.Turrets[i].PositionY = BitConverter.ToInt16(bytes, 582 + i * 2);
                this.Turrets[i].OrientationX = BitConverter.ToUInt16(bytes, 586 + i * 2);
                this.Turrets[i].OrientationY = BitConverter.ToUInt16(bytes, 590 + i * 2);
                this.Turrets[i].ModelId = BitConverter.ToInt16(bytes, 594 + i * 2);
                this.Turrets[i].ArcX = BitConverter.ToUInt16(bytes, 598 + i * 2);
                this.Turrets[i].ArcY = BitConverter.ToUInt16(bytes, 602 + i * 2);
            }

            this.DockPositionY = BitConverter.ToInt32(bytes, 606);
            this.DockFromSmallPositionZ = BitConverter.ToInt32(bytes, 610);
            this.DockFromBigPositionZ = BitConverter.ToInt32(bytes, 614);
            this.DockToSmallPositionZ = BitConverter.ToInt32(bytes, 618);
            this.DockToBigPositionZ = BitConverter.ToInt32(bytes, 622);
            this.InsideHangarX = BitConverter.ToInt32(bytes, 626);
            this.InsideHangarZ = BitConverter.ToInt32(bytes, 630);
            this.InsideHangarY = BitConverter.ToInt32(bytes, 634);
            this.OutsideHangarX = BitConverter.ToInt32(bytes, 638);
            this.OutsideHangarZ = BitConverter.ToInt32(bytes, 642);
            this.OutsideHangarY = BitConverter.ToInt32(bytes, 646);
        }

        public short Score { get; set; }

        public short PromoPoints { get; set; }

        public bool HasHyperdrive { get; set; }

        public XwaGunConvergence GunConvergence { get; set; }

        public bool HasShieldGenerator { get; set; }

        public uint ShieldStrength { get; set; }

        public byte AIHitsTakenToEvade { get; set; }

        public uint HullStrength { get; set; }

        public uint CriticalDamageThreshold { get; set; }

        public ushort SystemStrength { get; set; }

        public byte EngineThrottle { get; set; }

        public short Speed { get; set; }

        public short Acceleration { get; set; }

        public short Deceleration { get; set; }

        public short Yaw { get; set; }

        public ushort YawPercent { get; set; }

        public short Roll { get; set; }

        public short Pitch { get; set; }

        public short DestroyRotation { get; set; }

        public short DriftSpeed { get; set; }

        public string CockpitFile
        {
            get
            {
                return this._cockpitFile;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    this._cockpitFile = string.Empty;
                }
                else
                {
                    byte[] bytes = Encoding.ASCII.GetBytes(value.Trim());

                    this._cockpitFile = Encoding.ASCII.GetString(bytes, 0, Math.Min(bytes.Length, 255));
                }
            }
        }

        public XwaExeCraftLaser[] Lasers { get; } = new XwaExeCraftLaser[3]
        {
            new XwaExeCraftLaser(),
            new XwaExeCraftLaser(),
            new XwaExeCraftLaser()
        };

        public XwaExeCraftWarhead[] Warheads { get; } = new XwaExeCraftWarhead[2]
        {
            new XwaExeCraftWarhead(),
            new XwaExeCraftWarhead()
        };

        public byte CounterMeasuresCount { get; set; }

        public int CockpitDeltaY { get; set; }

        public short CockpitPositionX { get; set; }

        public short CockpitPositionY { get; set; }

        public short CockpitPositionZ { get; set; }

        public XwaExeCraftTurret[] Turrets { get; } = new XwaExeCraftTurret[2]
        {
            new XwaExeCraftTurret(),
            new XwaExeCraftTurret()
        };

        public int DockPositionY { get; set; }

        public int DockFromSmallPositionZ { get; set; }

        public int DockFromBigPositionZ { get; set; }

        public int DockToSmallPositionZ { get; set; }

        public int DockToBigPositionZ { get; set; }

        public int InsideHangarX { get; set; }

        public int InsideHangarZ { get; set; }

        public int InsideHangarY { get; set; }

        public int OutsideHangarX { get; set; }

        public int OutsideHangarZ { get; set; }

        public int OutsideHangarY { get; set; }

        public byte[] ToByteArray()
        {
            var bytes = new byte[EntryLength];

            for (int i = 0; i < 8; i++)
            {
                bytes[i] = 0;
            }

            BitConverter.GetBytes(this.Score).CopyTo(bytes, 8);
            BitConverter.GetBytes(this.PromoPoints).CopyTo(bytes, 10);
            bytes[12] = this.HasHyperdrive ? (byte)1 : (byte)0;
            bytes[13] = (byte)this.GunConvergence;
            bytes[14] = this.HasShieldGenerator ? (byte)1 : (byte)0;
            BitConverter.GetBytes(this.ShieldStrength).CopyTo(bytes, 15);
            bytes[19] = this.AIHitsTakenToEvade;
            bytes[20] = 0;
            BitConverter.GetBytes(this.HullStrength).CopyTo(bytes, 21);
            BitConverter.GetBytes(this.CriticalDamageThreshold).CopyTo(bytes, 25);
            BitConverter.GetBytes(this.SystemStrength).CopyTo(bytes, 29);
            bytes[31] = this.EngineThrottle;
            BitConverter.GetBytes(this.Speed).CopyTo(bytes, 32);
            BitConverter.GetBytes(this.Acceleration).CopyTo(bytes, 34);
            BitConverter.GetBytes(this.Deceleration).CopyTo(bytes, 36);
            BitConverter.GetBytes(this.Yaw).CopyTo(bytes, 38);
            BitConverter.GetBytes(this.YawPercent).CopyTo(bytes, 40);
            BitConverter.GetBytes(this.Roll).CopyTo(bytes, 42);
            BitConverter.GetBytes(this.Pitch).CopyTo(bytes, 44);
            BitConverter.GetBytes(this.DestroyRotation).CopyTo(bytes, 46);
            BitConverter.GetBytes(this.DriftSpeed).CopyTo(bytes, 48);

            byte[] cockpitFile = Encoding.ASCII.GetBytes(this.CockpitFile);
            cockpitFile.CopyTo(bytes, 50);
            for (int i = 50 + cockpitFile.Length; i < 306; i++)
            {
                bytes[i] = 0;
            }

            for (int i = 0; i < 3; i++)
            {
                XwaExeCraftLaser laser = this.Lasers[i] ?? new XwaExeCraftLaser();

                BitConverter.GetBytes(laser.TypeId).CopyTo(bytes, 306 + i * 2);
                bytes[312 + i] = laser.StartRack;
                bytes[315 + i] = laser.EndRack;
                bytes[318 + i] = laser.LinkCode;
                bytes[321 + i] = laser.Sequence;
                BitConverter.GetBytes(laser.Range).CopyTo(bytes, 324 + i * 4);
                BitConverter.GetBytes(laser.FireRatio).CopyTo(bytes, 336 + i * 2);
            }

            for (int i = 0; i < 2; i++)
            {
                XwaExeCraftWarhead warhead = this.Warheads[i] ?? new XwaExeCraftWarhead();

                BitConverter.GetBytes(warhead.TypeId).CopyTo(bytes, 342 + i * 2);
                bytes[346 + i] = warhead.StartRack;
                bytes[348 + i] = warhead.EndRack;
                bytes[350 + i] = warhead.LinkCode;
                bytes[352 + i] = warhead.Capacity;
            }

            for (int i = 354; i < 563; i++)
            {
                bytes[i] = 0;
            }

            bytes[563] = this.CounterMeasuresCount;
            BitConverter.GetBytes(this.CockpitDeltaY).CopyTo(bytes, 564);
            BitConverter.GetBytes(this.CockpitPositionY).CopyTo(bytes, 568);
            BitConverter.GetBytes(this.CockpitPositionZ).CopyTo(bytes, 570);
            BitConverter.GetBytes(this.CockpitPositionX).CopyTo(bytes, 572);

            for (int i = 0; i < 2; i++)
            {
                XwaExeCraftTurret turret = this.Turrets[i] ?? new XwaExeCraftTurret();

                BitConverter.GetBytes(turret.PositionX).CopyTo(bytes, 574 + i * 2);
                BitConverter.GetBytes(turret.PositionZ).CopyTo(bytes, 578 + i * 2);
                BitConverter.GetBytes(turret.PositionY).CopyTo(bytes, 582 + i * 2);
                BitConverter.GetBytes(turret.OrientationX).CopyTo(bytes, 586 + i * 2);
                BitConverter.GetBytes(turret.OrientationY).CopyTo(bytes, 590 + i * 2);
                BitConverter.GetBytes(turret.ModelId).CopyTo(bytes, 594 + i * 2);
                BitConverter.GetBytes(turret.ArcX).CopyTo(bytes, 598 + i * 2);
                BitConverter.GetBytes(turret.ArcY).CopyTo(bytes, 602 + i * 2);
            }

            BitConverter.GetBytes(this.DockPositionY).CopyTo(bytes, 606);
            BitConverter.GetBytes(this.DockFromSmallPositionZ).CopyTo(bytes, 610);
            BitConverter.GetBytes(this.DockFromBigPositionZ).CopyTo(bytes, 614);
            BitConverter.GetBytes(this.DockToSmallPositionZ).CopyTo(bytes, 618);
            BitConverter.GetBytes(this.DockToBigPositionZ).CopyTo(bytes, 622);
            BitConverter.GetBytes(this.InsideHangarX).CopyTo(bytes, 626);
            BitConverter.GetBytes(this.InsideHangarZ).CopyTo(bytes, 630);
            BitConverter.GetBytes(this.InsideHangarY).CopyTo(bytes, 634);
            BitConverter.GetBytes(this.OutsideHangarX).CopyTo(bytes, 638);
            BitConverter.GetBytes(this.OutsideHangarZ).CopyTo(bytes, 642);
            BitConverter.GetBytes(this.OutsideHangarY).CopyTo(bytes, 646);

            for (int i = 650; i < 987; i++)
            {
                bytes[i] = 0;
            }

            return bytes;
        }
    }
}
