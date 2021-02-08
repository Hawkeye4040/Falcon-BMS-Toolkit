using System;

namespace Falcon.Core
{
    public sealed class Waypoint : ICloneable
    {
        #region Fields

        public const byte WP_HAVE_DEPTIME = 1;

        public const byte WP_HAVE_TARGET = 2;

        private const int FLAGS_WIDENED_AT_VERSION = 73;

        #endregion

        #region Properties

        // TODO: Determine if an Int16 3-dimensional vector struct will better store Entity Coordinates so as to wire up an Event when Position of the entity changes.

        public short X { get; set; }

        public short Y { get; set; }

        public short Z { get; set; }

        public uint Arrival { get; set; }

        public uint Departure { get; set; }

        public byte Action { get; set; }

        public byte Formation { get; set; }

        public uint Flags { get; set; }

        public uint Speed { get; set; }

        public uint Target { get; set; }

        #endregion

        #region Methods

        public object Clone()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}