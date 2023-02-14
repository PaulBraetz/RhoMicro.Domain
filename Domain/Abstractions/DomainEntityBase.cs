using Fort;

using RhoMicro.Common.System;
using RhoMicro.Domain.ObjectSync.Synchronization;
using RhoMicro.ObjectSync.Attributes;

using System.ComponentModel;

namespace RhoMicro.Domain.Abstractions
{
    /// <summary>
    /// Base class for domain entities. Instances sharing an intrinsic identity should communicate shared data via the <see cref="SynchronizedAttribute"/>.
    /// </summary>
    [SynchronizationTarget(
        ContextTypeIsSealed = false,
        ContextTypeAccessibility = Accessibility.Protected,
        ContextTypeConstructorAccessibility = Accessibility.Public,
        ContextPropertyAccessibility = Accessibility.Protected,
        ContextPropertyModifier = Modifier.Virtual)]
    public abstract partial class EntityBase : ObservableDisposableBase, IEntity
    {
        /// <summary>
        /// Initializes a new instance based on a data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer object from which to initialize.</param>
        /// <param name="synchronizationAuthority">The synchronization authority used to synchronize entity instances sharing the same intrinsic identity.</param>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected EntityBase(IDto dto, ISynchronizationAuthority synchronizationAuthority)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            dto.ThrowIfDefault(nameof(dto));
            synchronizationAuthority.ThrowIfDefault(nameof(synchronizationAuthority));

            Id = dto.Id;
            SynchronizationAuthority = synchronizationAuthority;
            SourceInstanceId = Id.ToString();
            SynchronizationContext.Synchronize();

            CreationTimeInternal = dto.CreationTime;
            LastChangedAtInternal = dto.LastChangedAt;
        }

        /// <inheritdoc/>
        public DateTimeOffset CreationTime => CreationTimeInternal;
        /// <inheritdoc/>
        public DateTimeOffset LastChangedAt => LastChangedAtInternal;
        /// <inheritdoc/>
		public Guid Id { get; }

        [Synchronized(Observable = true, PropertyAccessibility = Accessibility.Private)]
        private DateTimeOffset _lastChangedAtInternal;
        [Synchronized(Observable = true, PropertyAccessibility = Accessibility.Private)]
        private DateTimeOffset _creationTimeInternal;

        [SourceInstanceId]
        private String SourceInstanceId { get; }
        [SynchronizationAuthority]
        private ISynchronizationAuthority SynchronizationAuthority { get; }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <inheritdoc/>
        protected override void DisposeUnmanaged(Boolean disposing)
        {
            SynchronizationContext.Desynchronize();
            base.DisposeUnmanaged(disposing);
        }
        /// <summary>
        /// Disposes unmanaged resources.
        /// </summary>
        ~EntityBase()
        {
            FinalizeDispose();
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected void RaisePropertyChanged(String propertyName)
        {
            propertyName.ThrowIfDefaultOrEmpty(nameof(propertyName));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if(propertyName != nameof(LastChangedAtInternal))
            {
                LastChangedAtInternal = DateTimeOffset.Now;
            }
        }

        partial void OnPropertyChanged(String propertyName) => RaisePropertyChanged(propertyName);
    }
}