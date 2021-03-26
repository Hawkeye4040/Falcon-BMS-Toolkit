using System;
using System.Collections.Generic;
using System.Linq;

namespace Falcon.Core.Events
{
    public sealed class ValueChangedEventArgs<T> : EventArgs
    {
        #region Properties

        public T OldValue { get; }

        public T NewValue { get; }

        #endregion

        #region Constructors

        public ValueChangedEventArgs(T oldValue, T newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        #endregion
    }

    public sealed class ValuesChangedEventArgs<T> : EventArgs
    {
        #region Properties

        public List<T> OldValues { get; }

        public List<T> NewValues { get; }

        public List<T> AddedValues => NewValues.Where(value => !OldValues.Contains(value)).ToList();

        public List<T> RemovedValues => OldValues.Where(value => !NewValues.Contains(value)).ToList();

        #endregion

        #region Constructors

        public ValuesChangedEventArgs(IEnumerable<T> oldValues, IEnumerable<T> newValues)
        {
            OldValues = new List<T>();
            OldValues.AddRange(oldValues);
            NewValues = new List<T>();
            NewValues.AddRange(newValues);
        }

        #endregion
    }

    public delegate void OnValueChanged<T>(object sender, ValueChangedEventArgs<T> e);

    public delegate void OnValuesChanged<T>(object sender, ValuesChangedEventArgs<T> e);
}