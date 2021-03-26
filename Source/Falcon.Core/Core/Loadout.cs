using System;
using System.Collections.Generic;

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
            throw new NotImplementedException();
        }

        public static List<T> GetOrdnanceOfType<T>(Loadout loadout)
            where T : Ordnance
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public sealed class Station
    {
    }
}