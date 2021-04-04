using System;
using Falcon.Core.Data;
using Falcon.Core.Events;

namespace Falcon.Core
{
    // TODO: Determine if Waypoint should be changed to struct as it's intended behavior is more like a value type than a reference type.

    public sealed class Waypoint : ICloneable, IEquatable<Waypoint>
    {
        #region Fields

        public const byte WP_HAVE_DEPTIME = 1;

        public const byte WP_HAVE_TARGET = 2;

        private const int FLAGS_WIDENED_AT_VERSION = 73;

        private Vector3S _position => Position;

        private uint _arrival => Arrival;

        private uint _departure => Departure;

        private byte _action => Action;

        private byte _formation => Formation;

        private uint _flags => Flags;

        private uint _speed => Speed;

        private uint _target => Target;

        #endregion

        #region Properties

        public Vector3S Position { get; set; }

        public uint Arrival { get; set; }

        public uint Departure { get; set; }

        public byte Action { get; set; } // TODO: Convert to enum Action : byte

        public byte Formation { get; set; } // TODO: Convert to enum Formation : byte

        public uint Flags { get; set; }

        public uint Speed { get; set; }

        public uint Target { get; set; }

        #endregion

        #region Methods

        public Waypoint Clone()
        {
            throw new NotImplementedException();
        }

        object ICloneable.Clone() => Clone();

        public bool Equals(Waypoint other)
        {
            if (other is null) return false;

            if (ReferenceEquals(this, other)) return true;

            return Position.Equals(other.Position) && Arrival == other.Arrival && Departure == other.Departure &&
                   Action == other.Action && Formation == other.Formation && Flags == other.Flags &&
                   Speed == other.Speed && Target == other.Target;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Waypoint);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_position, _arrival, _departure, _action, _formation, _flags, _speed, _target); // TODO: Ensure private computed properties return a proper hash code.
        }

        #endregion
    }

    public delegate void OnWaypointChanged(object sender, ValueChangedEventArgs<Waypoint> e);

    public delegate void OnWaypointsChanged(object sender, ValuesChangedEventArgs<Waypoint> e);
}