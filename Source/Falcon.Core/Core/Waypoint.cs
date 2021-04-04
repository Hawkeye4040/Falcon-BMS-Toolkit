using System;
using Falcon.Core.Data;
using Falcon.Core.Events;

namespace Falcon.Core
{
    public sealed class Waypoint : ICloneable, IEquatable<Waypoint>
    {
        #region Fields

        public const byte WP_HAVE_DEPTIME = 1;

        public const byte WP_HAVE_TARGET = 2;

        private const int FLAGS_WIDENED_AT_VERSION = 73;

        #endregion

        #region Properties

        public Vector3S Position { get; set; }

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

        public bool Equals(Waypoint other)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public delegate void OnWaypointChanged(object sender, ValueChangedEventArgs<Waypoint> e);

    public delegate void OnWaypointsChanged(object sender, ValuesChangedEventArgs<Waypoint> e);
}