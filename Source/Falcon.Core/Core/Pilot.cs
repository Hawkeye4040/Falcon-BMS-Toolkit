using Falcon.Core.Events;

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

    public delegate void OnPilotChanged(object sender, ValueChangedEventArgs<Pilot> e);

    public delegate void OnPilotsChanged(object sender, ValuesChangedEventArgs<Pilot> e);
}