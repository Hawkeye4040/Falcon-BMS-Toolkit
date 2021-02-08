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
}