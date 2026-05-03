
using Microsoft.EntityFrameworkCore;

namespace HRMS.Infrastructure.Models;

public partial class HRMSDbContext : DbContext
{
    public HRMSDbContext()
    {
    }

    public HRMSDbContext(DbContextOptions<HRMSDbContext> options)
        : base(options)
    {
    }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
