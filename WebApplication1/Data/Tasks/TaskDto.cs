
using System;
using WebApplication1.ServerCode.DataAccess;

namespace WebApplication1.Data.Tasks {
    public class TaskDto : SavableData {
        public string Text {get; set;}
        public TaskStatusEnum Status {get; set;}

        public TaskDto(){
            
        }
    }
}