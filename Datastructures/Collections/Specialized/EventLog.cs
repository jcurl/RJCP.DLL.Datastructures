namespace RJCP.Core.Collections.Specialized
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;

    /// <summary>
    /// A list implementing an event log.
    /// </summary>
    /// <typeparam name="T">The type of data used for identification in the <see cref="IEvent{T}"/>.</typeparam>
    public class EventLog<T> : IList<IEvent<T>>, INotifyCollectionChanged
    {
        private readonly List<IEvent<T>> m_Events = new List<IEvent<T>>();

        #region IList
        /// <summary>
        /// Gets or sets the <see cref="IEvent{T}"/> at the specified index.
        /// </summary>
        /// <param name="index">The index to the list.</param>
        /// <returns>The event that occurred at this slot.</returns>
        /// <exception cref="NotSupportedException">
        /// It is not allowed to change the slot at the location specified.
        /// </exception>
        public IEvent<T> this[int index]
        {
            get { return m_Events[index]; }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Determines the index of a specific item in the <see cref="EventLog{T}"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="EventLog{T}"/>.</param>
        /// <returns>The index of <paramref name="item"/> if found in the list; otherwise, -1.</returns>
        public int IndexOf(IEvent<T> item)
        {
            return m_Events.IndexOf(item);
        }

        /// <summary>
        /// Inserts an item to the <see cref="EventLog{T}"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
        /// <param name="item">The object to insert into the <see cref="EventLog{T}"/>.</param>
        /// <exception cref="NotSupportedException">
        /// It is not allowed to insert except at the end of the list.
        /// </exception>
        public void Insert(int index, IEvent<T> item)
        {
            if (index != m_Events.Count) throw new NotSupportedException();
            Add(item);
        }

        /// <summary>
        /// Removes the <see cref="EventLog{T}"/> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="NotSupportedException">It is not allowed to remove elements from the list.</exception>
        public void RemoveAt(int index)
        {
            throw new NotSupportedException();
        }
        #endregion

        #region ICollection
        /// <summary>
        /// Gets the number of elements contained in the <see cref="EventLog{T}"/>.
        /// </summary>
        /// <value>The number of event entries.</value>
        public int Count { get { return m_Events.Count; } }

        /// <summary>
        /// Gets a value indicating whether the <see cref="EventLog{T}"/> is read-only.
        /// </summary>
        /// <value>
        /// Is <see langword="true"/> if this instance is read only; otherwise, <see langword="false"/>. This function
        /// always returns <see langword="false"/>.
        /// </value>
        public bool IsReadOnly { get { return false; } }

        /// <summary>
        /// Adds an item to the end of <see cref="EventLog{T}"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="EventLog{T}"/>.</param>
        /// <remarks>
        /// When adding a new element to the event log, the <see cref="Max"/> property is updated if
        /// <paramref name="item"/> is not <see langword="null"/>, this new element <typeparamref name="T"/> implements
        /// <see cref="IComparable{T}"/> and precedes the existing <see cref="Max"/> element.
        /// </remarks>
        public void Add(IEvent<T> item)
        {
            m_Events.Add(item);
            UpdateMax(item);

            OnCollectionChanged(this,
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, m_Events.Count - 1));
        }

        /// <summary>
        /// Removes all items from the <see cref="EventLog{T}"/>.
        /// </summary>
        public void Clear()
        {
            m_Events.Clear();
            Max = null;

            OnCollectionChanged(this,
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary>
        /// Determines whether the <see cref="EventLog{T}"/> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="EventLog{T}"/>.</param>
        /// <returns>
        /// is <see langword="true"/> if <paramref name="item"/> is found in the <see cref="EventLog{T}"/>; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        public bool Contains(IEvent<T> item)
        {
            return m_Events.Contains(item);
        }

        /// <summary>
        /// Copies the elements of the <see cref="EventLog{T}"/> to an <see cref="Array"/>, starting at a particular
        /// <see cref="Array"/> index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="Array"/> that is the destination of the elements copied from
        /// <see cref="EventLog{T}"/>. The <see cref="Array"/> must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        public void CopyTo(IEvent<T>[] array, int arrayIndex)
        {
            m_Events.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="EventLog{T}"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="EventLog{T}"/>.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="item"/> was successfully removed from the
        /// <see cref="EventLog{T}"/>; otherwise, <see langword="false"/>. This method also returns false if
        /// <paramref name="item"/> is not found in the original <see cref="EventLog{T}"/>.
        /// </returns>
        /// <exception cref="NotSupportedException">It is not allowed to remove elements from the list.</exception>
        public bool Remove(IEvent<T> item)
        {
            throw new NotSupportedException();
        }
        #endregion

        #region IEnumerable
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>A <see cref="EventLog{T}"/> that can be used to iterate through the collection.</returns>
        public IEnumerator<IEvent<T>> GetEnumerator()
        {
            return m_Events.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="EventLog{T}"/> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_Events.GetEnumerator();
        }
        #endregion

        #region INotifyCollectionChanged
        /// <summary>
        /// Occurs when events have been added to this collection.
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// Handles the <see cref="CollectionChanged" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            NotifyCollectionChangedEventHandler handler = CollectionChanged;
            if (handler != null) handler(sender, args);
        }
        #endregion

        private void UpdateMax(IEvent<T> item)
        {
            // https://learn.microsoft.com/en-us/dotnet/standard/collections/comparisons-and-sorts-within-collections

            if (item != null && item.Identifier is IComparable<T> comparable) {
                if (Max == null) {
                    Max = item;
                    return;
                }

                int compare = comparable.CompareTo(Max.Identifier);
                if (compare == 0 && item.TimeStamp < Max.TimeStamp || compare < 0)
                    Max = item;
            }
        }

        /// <summary>
        /// Gets the first added entry of the highest "severity".
        /// </summary>
        /// <value>
        /// The first added entry of the highest "severity", as defined by the <see cref="IComparable{T}.CompareTo(T)"/>
        /// method.
        /// </value>
        /// <remarks>
        /// The type <typeparamref name="T"/> must implement <see cref="IComparable{T}"/> for this property to be set.
        /// </remarks>
        public IEvent<T> Max { get; private set; }
    }
}
