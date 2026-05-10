using EfCoreDDD.Application.Interface;
using EfCoreDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreDDD.Infrastructure.Database;

public partial class ApplicationDbContext : IRepository<TaskList, int>
{
    async Task<bool> IRepository<TaskList, int>.AnyAsync(int key) => await TaskLists.AnyAsync(t => t.Id == key);

    void IRepository<TaskList, int>.Add(TaskList aggregate) => TaskLists.Add(aggregate);
    void IRepository<TaskList, int>.Delete(TaskList aggregate) => TaskLists.Remove(aggregate);
    
    async Task<TaskList?> IRepository<TaskList, int>.TryFindAsync(int key) => await TaskLists.FirstOrDefaultAsync(t => t.Id == key);
}
