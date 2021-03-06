﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Falcon.Core.Collections
{
    [SuppressMessage("ReSharper", "ClassCanBeSealed.Global")]
    public class ObservableList<T> : IList<T>, IList, INotifyCollectionChanged, INotifyPropertyChanged
    {
        #region Properties

        public int Capacity { get; }

        public int Count => Items.Count;

        public List<T> Items { get; protected set; }

        public bool IsSynchronized { get; protected set; }

        public object SyncRoot { get; protected set; }

        int ICollection.Count => Count;

        int ICollection<T>.Count => Count;

        bool ICollection<T>.IsReadOnly => false;

        public bool IsFixedSize => true;

        bool IList.IsReadOnly => false;

        #endregion

        #region Indexers

        public T this[int index]
        {
            get => Items[index];
            set => Items.Insert(index, value);
        }

        object IList.this[int index]
        {
            get => Items[index];
            set => Items.Insert(index, (T) value);
        }

        #endregion

        #region Constructors

        public ObservableList()
        {
            Capacity = -1; // TODO: determine a better way to represent no capacity. Perhaps uint Capacity and 0 value?
            SyncRoot = this;
            IsSynchronized = false;
            Items = new List<T>();
        }

        public ObservableList(int capacity)
        {
            Capacity = capacity;
            SyncRoot = this;
            IsSynchronized = false;
            Items = new List<T>(Capacity);
        }

        public ObservableList(IEnumerable<T> items)
        {
            Capacity = -1; // TODO: determine a better way to represent no capacity. Perhaps uint Capacity and 0 value?
            SyncRoot = this;
            Items = new List<T>();
            Items.AddRange(items);
        }

        public ObservableList(IEnumerable<T> items, int capacity)
        {
            Capacity = capacity;
            SyncRoot = this;
            Items = new List<T>(Capacity);
            Items.AddRange(items);
        }

        #endregion

        #region Events

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        public void AddRange(IEnumerable<T> items)
        {
            Items.AddRange(items);
        }

        public virtual void Clear()
        {
            Items.Clear();
        }

        public void CopyTo(Array array, int index)
        {
            Items.CopyTo((T []) array, index);
        }

        public void Add(T item)
        {
            Items.Add(item);
        }

        public bool Contains(T item)
        {
            return Items.Contains(item);
        }

        public void CopyTo(T [] array, int arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return Items.Remove(item);
        }

        void ICollection<T>.Clear()
        {
            Clear();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public int Add(object value)
        {
            if (Count >= Items.Count) return -1;

            Items.Add((T) value);

            return Count - 1;
        }

        public bool Contains(object value)
        {
            return Items.Contains((T) value);
        }

        public int IndexOf(object value)
        {
            return Items.IndexOf((T) value);
        }

        public void RemoveAt(int index)
        {
            Items.RemoveAt(index);
        }

        void IList.Insert(int index, object value)
        {
            Items.Insert(index, (T) value);
        }

        void IList.Remove(object value)
        {
            Items.Remove((T) value);
        }

        void IList.Clear()
        {
            Clear();
        }

        public int IndexOf(T item)
        {
            return Items.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            Items.Insert(index, item);
        }

        void IList<T>.RemoveAt(int index)
        {
            Items.RemoveAt(index);
        }

        #endregion
    }
}