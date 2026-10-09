using Microsoft.Data.Sqlite;

namespace OrderHub.Core.Data;

/// <summary>
/// Resuelve rutas relativas de SQLite contra la raíz del repo (donde está OrderHub.sln),
/// para que la API y el Worker compartan el mismo archivo sin importar desde dónde se ejecuten.
/// </summary>
public static class SqlitePath
{
    public static string Normalize(string connectionString)
    {
        var builder = new SqliteConnectionStringBuilder(connectionString);
        var dataSource = builder.DataSource;

        if (string.IsNullOrWhiteSpace(dataSource) || dataSource == ":memory:" || Path.IsPathRooted(dataSource))
            return builder.ToString();

        var root = FindRepoRoot() ?? Directory.GetCurrentDirectory();
        var fullPath = Path.Combine(root, dataSource);
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
        builder.DataSource = fullPath;
        return builder.ToString();
    }

    private static string? FindRepoRoot()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir is not null)
        {
            if (File.Exists(Path.Combine(dir.FullName, "OrderHub.sln")))
                return dir.FullName;
            dir = dir.Parent;
        }
        return null;
    }
}
