using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Falcon.Core
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public sealed class DataCartridge : IFormattable, IEquatable<DataCartridge>, ICloneable
    {
        // TODO: Decide if also implementing IComparable will be necessary/appropriate here.

        #region Properties

        public string Id { get; }

        public EWSConfig EWS { get; }

        public MFDConfig MFD { get; }

        public RadioConfig Radio { get; }

        public NavigationConfig Navigation { get; }

        public SystemsConfig Systems { get; }

        public WeaponsConfig Weapons { get; }

        #endregion

        #region Constructors

        public DataCartridge(string id)
        {
            Id = id;
            
            EWS = new EWSConfig();
            MFD = new MFDConfig();
            Radio = new RadioConfig();
            Navigation = new NavigationConfig();
            Systems = new SystemsConfig();
            Weapons = new WeaponsConfig();
        }

        #endregion

        #region Methods

        public DataCartridge Clone()
        {
            throw new NotImplementedException();
        }

        object ICloneable.Clone() => Clone();

        public bool Equals(DataCartridge other)
        {
            throw new NotImplementedException();
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, format);
        }

        public async Task<bool> Save()
        {
            await Task.Run(() =>
            {
                
            });

            throw new NotImplementedException();
        }

        public async Task<bool> Load()
        {
            await Task.Run(() =>
            {
                
            });

            throw new NotImplementedException();
        }

        public async Task<bool> Backup()
        {
            await Task.Run(() =>
            {
                
            });

            throw new NotImplementedException();
        }

        #endregion

        #region Classes

        public sealed class EWSConfig
        {
            #region Properties

            public uint ChaffBingoQuantity { get; }

            public uint FlareBingoQuantity { get; }

            public bool REQJAM { get; }

            public bool REQCTR { get; }

            public bool Feedback { get; }

            public bool Bingo { get; }

            public CMDSConfig CMDS { get; }

            #endregion

            #region Constructors

            public EWSConfig()
            {
                ChaffBingoQuantity = 10;
                FlareBingoQuantity = 10;
                REQJAM = true;
                REQCTR = true;
                Feedback = true;
                Bingo = true;
                CMDS = new CMDSConfig();
            }

            #endregion

            #region Classes

            public sealed class CMDSConfig
            {
                public CMDSModes Mode { get; }

                public uint ProgramNumber { get; } // TODO: Clamp this value to appropriate ranges or change to another fixed enum.

                public CMDSConfig()
                {

                    Mode = CMDSModes.Off;
                    ProgramNumber = 1;
                }

                public enum CMDSModes
                {
                    Off,
                    Manual,
                    Semi,
                    Auto,
                }
            }

            #endregion
        }

        public sealed class MFDConfig
        {
            #region Properties

            public bool ShowBullseye { get; }

            public AGMasterModes AGModes { get; }

            public AAMasterModes AAModes { get; }

            #endregion

            #region Constructors

            public MFDConfig()
            {
                ShowBullseye = false;

                AGModes = new AGMasterModes();
                AAModes = new AAMasterModes();
            }

            #endregion

            #region Methods

            public async Task<bool> Backup()
            {
                await Task.Run(() =>
                {

                });

                throw new NotImplementedException();
            }

            #endregion

            #region Classes

            public sealed class AGMasterModes
            {
                public MFD MFD1 { get; }

                public MFD MFD2 { get; }

                public MFD MFD3 { get; }

                public MFD MFD4 { get; }

                public AGMasterModes()
                {
                    MFD1 = new MFD();
                    MFD2 = new MFD();
                    MFD3 = new MFD();
                    MFD4 = new MFD();
                }

                public AGMasterModes(MFD mfd1, MFD mfd2)
                {
                    MFD1 = mfd1;
                    MFD2 = mfd2;
                }

                public AGMasterModes(MFD mfd1, MFD mfd2, MFD mfd3, MFD mfd4)
                {
                    MFD1 = mfd1;
                    MFD2 = mfd2;
                    MFD3 = mfd3;
                    MFD4 = mfd4;
                }
            }

            public sealed class AAMasterModes
            {
                public MFD MFD1 { get; }

                public MFD MFD2 { get; }

                public MFD MFD3 { get; }

                public MFD MFD4 { get; }

                public AAMasterModes()
                {
                    MFD1 = new MFD();
                    MFD2 = new MFD();
                    MFD3 = new MFD();
                    MFD4 = new MFD();
                }

                public AAMasterModes(MFD mfd1, MFD mfd2)
                {
                    MFD1 = mfd1;
                    MFD2 = mfd2;
                }

                public AAMasterModes(MFD mfd1, MFD mfd2, MFD mfd3, MFD mfd4)
                {
                    MFD1 = mfd1;
                    MFD2 = mfd2;
                    MFD3 = mfd3;
                    MFD4 = mfd4;
                }
            }

            public sealed class NavMasterModes
            {
                public MFD MFD1 { get; }

                public MFD MFD2 { get; }

                public MFD MFD3 { get; }

                public MFD MFD4 { get; }

                public NavMasterModes()
                {
                    MFD1 = new MFD();
                    MFD2 = new MFD();
                    MFD3 = new MFD();
                    MFD4 = new MFD();
                }

                public NavMasterModes(MFD mfd1, MFD mfd2)
                {
                    MFD1 = mfd1;
                    MFD2 = mfd2;
                }

                public NavMasterModes(MFD mfd1, MFD mfd2, MFD mfd3, MFD mfd4)
                {
                    MFD1 = mfd1;
                    MFD2 = mfd2;
                    MFD3 = mfd3;
                    MFD4 = mfd4;
                }
            }

            public sealed class MSLOVRDModes
            {
                public MFD MFD1 { get; }

                public MFD MFD2 { get; }

                public MFD MFD3 { get; }

                public MFD MFD4 { get; }

                public MSLOVRDModes()
                {
                    MFD1 = new MFD();
                    MFD2 = new MFD();
                    MFD3 = new MFD();
                    MFD4 = new MFD();
                }

                public MSLOVRDModes(MFD mfd1, MFD mfd2)
                {
                    MFD1 = mfd1;
                    MFD2 = mfd2;
                }

                public MSLOVRDModes(MFD mfd1, MFD mfd2, MFD mfd3, MFD mfd4)
                {
                    MFD1 = mfd1;
                    MFD2 = mfd2;
                    MFD3 = mfd3;
                    MFD4 = mfd4;
                }
            }

            public sealed class DGFTOVRDModes
            {
                public MFD MFD1 { get; }

                public MFD MFD2 { get; }

                public MFD MFD3 { get; }

                public MFD MFD4 { get; }

                public DGFTOVRDModes()
                {
                    MFD1 = new MFD();
                    MFD2 = new MFD();
                    MFD3 = new MFD();
                    MFD4 = new MFD();
                }

                public DGFTOVRDModes(MFD mfd1, MFD mfd2)
                {
                    MFD1 = mfd1;
                    MFD2 = mfd2;
                }

                public DGFTOVRDModes(MFD mfd1, MFD mfd2, MFD mfd3, MFD mfd4)
                {
                    MFD1 = mfd1;
                    MFD2 = mfd2;
                    MFD3 = mfd3;
                    MFD4 = mfd4;
                }
            }

            public sealed class MFD
            {
                public MFDModes LeftMFDMode { get; }

                public MFDModes MiddleMFDMode { get; }

                public MFDModes RightMFDMode { get; }

                public MFD()
                {
                    LeftMFDMode = MFDModes.None;
                    MiddleMFDMode = MFDModes.None;
                    RightMFDMode = MFDModes.None;
                }

                public MFD(MFDModes left, MFDModes middle, MFDModes right)
                {
                    LeftMFDMode = left;
                    MiddleMFDMode = middle;
                    RightMFDMode = right;
                }
            }


            #endregion

            public enum MFDModes
            {
                None,
                HSD,
                FCR,
                WPN,
                TGP,
                HAD,
                SMS
            }
        }

        public sealed class RadioConfig
        {

        }

        public sealed class NavigationConfig
        {
            #region Properties

            public List<Waypoint> Steerpoints { get; private set; }

            public List<Waypoint> PPT { get; private set; }

            #endregion

            #region Constructors

            public NavigationConfig()
            {
                InitializeSteerpoints();
            }

            #endregion

            #region Methods

            private async void InitializeSteerpoints()
            {
                await Task.Run(() =>
                {
                    Steerpoints = new List<Waypoint>(24);
                    PPT = new List<Waypoint>(15);
                });
            }

            #endregion
        }

        public sealed class SystemsConfig
        {
            #region Properties

            public MasterArmModes MasterArmMode { get; }

            #endregion

            #region Constructors

            public SystemsConfig()
            {
                MasterArmMode = MasterArmModes.OFF;
            }

            #endregion

            #region Classes

            public sealed class HUDConfig
            {
                public HUDColors Color { get; }

                public uint Brightness { get; }

                public enum HUDColors
                {
                    Green
                }

                public enum SymWheelPositions
                {
                    ON
                }

                public enum HUDScales
                {
                    VAH
                }

                public enum FPMModes
                {
                    FPM,
                    ATTFPM
                }

                public enum DEDModes
                {
                    DED
                }
            }

            public sealed class ICPConfig
            {
                public MasterModes CurrentMasterMode { get; }

                public uint AllowMSL { get; }

                public uint AllowAGL { get; }

                public uint AllowTFadv { get; }

                public uint Wingspan { get; }

                public uint BingoFuelQuantity { get; }

                public ICPConfig()
                {
                    CurrentMasterMode = MasterModes.Nav;
                    AllowMSL = 10000;
                    AllowAGL = 300;
                    AllowTFadv = 400;
                    Wingspan = 35;
                    BingoFuelQuantity = 2500;

                }

                internal static uint GetWingspanForAircraftModel(string modelId)
                {
                    var wingspan = modelId switch
                    {
                        "Mig-17" => 36,
                        "Mig-21" => 23,
                        "Mig-23" => 45,
                        "Mig-25" => 45,
                        "Mig-27" => 45,
                        "Mig-29" => 36,
                        "Mig-31" => 49,
                        "Su-17" => 44,
                        "Su-25" => 47,
                        "Su-27" => 48,
                        "A-10" => 58,
                        "EF2000" => 35,
                        "F-4" => 38,
                        "F-5" => 27,
                        "F-14" => 64,
                        "F-15" => 43,
                        "F-16" => 32,
                        "F-18" =>37,
                        "F/A-18" => 37,
                        "M2000" => 30,
                        "Tornado" => 46,
                        _ => throw new ArgumentOutOfRangeException(nameof(modelId))
                    };

                    return (uint)wingspan;
                }
            }

            #endregion

            #region enums

            public enum MasterModes
            {
                AG,
                AA,
                Nav,
                COM1,
                COM2
            }

            public enum MasterArmModes
            {
                OFF,
                ON,
                SIM
            }

            public enum VelocityModes
            {
                TAS,
                CAS
            }

            public enum AltitudeModes
            {
                BARO
            }

            #endregion
        }

        public sealed class WeaponsConfig
        {
            #region Properties

            public BombProfile BombProfile1 { get; }

            public BombProfile BombProfile2 { get; }

            public AAMissileProfile AAMProfile { get; }

            public AGMissileProfile AGMProfile { get; }

            #endregion

            #region Constructors

            public WeaponsConfig()
            {
                BombProfile1 = new BombProfile();

                BombProfile2 = new BombProfile
                {
                    SubMode = BombProfile.SubModes.CCIP,
                    FuzeMode = BombProfile.FuzeModes.NOSE,
                    ImpactSpacing = 175,
                    Angle = 45,
                    C2BurstAltitude = 2000,
                    ReleasePulses = 2
                };

                AAMProfile = new AAMissileProfile();

                AGMProfile = new AGMissileProfile();
            }

            #endregion

            #region Classes

            public sealed class BombProfile
            {
                public uint ImpactSpacing { get; set; } = 100;

                public uint ReleasePulses { get; set; }

                public int Angle { get; set; }

                public float C1ArmingDelay1 { get; set; } = 1.0f;

                public float C1ArmingDelay2 { get; set; } = 2.0f;

                public float C2ArmingDelay1 { get; set; } = 1.5f;

                public uint C2BurstAltitude { get; set; } = 1000;

                public SubModes SubMode { get; set; } = SubModes.CCRP;

                public FuzeModes FuzeMode { get; set; } = FuzeModes.NOSE;

                public PairingModes PairingMode { get; set; } = PairingModes.SGL;

                public enum SubModes
                {
                    CCRP,
                    CCIP
                }

                public enum FuzeModes
                {
                    NOSE
                }

                public enum PairingModes
                {
                    SGL,
                    PAIR
                }
            }

            public sealed class AAMissileProfile
            {
                public SidewinderScanModes ScanMode { get; } = SidewinderScanModes.SPOT;

                public SidewinderTDBP TDBP { get; } = SidewinderTDBP.TD;

                public AMRAAMTargetSize TargetSize { get; } = AMRAAMTargetSize.UNKNOWN;

                public enum SidewinderScanModes
                {
                    SPOT,
                    SCAN
                }

                public enum SidewinderTDBP
                {
                    TD,
                    BP
                } // TODO: Rename enum and label members.

                public enum AMRAAMTargetSize
                {
                    UNKNOWN
                }
            }

            public sealed class AGMissileProfile
            {
                private uint _autoPowerWaypoint;
                public AutoPowerModes AutoPowerMode { get; } = AutoPowerModes.OFF;

                public AutoPowerDirections AutoPowerDirection { get; } = AutoPowerDirections.North;

                public uint AutoPowerWaypoint
                {
                    get => _autoPowerWaypoint;
                    set
                    {
                        if (value == 0)
                            _autoPowerWaypoint = 1;

                        else if (value > 0 && value < 25)
                            _autoPowerWaypoint = value;

                        else if (value > 25)
                            _autoPowerWaypoint = 24;
                    }
                }

                public AGMissileProfile()
                {
                    AutoPowerWaypoint = 1;
                }

                public enum AutoPowerModes
                {
                    ON,
                    OFF
                }

                public enum AutoPowerDirections
                {
                    North,
                    East,
                    South,
                    West
                }
            }

            #endregion
        }

        #endregion
    }
}