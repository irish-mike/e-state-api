using System.ComponentModel.DataAnnotations;
using EState.Api.Shared.Serialization;

namespace EState.Api.Shared.Validation;

[AttributeUsage(AttributeTargets.Property)]
internal abstract class EnumValueAttribute : ValidationAttribute
{
	public abstract IReadOnlyCollection<string> Names { get; }
}

internal sealed class EnumValueAttribute<TEnum> : EnumValueAttribute
	where TEnum : struct, Enum
{
	public override IReadOnlyCollection<string> Names => SnakeCaseEnum<TEnum>.Names;

	public override bool IsValid(object? value)
	{
		if (value is null)
			return true;

		return value is string name && SnakeCaseEnum<TEnum>.IsDefined(name);
	}
}
