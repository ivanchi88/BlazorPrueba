using System.Linq;
using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;
using WebApplication1.Data.Tasks; 
using Newtonsoft.Json;

namespace WebApplication1.ServerCode.DataAccess {
 
    public class MySqlDataAccessor : IGenericDataAccessor {

        private MySqlConnection databaseConnection;
        private string connectionString = "datasource=127.0.0.1;port=3306;username=test2;password=test;database=BlazorDB;";

        private string defaultTable = "blazordb.defaulttable";

        public void connect() { 
            
        }   

        public void insert <T>(SavedDataDto<T> data){

            databaseConnection = new MySqlConnection(connectionString);

            data.Type = typeof(T);

            var serialized = JsonConvert.SerializeObject(data);
            var globalType = JsonConvert.SerializeObject(typeof(T)); 
            var serializedUid = JsonConvert.SerializeObject(data.Uid);

            var query = $"INSERT INTO {defaultTable} (Uid, Type, Data) VALUES ( '{serializedUid}', '{globalType}', '{serialized}');"; 

            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            
            try
            {
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();
            }
            catch (Exception ex)
            {
                // Mostrar cualquier error
                Console.WriteLine(ex.Message);
            } finally {
                databaseConnection.Close();
            }
        }

        public SavedDataDto<T> actualize <T> () {
            return null;
        }

        public T find <T> (Guid id){
            var serializedUid = JsonConvert.SerializeObject(id);

            var query = $"SELECT * FROM {defaultTable} where Uid = '{serializedUid}'";
            
            return executeQuery<T>(query).FirstOrDefault();
        }

        public IEnumerable<T> findAll<T>(){
            var serializedType = JsonConvert.SerializeObject(typeof(T));

            var query = $"SELECT * FROM {defaultTable} where Type = '{serializedType}'";

            Console.WriteLine(query);
            return executeQuery<T>(query);
       }

        private IEnumerable<T> executeQuery <T>(string query) {
            databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            List<T> result = new List<T>();
            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2)};
                        result.Add(parseRow<T>(row)); 
                    }
                }
                else
                {
                    Console.WriteLine("No se encontraron datos.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            } finally {
                databaseConnection.Close();
            } 
            return result;
       }

        private T parseRow<T> (string [] row) {
            Type type = JsonConvert.DeserializeObject<Type>(row[1]);

            var data = JsonConvert.DeserializeObject<SavedDataDto<T>>(row[2]);

            if (typeof(T) !=  type) {
                throw new Exception($"Invalid type {typeof(T)} is not equal to {data.Type}"  );
            }
            
            return  data.Data;
        }
    }

}