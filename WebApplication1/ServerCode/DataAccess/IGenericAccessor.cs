using System.Collections.Generic; 
using System;
namespace WebApplication1.ServerCode.DataAccess {
    public interface IGenericDataAccessor {
        
        void connect();

        void insert(SavableData received);

        void update (SavableData received);

        T find <T> (Guid id);

        IEnumerable<T> findAll<T>();
    }
}