namespace RJCP.Core.Collections
{
    /// <summary>
    /// Interface for a named item, as required by the <see cref="Generic.NamedItemCollection{T}"/>
    /// </summary>
    public interface INamedItem
    {
        /// <summary>
        /// Gets the name of the particular item.
        /// </summary>
        /// <value>
        /// The name of this particular item.
        /// </value>
        string Name { get; }
    }
}
