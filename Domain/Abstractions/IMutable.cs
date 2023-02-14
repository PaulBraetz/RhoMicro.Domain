namespace RhoMicro.Domain.Abstractions
{
    /// <summary>
    /// Interface for types whose instances are mutable.
    /// </summary>
    public interface IMutable : IHasCreationTime
    {
        /// <summary>
        /// Gets the time at which the last change has occured.
        /// </summary>
		DateTimeOffset LastChangedAt { get; }
    }
}