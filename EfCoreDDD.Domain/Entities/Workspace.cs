using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace EfCoreDDD.Domain.Entities
{
    public class Workspace : BaseEntity
    {
        public string Name { get; private set; }

        private readonly List<TaskList> _lists = new();
        public IReadOnlyCollection<TaskList> Lists => _lists.AsReadOnly();

        private Workspace() { }

        public Workspace(string name)
        {
            Name = name;
        }
        public void UpdateWorkspace(string name) { 
            Name  = name;
            SetUpdated();
        }
        public void AddtaskList(TaskList list) {
            _lists.Add(list);
        }
    }
}
