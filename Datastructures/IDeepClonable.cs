namespace RJCP.Core
{
    /// <summary>
    /// Interface IDeepCloneable.
    /// </summary>
    public interface IDeepCloneable : IDeepCloneable<object> { }

    /// <summary>
    /// Interface IDeepCloneable
    /// </summary>
    /// <typeparam name="T">The type that is cloneable.</typeparam>
    public interface IDeepCloneable<out T>
    {
        /// <summary>
        /// Performs a deep clone of the object.
        /// </summary>
        /// <returns>A copy of the original object.</returns>
        T DeepClone();
    }
}
