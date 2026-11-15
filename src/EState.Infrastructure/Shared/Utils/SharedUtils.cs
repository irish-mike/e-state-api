using System.Text.Json;

namespace EState.Infrastructure.Shared.Utils;

internal static class SharedUtils
{
    public static string[] SnakeCaseEnumNames<TEnum>() where TEnum : struct, Enum =>
        Enum.GetNames<TEnum>()
            .Select(JsonNamingPolicy.SnakeCaseLower.ConvertName)
            .ToArray();
}
