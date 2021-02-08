using System;

namespace Falcon.Core.Events
{
    public sealed class Int16ChangedEventArgs : EventArgs
    {
        #region Properties

        public short OldValue { get; }

        public short NewValue { get; }

        #endregion

        #region Constructors

        public Int16ChangedEventArgs(short oldValue, short newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        #endregion
    }

    public delegate void OnInt16Changed(object sender, Int16ChangedEventArgs e);
}