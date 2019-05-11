using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using WebApplication1.Data.Tasks;


namespace WebApplication1.Components.Tasks {

    public class TaskListBase : ComponentBase {
        protected List<TaskDto>  tasks {get; set;}
        protected string currentTask {get; set;} 

        protected void AddTask () {
            if (tasks == null) 
            {
                tasks = new List<TaskDto>();
            }
            tasks.Add(new TaskDto {
                id = Guid.NewGuid(),
                status = TaskStatusEnum.Active,
                creationDate = DateTime.UtcNow,
                text = currentTask
            });
            currentTask = "";
        }

        protected void MaybeEnter(UIKeyboardEventArgs e) {
            if (!string.IsNullOrEmpty(currentTask) && e.Code == "Enter") {
                this.AddTask(); 
            }
        } 

        protected void ChangeTaskStatus(TaskDto task, TaskStatusEnum status) {
            task.status = status;
        }

        protected void RemoveItem(TaskDto task) {
            tasks.Remove(task);
        }
    }
}