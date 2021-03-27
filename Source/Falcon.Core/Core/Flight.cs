using Falcon.Core.Collections;
using Falcon.Core.Events;
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

        public ObservableList<Pilot> Pilots { get; }

        #endregion

        #region Constructors

        public Flight()
        {
            Pilots = new ObservableList<Pilot>(4);
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

    public delegate void OnFlightsChanged(object sender, ValueChangedEventArgs<Flight> e);

    public delegate void OnFlightChanged(object sender, ValuesChangedEventArgs<Flight> e);
}