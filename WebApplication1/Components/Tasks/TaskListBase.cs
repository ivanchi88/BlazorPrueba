using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;


namespace WebApplication1.Components.Tasks {

    public class TaskListBase : ComponentBase {
        protected List<string>  tasks {get; set;}
        protected string currentTask {get; set;} 

        protected void addTask () {
            if (tasks == null) 
            {
                tasks = new List<string>();
            }
            tasks.Add(currentTask);
            currentTask = "";
        }

        protected void maybeEnter(UIKeyboardEventArgs e) {
            if (!string.IsNullOrEmpty(currentTask) && e.Code == "Enter") {
                this.addTask(); 
            } else {
                Console.WriteLine(currentTask);
            }
        } 
    }
}