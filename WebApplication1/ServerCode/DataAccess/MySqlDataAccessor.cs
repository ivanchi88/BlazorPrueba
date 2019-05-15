using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;
using WebApplication1.Data.Tasks; 
using Newtonsoft.Json;

namespace WebApplication1.ServerCode.DataAccess {
 
    public class MySqlDataAccessor : IGenericDataAccessor {

        private MySqlConnection databaseConnection;
        private string connectionString = "datasource=127.0.0.1;port=3306;username=test2;password=test;database=BlazorDB;";

        public void connect() { 
            
        }   

        public void insert <T>(SavedDataDto<T> data){

            databaseConnection = new MySqlConnection(connectionString);

            data.Type = typeof(T);
            var serialized = JsonConvert.SerializeObject(data);
            var globalType = JsonConvert.SerializeObject(data.GetType()); 

            var query = $"INSERT INTO blazordb.DefaultTable (Uid, Type, Data) VALUES ( {JsonConvert.SerializeObject(data.Uid)}, {globalType}, '{serialized}');"; 

            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            
            try
            {
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();
                 
        
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                // Mostrar cualquier error
                Console.WriteLine(ex.Message);
            }
        }

        public SavedDataDto<T> actualize <T> () {
            return null;
        }

        public T find <T> (Guid id){
            return default(T);
        }

        public IEnumerable<Guid> findAll(Type type){
            return null;
        }
    }

}