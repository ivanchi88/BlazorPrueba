
using System;
namespace WebApplication1.Data.Tasks {
    public class TaskDto {
        public Guid id {get; set;}
        public string text {get; set;}
        public DateTime creationDate {get; set;}
        public TaskStatusEnum status {get; set;}

        public TaskDto(){
            
        }
    }
}