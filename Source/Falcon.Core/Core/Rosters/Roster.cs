using System.Collections;
using System.Collections.Generic;

using Falcon.Core.Collections;

namespace Falcon.Core.Rosters
{
    /// <summary>
    ///     Provides the basic functionality for rosters.
    /// </summary>
    public interface IRoster : IList<IRosterEntry>
    {
        #region Properties

        ObservableList<IRosterEntry> Entries { get; }

        #endregion
    }

    public abstract class Roster : IRoster
    {
        #region Properties

        int ICollection<IRosterEntry>.Count => Entries.Count;

        bool ICollection<IRosterEntry>.IsReadOnly => false;

        public ObservableList<IRosterEntry> Entries { get; protected set; }

        #endregion

        #region Indexers

        public IRosterEntry this[int index]
        {
            get => Entries[index];
            set => Entries.Insert(index, value);
        }

        #endregion

        #region Methods

        public void Add(IRosterEntry entry)
        {
            Entries.Add(entry);
        }

        public void Clear()
        {
            Entries.Clear();
        }

        public bool Contains(IRosterEntry entry)
        {
            return Entries.Contains(entry);
        }

        public void CopyTo(IRosterEntry [] array, int arrayIndex)
        {
            Entries.CopyTo(array, arrayIndex);
        }

        public bool Remove(IRosterEntry entry)
        {
            return Entries.Remove(entry);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<IRosterEntry> GetEnumerator()
        {
            return Entries.GetEnumerator();
        }

        public int IndexOf(IRosterEntry entry)
        {
            return Entries.IndexOf(entry);
        }

        public void Insert(int index, IRosterEntry entry)
        {
            Entries.Insert(index, entry);
        }

        public void RemoveAt(int index)
        {
            Entries.RemoveAt(index);
        }

        #endregion
    }

    public sealed class WingRoster : Roster
    {

    }

    public sealed class SquadronRoster : Roster
    {

    }
}