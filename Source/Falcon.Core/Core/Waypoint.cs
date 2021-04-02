using System;
using System.Collections.Generic;
using System.Linq;

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

        public bool Equals(Waypoint? other)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public sealed class WaypointChangedEventArgs : EventArgs
    {
        #region Properties

        public Waypoint OldWaypoint { get; }

        public Waypoint NewWaypoint { get; }

        #endregion

        #region Constructors

        public WaypointChangedEventArgs(Waypoint oldWaypoint, Waypoint newWaypoint)
        {
            OldWaypoint = oldWaypoint;
            NewWaypoint = newWaypoint;
        }

        #endregion
    }

    public sealed class WaypointsChangedEventArgs : EventArgs
    {
        #region Properties

        public List<Waypoint> OldWaypoints { get; }

        public List<Waypoint> NewWaypoints { get; }

        public List<Waypoint> AddedWaypoints =>
            NewWaypoints.Where(waypoint => !OldWaypoints.Contains(waypoint)).ToList();

        public List<Waypoint> RemovedWaypoints =>
            OldWaypoints.Where(waypoint => !NewWaypoints.Contains(waypoint)).ToList();

        #endregion

        #region Constructors

        public WaypointsChangedEventArgs(IEnumerable<Waypoint> oldWaypoints, IEnumerable<Waypoint> newWaypoints)
        {
            OldWaypoints = new List<Waypoint>();
            OldWaypoints.AddRange(oldWaypoints);
            NewWaypoints = new List<Waypoint>();
            NewWaypoints.AddRange(newWaypoints);
        }

        #endregion
    }

    public delegate void OnWaypointChanged(object sender, ValueChangedEventArgs<Waypoint> e);

    public delegate void OnWaypointsChanged(object sender, ValuesChangedEventArgs<Waypoint> e);
}