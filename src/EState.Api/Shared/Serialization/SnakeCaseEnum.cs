using System.Text.Json;

namespace EState.Api.Shared.Serialization;

internal static class SnakeCaseEnum<TEnum> where TEnum : struct, Enum
{
	private static readonly Dictionary<string, TEnum> ValuesByName = Enum.GetNames<TEnum>()
		.ToDictionary(
			JsonNamingPolicy.SnakeCaseLower.ConvertName,
			name => Enum.Parse<TEnum>(name),
			StringComparer.Ordinal);

	public static bool IsDefined(string name) => ValuesByName.ContainsKey(name);

	public static TEnum Parse(string name)
	{
		if (ValuesByName.TryGetValue(name, out var value))
			return value;

		throw new ArgumentException($"Expected a snake-case {typeof(TEnum).Name} name.", nameof(name));
	}

	public static TEnum? ParseNullable(string? name) => name is null ? null : Parse(name);
}
