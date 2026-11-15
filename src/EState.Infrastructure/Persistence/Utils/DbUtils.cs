namespace EState.Infrastructure.Persistence.Utils;

internal static class DbUtils
{
    public static string AllowedValues(string column, IReadOnlyList<string> values)
    {
        var literals = values.Select(value => "'" + value.Replace("'", "''") + "'");
        return $"{column} IN ({string.Join(", ", literals)})";
    }
}
