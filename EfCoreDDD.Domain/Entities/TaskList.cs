using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreDDD.Domain.Entities
{
    public class TaskList: BaseEntity
    {
        public string Title { get; private set; }

        public int WorkspaceId { get; private set; }
        public Workspace Workspace { get; private set; }

        private readonly List<TaskItem> _tasks = new();
        public IReadOnlyCollection<TaskItem> Tasks => _tasks.AsReadOnly();

        private TaskList() { }

        public TaskList(string title, int workspaceId)
        {
            Title = title;
            WorkspaceId = workspaceId;
        }

        public TaskItem AddTask(string title, DateTime? dueDate = null)
        {
            var task = new TaskItem(title, dueDate);
            _tasks.Add(task);
            return task;
        }

        public void RemoveTask(int taskId)
        {
            var task = _tasks.FirstOrDefault(x => x.Id == taskId);
            if (task == null) return;

            _tasks.Remove(task);
        }
    }
}
