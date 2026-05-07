using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreDDD.Domain.Entities
{
    public class TaskItem : BaseEntity
    {
        public int TaskListId { get; private set; }
        public TaskList TaskList { get; private set; }

        public string Title { get; private set; } = null!;
        public bool IsCompleted { get; private set; } = false;
        public DateTime? DueDate { get; private set; }

        private TaskItem() { }

        internal TaskItem(int taskListId, string title, DateTime? dueDate)
        {
            TaskListId = taskListId;
            Title = title;
            DueDate = dueDate;
        }

        public TaskItem(string title, DateTime? dueDate)
        {
            Title = title;
            DueDate = dueDate;
        }

        public void MarkComplete()
        {
            IsCompleted = true;
            SetUpdated();
        }

        public void Rename(string newTitle)
        {
            Title = newTitle;
            SetUpdated();
        }
    }
}
