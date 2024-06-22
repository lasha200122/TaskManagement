namespace TaskManagement.Infrastructure.Persistence;

public class TaskManagementContext : DbContext
{
    public TaskManagementContext(DbContextOptions<TaskManagementContext> options) : base(options) {}

    public virtual DbSet<User> Users { get; set; } = null!;
    public virtual DbSet<Assignment> Assignments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(TaskManagementContext).Assembly);
        base.OnModelCreating(builder);
    }
}
