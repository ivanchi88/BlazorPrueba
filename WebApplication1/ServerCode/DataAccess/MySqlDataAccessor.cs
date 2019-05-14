using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;
using WebApplication1.Data.Tasks;
using Newtonsoft.Json;

namespace WebApplication1.ServerCode.DataAccess {
 
    public class MySqlDataAccessor : IGenericDataAccessor {

    private MySqlConnection databaseConnection;
    private string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=root;database=blazorDB;";

        public void connect() { 
            databaseConnection = new MySqlConnection(connectionString);
        }

        public void insert <T>(SavedDataDto<T> data){
            var serialized = JsonConvert.SerializeObject(data);

            var toSerialize = new SavedDataDto<T> {
                Type = typeof(T),
                SerializedObject = serialized
            };

        }

        public T find <T> (Guid id){
            return default(T);
        }

        public IEnumerable<Guid> findAll(Type type){
            return null;
        }
    }

}