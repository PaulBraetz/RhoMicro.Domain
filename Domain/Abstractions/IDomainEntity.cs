using RhoMicro.Common.System.Abstractions;

using System.ComponentModel;

namespace RhoMicro.Domain.Abstractions
{
    /// <summary>
    /// The base domain entity interface, representing domain entities possessing an intrinsic identity.
    /// </summary>
    public interface IEntity : INotifyPropertyChanged, IObservableDisposable, IHasIntrinsicIdentity, IMutable
    {
    }
}