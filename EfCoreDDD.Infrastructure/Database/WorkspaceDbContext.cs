using EfCoreDDD.Application.Interface;
using EfCoreDDD.Domain.Entities;
namespace EfCoreDDD.Infrastructure.Database;

public partial class ApplicationDbContext : IRepository<Workspace, int>
{
    void IRepository<Workspace, int>.Add(Workspace aggregate) => Workspaces.Add(aggregate);
    void IRepository<Workspace, int>.Delete(Workspace aggregate) => Workspaces.Remove(aggregate);
    async Task<Workspace?> IRepository<Workspace, int>.TryFindAsync(int key) => await Workspaces.FirstOrDefaultAsync(w => w.Id == key);
    async Task<bool> IRepository<Workspace, int>.AnyAsync(int key) => await Workspaces.AnyAsync(w => w.Id == key);

}
