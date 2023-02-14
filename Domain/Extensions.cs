using Fort;

using RhoMicro.Domain.Abstractions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace RhoMicro.Domain
{
    /// <summary>
    /// Extensions for the <c>RhoMicro.Domain</c> namespace.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Releases the state of an entity into a new data transfer object.
        /// </summary>
        /// <typeparam name="TDto">The type of data transfer object to release state into.</typeparam>
        /// <param name="entity">The enitty whose state to release.</param>
        /// <param name="domain">The domain used to release state.</param>
        /// <returns>A new instance of <typeparamref name="TDto"/> containing the state released from <paramref name="entity"/>.</returns>
        public static TDto Release<TDto>(this IEntity entity, IDomain domain)
            where TDto : IDto
        {
            entity.ThrowIfNull(nameof(entity));
            domain.ThrowIfNull(nameof(domain));

            var result = domain.Release<TDto>(entity);

            return result;
        }
        /// <summary>
        /// Captures the state of an entity represented by a data transfer object.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity whose state to capture.</typeparam>
        /// <param name="dto">The data transfer object to capture state from.</param>
        /// <param name="domain">The domain used to capture state.</param>
        /// <returns>The entity represented by the data transfer object.</returns>
        public static TEntity Capture<TEntity>(this IDto dto, IDomain domain)
            where TEntity : IEntity
        {
            dto.ThrowIfNull(nameof(dto));
            domain.ThrowIfNull(nameof(domain));

            var result = domain.Capture<TEntity>(dto);

            return result;
        }


        /// <summary>
        /// Captures the state of an entity represented by a data transfer object.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity whose state to capture.</typeparam>
        /// <param name="dto">The data transfer object to capture state from.</param>
        /// <param name="domain">The domain used to capture state.</param>
        /// <returns>The entity represented by the data transfer object.</returns>
        public static TEntity Capture<TEntity>(this IDomain domain, IDto dto)
                    where TEntity : IEntity
        {
            dto.ThrowIfNull(nameof(dto));
            domain.ThrowIfNull(nameof(domain));

            var captured = domain.Capture(dto);

            if(captured is not TEntity result)
            {
                throw new DomainCaptureException(dto, domain);
            }

            return result;
        }
        /// <summary>
        /// Releases the state of an entity into a new data transfer object.
        /// </summary>
        /// <typeparam name="TDto">The type of data transfer object to release state into.</typeparam>
        /// <param name="entity">The enitty whose state to release.</param>
        /// <param name="domain">The domain used to release state.</param>
        /// <returns>A new instance of <typeparamref name="TDto"/> containing the state released from <paramref name="entity"/>.</returns>
        public static TDto Release<TDto>(this IDomain domain, IEntity entity)
            where TDto : IDto
        {
            entity.ThrowIfNull(nameof(entity));
            domain.ThrowIfNull(nameof(domain));

            var released = domain.Release(entity);

            if(released is not TDto result)
            {
                throw new DomainReleaseException(entity, domain);
            }

            return result;
        }
    }
}
