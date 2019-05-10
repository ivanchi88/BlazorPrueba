using System.Collections.Generic;
using Microsoft.AspNetCore.Components;


namespace WebApplication1.Components.TaskList {

    public class TaskList : ComponentBase{
        List<string>  tasks;

        string currentTask {get; set;} 

        public void addTask () {
            if (tasks == null) 
            {
                tasks = new List<string>();
            }
            tasks.Add(currentTask);
            currentTask = "";
        }
    }
}