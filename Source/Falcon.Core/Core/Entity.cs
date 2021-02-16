using System;
using System.Collections.Generic;
using System.Linq;

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

        #endregion

        #region Constructors

        protected Entity()
        {
            
        }

        #endregion
    }

    public sealed class EntityChangedEventArgs : EventArgs
    {
        #region Properties

        public Entity OldEntity { get; }

        public Entity NewEntity { get; }

        #endregion

        #region Constructors

        public EntityChangedEventArgs(Entity oldEntity, Entity newEntity)
        {
            OldEntity = oldEntity;
            NewEntity = newEntity;
        }

        #endregion
    }

    public sealed class EntitiesChangedEventArgs : EventArgs
    {
        #region Properties

        public List<Entity> OldEntities { get; }

        public List<Entity> NewEntities { get; }

        public List<Entity> AddedEntities => NewEntities.Where(entity => !OldEntities.Contains(entity)).ToList();

        public List<Entity> RemovedEntities => OldEntities.Where(entity => !NewEntities.Contains(entity)).ToList();

        #endregion

        #region Constructors

        public EntitiesChangedEventArgs(IEnumerable<Entity> oldEntities, IEnumerable<Entity> newEntities)
        {
            OldEntities = new List<Entity>();
            OldEntities.AddRange(oldEntities);
            NewEntities = new List<Entity>();
            NewEntities.AddRange(newEntities);
        }

        #endregion
    }

    public delegate void OnEntityChanged(object sender, EntityChangedEventArgs e);

    public delegate void OnEntitiesChanged(object sender, EntitiesChangedEventArgs e);
}