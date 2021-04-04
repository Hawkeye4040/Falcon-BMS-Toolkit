using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Falcon.Core.Events;

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
        #region Fields

        public static readonly List<AircraftDefinition> AircraftDefinitions;

        #endregion

        #region Properties

        public string Model { get; }

        public Dictionary<Station, Ordnance> LoadoutConfiguration { get; }

        static AircraftDefinition()
        {
            AircraftDefinitions = new List<AircraftDefinition>();
            InitializeAircraftDefinitions();
        }

        private static async void InitializeAircraftDefinitions()
        {
            await Task.Run(() =>
            {
                // TODO: Initialize aircraft definitions from file here.
            });
        }

        #endregion

        // TODO: Determine if this property should be a pure key-value pair or a custom implementation of IDictionary.
    }

    public delegate void OnAircraftChanged(object sender, ValueChangedEventArgs<Aircraft> e);

    [SuppressMessage("ReSharper", "IdentifierTypo")]
    public delegate void OnAircraftsChanged(object sender, ValuesChangedEventArgs<Aircraft> e);
}