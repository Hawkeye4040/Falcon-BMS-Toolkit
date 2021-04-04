using System.Collections.Generic;
using System.Linq;
using Falcon.Core.Events;

namespace Falcon.Core
{
    /// <summary>
    ///     Represents an <see cref="Aircraft" /> ordnance loadout instance.
    /// </summary>
    public sealed class Loadout
    {
        #region Properties

        public List<Station> Stations { get; set; }

        #endregion

        #region Methods

        public static List<Ordnance> GetOrdnance(Loadout loadout)
        {
            return loadout.Stations.Select(station => station.Ordnance).ToList();
        }

        public static List<T> GetOrdnanceOfType<T>(Loadout loadout)
            where T : Ordnance
        {
            List<T> ordnance = new List<T>();

            foreach (Station station in loadout.Stations)
            {
                if (station.Ordnance is T stationOrdnance)
                    ordnance.Add(stationOrdnance);
            }

            return ordnance;
        }

        #endregion
    }

    public sealed class Station
    {
        private Ordnance _ordnance;

        public Ordnance Ordnance
        {
            get => _ordnance;
            set
            {
                ValueChangedEventArgs<Ordnance> e = new ValueChangedEventArgs<Ordnance>(_ordnance, value);

                _ordnance = value;

                if (e.OldValue != e.NewValue)
                    OrdnanceChanged?.Invoke(this, e);
            }
        }

        public event OnOrdnanceChanged OrdnanceChanged;
    }
}