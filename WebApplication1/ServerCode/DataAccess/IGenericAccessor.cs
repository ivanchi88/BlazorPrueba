using System.Collections.Generic; 
using System;
namespace WebApplication1.ServerCode.DataAccess {
    public interface IGenericDataAccessor {
        
        void connect();

        void insert<T>(SavedDataDto<T> data);

        T find <T> (Guid id);

        IEnumerable<T> findAll<T>();
    }
}