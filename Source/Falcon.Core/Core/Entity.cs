using Falcon.Core.Data;

namespace Falcon.Core
{
    /// <summary>
    ///     Base class for most map objects in BMS.
    /// </summary>
    /// <remarks>
    ///     Called CampaignBase in Weapon Delivery Planner.
    /// </remarks>
    public abstract class Entity
    {
        #region Properties

        public ushort EntityType { get; protected set; }

        public Vector3S Position { get; set; }

        public uint SpotTime { get; set; }

        public short Spotted { get; set; }

        public short BaseFlags { get; set; }

        public byte Owner { get; set; }

        protected Entity()
        {

        }

        #endregion
    }
}