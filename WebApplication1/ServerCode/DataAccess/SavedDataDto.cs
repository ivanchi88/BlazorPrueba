
using System;
namespace WebApplication1.ServerCode.DataAccess {
    public class SavedDataDto <T> {
        
        public Type Type {get; set;} 

        public Guid Uid {get; set;}

        public T Data {get; set;}

        public DateTime Created {get; set;}

        public DateTime Updated  {get; set;}
    }
}