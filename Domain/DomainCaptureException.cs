using Fort;

using RhoMicro.Domain.Abstractions;

namespace RhoMicro.Domain
{
    /// <summary>
    /// Indicates a domains inability to capture the state contained within a data transfer object.
    /// </summary>
    public class DomainCaptureException : Exception
    {
        /// <summary>
        /// Indicates a domains inability to capture the state contained within a data transfer object.
        /// </summary>
        /// <param name="dto">
        /// The data transfer object whose state could not be captured by <paramref name="domain"/>.
        /// </param>
        /// <param name="domain">
        /// The domain unable to capture the state contained in <paramref name="dto"/>.
        /// </param>
        public DomainCaptureException(IDto dto, IDomain domain)
        {
            dto.ThrowIfNull(nameof(dto));
            domain.ThrowIfNull(nameof(domain));

            Dto = dto;
            Domain = domain;
        }
        /// <summary>
        /// The data transfer object whose state could not be captured by <see cref="Domain"/>.
        /// </summary>
        public IDto Dto { get; }
        /// <summary>
        /// The domain unable to capture the state contained in <see cref="Dto"/>.
        /// </summary>
        public IDomain Domain { get; }
    }
}
