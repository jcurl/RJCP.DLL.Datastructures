namespace RJCP.Core.Collections.Generic
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A collection of items which are named and then indexed in a dictionary by name.
    /// </summary>
    /// <typeparam name="T">The type of the object which this collection manages.</typeparam>
    public class NamedItemCollection<T> : ICollection<T> where T : class, INamedItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedItemCollection{T}"/> class without a <see cref="Name"/>.
        /// </summary>
        /// <remarks>
        /// This constructor is intended for collections that need to be able to set the name during the constructor,
        /// instead of prior (for example, a file might need to be parsed to determine the name). The name should be set
        /// once the construction of your object is complete.
        /// </remarks>
        protected NamedItemCollection() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedItemCollection{T}"/> class.
        /// </summary>
        /// <param name="name">The name of the objects that this instance manages.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="name"/> may not be <see langword="null"/>.
        /// </exception>
        public NamedItemCollection(string name)
        {
            Name = name;
        }

        private string m_Name;

        /// <summary>
        /// Gets the name of this collection.
        /// </summary>
        /// <value>The name of this collection.</value>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> may not be <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Name is read only. It has already been assigned, so cannot be assigned a second time.
        /// </exception>
        public string Name
        {
            get
            {
                return m_Name ?? GetType().ToString();
            }

            protected set
            {
                ThrowHelper.ThrowIfNull(value);
                if (m_Name != null) throw new InvalidOperationException("Name is read only");
                m_Name = value;
            }
        }

        private readonly Dictionary<string, T> m_Items = new Dictionary<string, T>();

        /// <summary>
        /// Gets the item for the given <paramref name="key"/>.
        /// </summary>
        /// <value>The requested item.</value>
        /// <param name="key">The item name.</param>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> may not be <see langword="null"/>.</exception>
        /// <exception cref="KeyNotFoundException">Key not found in the collection</exception>
        public virtual T this[string key]
        {
            get
            {
                ThrowHelper.ThrowIfNull(key);
                if (m_Items.TryGetValue(key, out T item)) return item;
                throw new KeyNotFoundException("Key not found in the collection");
            }
        }

        /// <summary>
        /// Tries the get value for the given key.
        /// </summary>
        /// <param name="key">The key to search for in the index.</param>
        /// <param name="value">The value for the key, if it was found.</param>
        /// <returns>
        /// Returns <see langword="true"/> if the key could be found, and so <paramref name="value"/> contains the
        /// value for the <paramref name="key"/>, <see langword="false"/> otherwise.
        /// </returns>
        public bool TryGetValue(string key, out T value)
        {
            return m_Items.TryGetValue(key, out value);
        }

        #region IEnumerable<T>
        /// <summary>
        /// Returns an enumerator that iterates through the collection of items.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection of items.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return m_Items.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region ICollection<T>
        /// <summary>
        /// Adds an item to this collection.
        /// </summary>
        /// <param name="item">The item to add to the collection.</param>
        /// <exception cref="InvalidOperationException">Collection is read only.</exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="item"/> may not be <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// an <paramref name="item"/> with same name already exists in the collection.
        /// </exception>
        /// <remarks>
        /// It is expected that the name of the item does not change after being added. Doing so may result in unexpected
        /// behavior.
        /// </remarks>
        public void Add(T item)
        {
            OnAdd(item);
            InternalAdd(item);
            try {
                OnAddComplete(item);
            } catch {
                m_Items.Remove(item.Name);
                throw;
            }
        }

        /// <summary>
        /// Adds a range of items to the collection.
        /// </summary>
        /// <param name="items">The items that should be added.</param>
        /// <exception cref="InvalidOperationException">Collection is read only.</exception>
        /// <exception cref="ArgumentNullException">
        /// Argument <paramref name="items"/> is <see langword="null"/>
        /// <para>- or -</para>
        /// An element in <paramref name="items"/> may not be <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// An invalid element name in <paramref name="items"/>; or an element in <paramref name="items"/> with same name
        /// already exists in the collection.
        /// </exception>
        public void AddRange(IEnumerable<T> items)
        {
            ThrowHelper.ThrowIfNull(items);

            foreach (T item in items) {
                OnAdd(item);
                InternalAddCheck(item);
            }
            foreach (T item in items) {
                m_Items.Add(item.Name, item);
                try {
                    OnAddComplete(item);
                } catch {
                    m_Items.Remove(item.Name);
                    throw;
                }
            }
        }

        private void InternalAddCheck(T item)
        {
            ThrowHelper.ThrowIfNull(item);
            if (item.Name == null) throw new ArgumentException("Name may not be null", nameof(item));
            if (IsReadOnly) throw new InvalidOperationException("Collection is read only");
            if (m_Items.ContainsKey(item.Name)) {
                string message = string.Format("Item with name '{0}' already exists in the collection", item.Name);
                throw new ArgumentException(message);
            }
        }

        /// <summary>
        /// Adds the item to the collection directly without raising events.
        /// </summary>
        /// <param name="item">The item that should be added.</param>
        /// <exception cref="InvalidOperationException">Collection is read only.</exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="item"/> may not be <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// an <paramref name="item"/> with same name already exists in the collection.
        /// </exception>
        /// <remarks>
        /// This method adds an item to the collection. This allows items to be added to the collection within the
        /// constructor and being independent of derived classes to avoid undefined behavior in .NET.
        /// </remarks>
        protected void InternalAdd(T item)
        {
            InternalAddCheck(item);
            m_Items.Add(item.Name, item);
        }

        /// <summary>
        /// A derived class can override this method to perform additional checks on the item before it is added.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <remarks>
        /// Implementor should override this method and raise an exception to prevent the item from being added.
        /// </remarks>
        protected virtual void OnAdd(T item) { }

        /// <summary>
        /// Called after the item is added to the collection.
        /// </summary>
        /// <param name="item">The item that was added to the collection.</param>
        protected virtual void OnAddComplete(T item) { }

        /// <summary>
        /// Removes all items from this collection.
        /// </summary>
        /// <exception cref="InvalidOperationException">Collection is read only.</exception>
        public void Clear()
        {
            if (IsReadOnly) throw new InvalidOperationException("Collection is read only");

            OnClear();
            InternalClear();
            OnClearComplete();
        }

        /// <summary>
        /// Clears the internal collection without raising events.
        /// </summary>
        protected void InternalClear()
        {
            m_Items.Clear();
        }

        /// <summary>
        /// Called before the collection is about to be cleared.
        /// </summary>
        /// <remarks>
        /// If there is an error that should prevent the collection being cleared, an exception should be raised here.
        /// </remarks>
        protected virtual void OnClear() { }

        /// <summary>
        /// Called after the internal collection is cleared.
        /// </summary>
        protected virtual void OnClearComplete() { }

        /// <summary>
        /// Determines whether the collection contains an item of the specified name.
        /// </summary>
        /// <param name="itemName">The item name to check for.</param>
        /// <returns><see langword="true"/> if the collection contains an item with the name specified.</returns>
        /// <remarks>
        /// This method returns <see langword="false"/> if no element with the keyword exists. If <paramref name="itemName"/>
        /// is <see langword="null"/>, then <see langword="false"/> is returned.
        /// </remarks>
        public bool Contains(string itemName)
        {
            if (itemName == null) return false;
            return m_Items.ContainsKey(itemName);
        }

        /// <summary>
        /// Determines whether the collection contains an item of the same name.
        /// </summary>
        /// <param name="item">The item whose name to locate in the collection.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="item"/> is found in the collection; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="item"/> may not be <see langword="null"/>.
        /// </exception>
        public bool Contains(T item)
        {
            ThrowHelper.ThrowIfNull(item);
            return m_Items.ContainsValue(item);
        }

        /// <summary>
        /// Copies the collection of items in the collection to the specified array.
        /// </summary>
        /// <param name="array">The array to copy the collection to.</param>
        /// <param name="arrayIndex">Index in the array where to start copying to.</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than zero.</exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="array"/> is multidimensional; or the number of elements in the collection is greater than the
        /// available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>; or
        /// the collection cannot be automatically cast to the type of the destination <paramref name="array"/>.
        /// </exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            m_Items.Values.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of elements contained in the collection.
        /// </summary>
        /// <returns>The number of elements contained in the collection.</returns>
        public int Count
        {
            get
            {
                return m_Items.Count;
            }
        }

        private bool m_IsReadOnly;

        /// <summary>
        /// Gets a value indicating whether the collection is read-only.
        /// </summary>
        /// <exception cref="InvalidOperationException">Collection is read only.</exception>
        /// <returns><see langword="true"/> if the collection is read-only; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// Initially, the collection is read/write. You may write to this property to set the collection to read-only,
        /// which the collection can never be made read/write again.
        /// </remarks>
        public bool IsReadOnly
        {
            get { return m_IsReadOnly; }
            set
            {
                if (m_IsReadOnly != value) {
                    if (m_IsReadOnly) throw new InvalidOperationException("Collection is read only");
                    m_IsReadOnly = value;
                }
            }
        }

        /// <summary>
        /// Removes the occurrence of the item from the collection.
        /// </summary>
        /// <param name="item">The item to remove from the collection.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="item"/> was successfully removed from the collection; otherwise,
        /// <see langword="false"/>. This method also returns <see langword="false"/> if <paramref name="item"/> is not
        /// found in the collection.
        /// </returns>
        /// <exception cref="InvalidOperationException">Collection is read only.</exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="item"/> may not be <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException"><see cref="INamedItem.Name"/> field of <paramref name="item"/> is <see langword="null"/>.</exception>
        public bool Remove(T item)
        {
            ThrowHelper.ThrowIfNull(item);
            if (!Contains(item)) return false;
            return InternalRemoveWithChecks(item);
        }

        /// <summary>
        /// Removes the occurrence of the item having the specified name from the collection.
        /// </summary>
        /// <param name="itemName">The item name to remove from the collection.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="itemName"/> was successfully removed from the collection;
        /// otherwise, <see langword="false"/>. This method also returns <see langword="false"/> if
        /// <paramref name="itemName"/> is not found in the collection.
        /// </returns>
        /// <exception cref="InvalidOperationException">Collection is read only.</exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="itemName"/> may not be <see langword="null"/>.
        /// </exception>
        public bool Remove(string itemName)
        {
            ThrowHelper.ThrowIfNull(itemName);
            if (!Contains(itemName)) return false;
            return InternalRemoveWithChecks(m_Items[itemName]);
        }

        private bool InternalRemoveWithChecks(T item)
        {
            if (IsReadOnly) throw new InvalidOperationException("Collection is read only");
            if (item.Name == null) throw new ArgumentException("Name may not be null", nameof(item));

            OnRemove(item);
            if (m_Items.Remove(item.Name)) {
                try {
                    OnRemoveComplete(item);
                    return true;
                } catch {
                    m_Items.Add(item.Name, item);
                    throw;
                }
            }
            return false;
        }

        /// <summary>
        /// Internally remove the item from the collection without raising events.
        /// </summary>
        /// <param name="itemName">The item identifier that should be removed.</param>
        /// <returns><see langword="true"/> if the item was removed, <see langword="false"/> otherwise.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="itemName"/> may not be <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">Collection is read only.</exception>
        protected bool InternalRemove(string itemName)
        {
            ThrowHelper.ThrowIfNull(itemName);
            if (IsReadOnly) throw new InvalidOperationException("Collection is read only");
            return m_Items.Remove(itemName);
        }

        /// <summary>
        /// Called before an item is removed from the collection.
        /// </summary>
        /// <param name="item">The item to be removed.</param>
        protected virtual void OnRemove(T item) { }

        /// <summary>
        /// Called when the item has been removed from the collection.
        /// </summary>
        /// <param name="item">The item that was removed.</param>
        /// <remarks>This method is only called if the object was found in the collection and was removed.</remarks>
        protected virtual void OnRemoveComplete(T item) { }
        #endregion
    }
}
