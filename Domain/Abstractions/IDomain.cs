namespace RhoMicro.Domain.Abstractions
{
    /// <summary>
    /// Represents a domain capable of capturing and releasing entity state.
    /// </summary>
    public interface IDomain
    {
        /// <summary>
        /// Captures the state of an entity represented by a data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer object to capture state from.</param>
        /// <returns>The entity represented by the data transfer object.</returns>
        IEntity Capture(IDto dto);
        /// <summary>
        /// Releases the state of an entity into a new data transfer object.
        /// </summary>
        /// <param name="entity">The enitty whose state to release.</param>
        /// <returns>A new data transfer object containing the state released from <paramref name="entity"/>.</returns>
        IDto Release(IEntity entity);
    }
}
