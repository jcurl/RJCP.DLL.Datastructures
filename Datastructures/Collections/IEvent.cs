namespace RJCP.Core.Collections
{
    using System;

    /// <summary>
    /// Describes a single event.
    /// </summary>
    /// <typeparam name="T">
    /// The information associated with the event. It must allow comparison for event severity.
    /// </typeparam>
    public interface IEvent<out T>
    {
        /// <summary>
        /// Gets the time stamp when the event occurred.
        /// </summary>
        /// <value>The time stamp when the event occurred.</value>
        DateTime TimeStamp { get; }

        /// <summary>
        /// Gets the identifier, which is comparable to determine severity.
        /// </summary>
        /// <value>The identifier, that can be compared for severity.</value>
        T Identifier { get; }
    }
}
