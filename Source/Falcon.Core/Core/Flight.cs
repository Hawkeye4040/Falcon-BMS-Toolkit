using System.Collections.Generic;

namespace Falcon.Core
{
    public sealed class Flight
    {
        #region Properties

        public string Callsign { get; set; }

        public Package Package { get; set; }

        public List<Pilot> Pilots { get; set; }

        #endregion

        public Flight()
        {
            Pilots = new List<Pilot>(4); 
        }
    }
}