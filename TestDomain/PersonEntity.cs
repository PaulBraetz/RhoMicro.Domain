using RhoMicro.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fort;
using RhoMicro.ObjectSync.Attributes;

namespace TestDomain
{
	[SynchronizationTarget(
		BaseContextTypeName = nameof(EntityBase<PersonDto>.EntityBaseSynchronizationContext),
		ContextTypeIsSealed = true,
		ContextTypeAccessibility = Accessibility.Protected,
		ContextPropertyAccessibility = Accessibility.Protected,
		ContextPropertyModifier = Modifier.Override)]
	internal sealed partial class PersonEntity : EntityBase<PersonDto>
	{
		public PersonEntity(PersonDto dto) : base(dto)
		{
			Name = dto.Name;
		}
		public PersonEntity(String name) : base()
		{
			name.ThrowIfDefault(nameof(name));

			Name = name;
		}

		[Synchronized(Observable = true)]
		private String _name;

		partial void OnPropertyChanged(String propertyName)
		{
			base.OnPropertyChanged(propertyName);
		}

		public override PersonDto ToDto()
		{
			return new PersonDto()
			{
				FirstUpdate = FirstUpdate,
				LastUpdate = LastUpdate,
				Id = Id,
				Name = Name
			};
		}
	}
}
