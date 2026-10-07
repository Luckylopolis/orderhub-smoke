using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using OrderHub.Core.Data;

namespace OrderHub.Tests;

public class SmokeTests
{
    [Fact]
    public async Task SqliteInMemory_CanConnect()
    {
        await using var connection = new SqliteConnection("Data Source=:memory:");
        await connection.OpenAsync();

        await using var db = new OrderHubDbContext(
            new DbContextOptionsBuilder<OrderHubDbContext>().UseSqlite(connection).Options);

        Assert.True(await db.Database.CanConnectAsync());
    }
}
