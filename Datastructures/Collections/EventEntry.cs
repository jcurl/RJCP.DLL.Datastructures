namespace RJCP.Core.Collections
{
    using System;

    /// <summary>
    /// Provides a basic implementation of an event entry.
    /// </summary>
    /// <typeparam name="T">The user provided information with each event.</typeparam>
    public class EventEntry<T> : IEvent<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventEntry{T}"/> class.
        /// </summary>
        public EventEntry()
        {
            TimeStamp = DateTime.Now.ToLocalTime();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventEntry{T}"/> class.
        /// </summary>
        /// <param name="data">The data associated with this event entry.</param>
        public EventEntry(T data)
        {
            TimeStamp = DateTime.Now.ToLocalTime();
            Identifier = data;
        }

        /// <summary>
        /// Gets the time stamp when the event occurred.
        /// </summary>
        /// <value>The time stamp when the event occurred.</value>
        public DateTime TimeStamp { get; }

        /// <summary>
        /// Gets the identifier, which is comparable to determine severity.
        /// </summary>
        /// <value>The identifier, that can be compared for severity.</value>
        public T Identifier { get; }
    }
}
