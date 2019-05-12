using System.Collections.Generic; 
using System;
namespace WebApplication1.ServerCode.DataAccess {
    public interface IDataAccessor {
        
        void connect();

        void insert<T>(T data);

        T find <T> (Guid id);

        IEnumerable<Guid> findAll(Type type);
    }
}