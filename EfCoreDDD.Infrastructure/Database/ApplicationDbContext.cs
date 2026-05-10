using EfCoreDDD.Application.Interface;
using EfCoreDDD.Domain.Entities;
using EfCoreDDD.Infrastructure.Configuration;

namespace EfCoreDDD.Infrastructure.Database;

public partial class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options) , IUnitOfWork
{
    public DbSet<TaskList> TaskLists => Set<TaskList>();
    public DbSet<TaskItem> TaskItems => Set<TaskItem>();
    public DbSet<Workspace> Workspaces => Set<Workspace>();

    public IRepository<TAggregate, TKey> GetRepository<TAggregate, TKey>() where TAggregate : class => (IRepository<TAggregate, TKey>) this;
 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TaskListConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
