namespace Falcon.Core.Rosters
{
    /// <summary>
    ///     Provides the basic functionality for roster entries.
    /// </summary>
    public interface IRosterEntry
    {
        #region Properties

        IRoster Roster { get; }

        #endregion
    }

    public sealed class PilotEntry : IRosterEntry
    {
        #region Properties

        public IRoster Roster { get; }

        #endregion

        #region Constructors

        public PilotEntry(IRoster roster)
        {
            Roster = roster;
        }

        #endregion
    }
}