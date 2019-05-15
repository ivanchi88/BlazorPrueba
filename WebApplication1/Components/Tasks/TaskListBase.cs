using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using WebApplication1.Data.Tasks; 
using WebApplication1.ServerCode.DataAccess;
using System.Linq;


namespace WebApplication1.Components.Tasks {

    public class TaskListBase : ComponentBase {
        protected List<TaskDto>  tasks {get; set;}
        protected string currentTask {get; set;} 

        [Inject]
        protected IGenericDataAccessor dataAccessor {get; set;}

        protected override void OnInit()
        {
            dataAccessor.connect();
        }

        protected override async Task OnInitAsync()
        {
            tasks = dataAccessor.findAll<TaskDto>().ToList();
        }

        protected void AddTask () {
            if (string.IsNullOrWhiteSpace(currentTask)) return;

            if (tasks == null) 
            {
                tasks = new List<TaskDto>();
            }
            var task = new TaskDto {
                id = Guid.NewGuid(),
                status = TaskStatusEnum.Active,
                creationDate = DateTime.UtcNow,
                text = currentTask
            };
            tasks.Add(task);
            dataAccessor.insert(new SavedDataDto<TaskDto> {
                Data = task,
                Uid = task.id
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
    }
}