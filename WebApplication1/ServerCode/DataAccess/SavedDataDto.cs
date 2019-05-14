
using System;
namespace WebApplication1.ServerCode.DataAccess {
    public class SavedDataDto <T> {
        
        public Type Type {get; set;}

        public  string SerializedObject {get; set;}

        public Guid uid {get; set;}

        public T data {get; set;}
    }
}