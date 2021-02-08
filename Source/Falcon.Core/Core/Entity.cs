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

        public short X { get; set; }

        public short Y { get; set; }

        public short Z { get; set; }

        // TODO: Determine if an Int16 3-dimensional vector struct will better store Entity Coordinates so as to wire up an Event when Position of the entity changes.

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