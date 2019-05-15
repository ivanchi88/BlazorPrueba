using System;
namespace WebApplication1.ServerCode.DataAccess {
    public class SavableData {
        
        public DateTime LastUpdate {get; set;}

        public DateTime CreationDate {get; set;}
        public Guid Uid {get; set;}
    }
}