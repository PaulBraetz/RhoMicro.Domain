using RhoMicro.Common.System.Abstractions;

namespace RhoMicro.Domain.Abstractions
{
    /// <summary>
    /// The base data transfer object interface, intended for mutable data transfer objects containing initialization data for a specific entity.
    /// </summary>
    public interface IDto : IHasIntrinsicIdentity, IMutable
    {
    }
}