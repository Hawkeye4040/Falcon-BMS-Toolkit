using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Falcon.Core
{
    public sealed class Flight
    {
        #region Properties

        public string Callsign { get; set; }

        public Package Package { get; set; }

        public ObservableCollection<Pilot> Pilots { get; }

        #endregion

        #region Constructors

        public Flight()
        {
            Pilots = new ObservableCollection<Pilot>(4);
        }

        #endregion

        #region Methods

        public void Add(Pilot pilot)
        {
            Pilots.Add(pilot);
        }

        public void Add(IEnumerable<Pilot> pilots)
        {
            Pilots.AddRange(pilots);
        }

        #endregion
    }

    public sealed class FlightChangedEventArgs : EventArgs
    {
        #region Properties

        public Flight OldFlight { get; }

        public Flight NewFlight { get; }

        #endregion

        #region Constructors

        public FlightChangedEventArgs(Flight oldFlight, Flight newFlight)
        {
            OldFlight = oldFlight;
            NewFlight = newFlight;
        }

        #endregion
    }

    public sealed class FlightsChangedEventArgs : EventArgs
    {
        #region Properties

        public List<Flight> OldFlights { get; }

        public List<Flight> NewFlights { get; }

        public List<Flight> AddedFlights => NewFlights.Where(flight => !OldFlights.Contains(flight)).ToList();

        public List<Flight> RemovedFlights => OldFlights.Where(flight => !NewFlights.Contains(flight)).ToList();

        #endregion

        #region Constructors

        public FlightsChangedEventArgs(IEnumerable<Flight> oldFlights, IEnumerable<Flight> newFlights)
        {
            OldFlights = new List<Flight>();
            OldFlights.AddRange(oldFlights);
            NewFlights = new List<Flight>();
            NewFlights.AddRange(newFlights);
        }

        #endregion
    }

    public delegate void OnFlightsChanged(object sender, FlightsChangedEventArgs e);

    public delegate void OnFlightChanged(object sender, FlightChangedEventArgs e);
}