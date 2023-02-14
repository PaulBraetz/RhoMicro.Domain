namespace RhoMicro.Domain.Abstractions
{
    /// <summary>
    /// Interface for types whose instances have an intrinsic identity.
    /// </summary>
    public interface IHasIntrinsicIdentity
    {
        /// <summary>
        /// Gets the intrinsic identity.
        /// </summary>
		Guid Id { get; }
    }
}