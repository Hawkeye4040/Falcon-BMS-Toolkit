using System;
using System.Collections.Generic;
using System.Linq;

using Falcon.Core.Data;
using Falcon.Core.Events;

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

        #endregion

        #region Constructors

        protected Entity()
        {
            Position = Vector3S.Zero;
        }

        #endregion
    }

    public delegate void OnEntityChanged(object sender, ValueChangedEventArgs<Entity> e);

    public delegate void OnEntitiesChanged(object sender, ValuesChangedEventArgs<Entity> e);
}