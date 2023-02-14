using Fort;

using RhoMicro.Domain.Abstractions;

namespace RhoMicro.Domain
{
    /// <summary>
    /// Indicates a domains inability to release state into a data transfer object.
    /// </summary>
    public class DomainReleaseException : Exception
    {
        /// <summary>
        /// Indicates a domains inability to release state into a data transfer object.
        /// </summary>
        /// <param name="entity">
        /// The entity whose state could not be released from <paramref name="domain"/>.
        /// </param>
        /// <param name="domain">
        /// The domain unable to release the state contained in <paramref name="entity"/>.
        /// </param>
        public DomainReleaseException(IEntity entity, IDomain domain)
        {
            entity.ThrowIfNull(nameof(entity));
            domain.ThrowIfNull(nameof(domain));

            Entity = entity;
            Domain = domain;
        }
        /// <summary>
        /// The entity whose state could not be released from <see cref="Domain"/>.
        /// </summary>
        public IEntity Entity { get; }
        /// <summary>
        /// The domain unable to release the state contained in <see cref="Entity"/>.
        /// </summary>
        public IDomain Domain { get; }
    }
}
