using Fort;

using RhoMicro.Common.System.Abstractions;

using System.Security.Claims;

namespace RhoMicro.Domain.Abstractions
{
    /// <summary>
    /// Base class fopr types implementing <see cref="IDto"/>.
    /// </summary>
    public abstract class DtoBase : IDto
    {
        /// <summary>
        /// Initializes a new instance based on an entities state.
        /// </summary>
        /// <param name="entity">The entity whose state to take on.</param>
        protected DtoBase(IEntity entity)
        {
            entity.ThrowIfNull(nameof(entity));

            CreationTime = entity.CreationTime;
            LastChangedAt = entity.LastChangedAt;
            Id = entity.Id;
        }

        /// <summary>
        /// Initializes a new empty instance.
        /// </summary>
        protected DtoBase() 
        {
            LastChangedAt = DateTimeOffset.UtcNow;
            CreationTime= DateTimeOffset.UtcNow;
            Id = Guid.NewGuid();
        }

        /// <inheritdoc/>
        public DateTimeOffset LastChangedAt { get; private set; }
        /// <inheritdoc/>
        public DateTimeOffset CreationTime { get; private set; }
        /// <inheritdoc/>
        public Guid Id { get; private set; }
    }
}