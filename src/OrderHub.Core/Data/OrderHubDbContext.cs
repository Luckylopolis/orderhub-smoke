using Microsoft.EntityFrameworkCore;

namespace OrderHub.Core.Data;

public class OrderHubDbContext(DbContextOptions<OrderHubDbContext> options) : DbContext(options);
