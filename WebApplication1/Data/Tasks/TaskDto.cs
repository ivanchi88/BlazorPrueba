
using System;
using WebApplication1.ServerCode.DataAccess;

namespace WebApplication1.Data.Tasks {
    public class TaskDto : SavableData {
        public string text {get; set;}
        public DateTime creationDate {get; set;}
        public TaskStatusEnum status {get; set;}

        public TaskDto(){
            
        }
    }
}