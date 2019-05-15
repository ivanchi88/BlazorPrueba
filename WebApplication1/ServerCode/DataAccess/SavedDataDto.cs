
using System;
namespace WebApplication1.ServerCode.DataAccess {
    public class SavedDataDto <T> {
        
        public Type Type {get; set;}

        public  string SerializedObject {get; set;}

        public Guid Uid {get; set;}

        public T Data {get; set;}
    }
}