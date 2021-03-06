﻿using System;
using Falcon.Core.Data;
using Falcon.Core.Events;

namespace Falcon.Core
{
    public struct Waypoint : ICloneable, IEquatable<Waypoint>
    {
        #region Fields

        public const byte WP_HAVE_DEPTIME = 1;

        public const byte WP_HAVE_TARGET = 2;

        private const int FLAGS_WIDENED_AT_VERSION = 73;

        private Vector3S _position => Position;

        private double _elevation => Elevation;

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

        public double Elevation { get; set; }

        public uint Arrival { get; set; }

        public uint Departure { get; set; }

        public byte Action { get; set; } // TODO: Convert to enum Actions : byte

        public byte Formation { get; set; } // TODO: Convert to enum Formations : byte

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
            return Position.Equals(other.Position) && Elevation.Equals(other.Elevation) && Arrival == other.Arrival &&
                   Departure == other.Departure && Action == other.Action && Formation == other.Formation &&
                   Flags == other.Flags && Speed == other.Speed && Target == other.Target;
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equals((Waypoint)obj);
        }

        public override int GetHashCode()
        {
            HashCode hashCode = new HashCode();
            hashCode.Add(_position);
            hashCode.Add(_elevation);
            hashCode.Add(_arrival);
            hashCode.Add(_departure);
            hashCode.Add(_action);
            hashCode.Add(_formation);
            hashCode.Add(_flags);
            hashCode.Add(_speed);
            hashCode.Add(_target);
            return hashCode.ToHashCode();
        }

        #endregion
    }

    #region Delegates

    public delegate void OnWaypointChanged(object sender, ValueChangedEventArgs<Waypoint> e);

    public delegate void OnWaypointsChanged(object sender, ValuesChangedEventArgs<Waypoint> e);

    #endregion
}