namespace RhoMicro.Domain.Abstractions
{
    /// <summary>
    /// Interface for types whose instance store their time of creation.
    /// </summary>
    public interface IHasCreationTime
    {
        /// <summary>
        /// Gets the time of creation.
        /// </summary>
        DateTimeOffset CreationTime { get; }
    }
}