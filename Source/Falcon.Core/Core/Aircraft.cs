using System.Collections.Generic;

namespace Falcon.Core
{
    /// <summary>
    ///     Represents an instance of an aircraft for type definitions see <see cref="AircraftDefinition" />.
    /// </summary>
    public sealed class Aircraft
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the model identifier of this <see cref="Aircraft" />.
        /// </summary>
        /// <remarks>
        ///     This should match the corresponding <see cref="AircraftDefinition" />'s <see cref="AircraftDefinition.Model" />
        ///     property exactly.
        /// </remarks>
        public string Model { get; set; }

        public Pilot Pilot { get; set; }

        public Loadout Loadout { get; set; }

        #endregion
    }

    public sealed class AircraftDefinition
    {
        #region Properties

        public string Model { get; }

        public Dictionary<Station, Ordnance> LoadoutConfiguration { get; }

        #endregion

        // TODO: Determine if this property should be a pure key-value pair or a custom implementation of IDictionary.
    }
}