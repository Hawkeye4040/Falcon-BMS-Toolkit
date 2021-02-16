using System;
using System.Collections.Generic;
using System.Linq;

namespace Falcon.Core
{
    public sealed class Pilot
    {
        #region Properties

        public string Callsign { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        #endregion
    }

    public sealed class PilotChangedEventArgs : EventArgs
    {
        #region Properties

        public Pilot OldPilot { get; }

        public Pilot NewPilot { get; }

        #endregion

        #region Constructors

        public PilotChangedEventArgs(Pilot oldPilot, Pilot newPilot)
        {
            OldPilot = oldPilot;
            NewPilot = newPilot;
        }

        #endregion
    }

    public sealed class PilotsChangedEventArgs : EventArgs
    {
        public PilotsChangedEventArgs(IEnumerable<Pilot> oldPilots, IEnumerable<Pilot> newPilots)
        {
            OldPilots = new List<Pilot>();
            OldPilots.AddRange(oldPilots);
            NewPilots = new List<Pilot>();
            NewPilots.AddRange(newPilots);
        }

        public List<Pilot> OldPilots { get; }

        public List<Pilot> NewPilots { get; }

        public List<Pilot> AddedPilots => NewPilots.Where(pilot => !OldPilots.Contains(pilot)).ToList();

        public List<Pilot> RemovedPilots => OldPilots.Where(pilot => !NewPilots.Contains(pilot)).ToList();
    }

    public delegate void OnPilotChanged(object sender, PilotChangedEventArgs e);

    public delegate void OnPilotsChanged(object sender, PilotsChangedEventArgs e);
}