using EfCoreDDD.Application.Interface;
using EfCoreDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreDDD.Infrastructure.Database;

public partial class ApplicationDbContext : IRepository<TaskItem, int>
{
    void IRepository<TaskItem, int>.Add(TaskItem aggregate) => TaskItems.Add(aggregate);

    async Task<bool> IRepository<TaskItem, int>.AnyAsync(int key) => await TaskItems.AnyAsync(t => t.Id == key);

    void IRepository<TaskItem, int>.Delete(TaskItem aggregate) => TaskItems.Remove(aggregate);

    async Task<TaskItem?> IRepository<TaskItem, int>.TryFindAsync(int key) => await TaskItems.FirstOrDefaultAsync(t => t.Id == key);

}
