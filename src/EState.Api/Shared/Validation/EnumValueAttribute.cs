using System.ComponentModel.DataAnnotations;
using EState.Api.Shared.Serialization;

namespace EState.Api.Shared.Validation;

[AttributeUsage(AttributeTargets.Property)]
internal sealed class EnumValueAttribute<TEnum> : ValidationAttribute
	where TEnum : struct, Enum
{
	public override bool IsValid(object? value)
	{
		if (value is null)
			return true;

		return value is string name && SnakeCaseEnum<TEnum>.IsDefined(name);
	}
}
