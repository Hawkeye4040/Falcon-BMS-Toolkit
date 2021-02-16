using System;
using System.Collections.Generic;

namespace Falcon.Core
{
    public sealed class Aircraft
    {
        #region Properties

        public string Model { get; set; }

        public Pilot Pilot { get; set; }

        public Loadout Loadout { get; set; }

        #endregion
    }

    public sealed class AircraftDefinition
    {
        public string Model { get; }

        public Dictionary<Station, Ordnance> LoadoutConfiguration { get; } // TODO: Determine if this property should be a pure key-value pair or a custom implementation of IDictionary.
    }
}