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

        public void insert (SavableData received){

            var data = new SavedDataDto<SavableData> {
                Data = received,
                Uid = received.Uid,
                Updated = DateTime.UtcNow,
                Type = received.GetType()
            };

            var serialized = JsonConvert.SerializeObject(data);
            var globalType = JsonConvert.SerializeObject(received.GetType()); 
            var serializedUid = JsonConvert.SerializeObject(data.Uid);

            Console.WriteLine(globalType);
            var query = $"INSERT INTO {defaultTable} (Uid, Type, Data) VALUES ( '{serializedUid}', '{globalType}', '{serialized}');"; 

            executeVoidQuery(query);
           
        }

        public void update (SavableData received) {

            var data = new SavedDataDto<SavableData> {
                Data = received,
                Uid = received.Uid,
                Updated = DateTime.UtcNow,
            };

            var serialized = JsonConvert.SerializeObject(data); 
            var serializedUid = JsonConvert.SerializeObject(data.Uid);
 
            var query = $"UPDATE {defaultTable} SET Data = '{serialized}' WHERE Uid = '{serializedUid}';"; 

            executeVoidQuery(query);
        }

        public T find <T> (Guid id){
            var serializedUid = JsonConvert.SerializeObject(id);

            var query = $"SELECT * FROM {defaultTable} where Uid = '{serializedUid}'";
            
            return executeSearchQuery<T>(query).FirstOrDefault();
        }

        public IEnumerable<T> findAll<T>(){
            var serializedType = JsonConvert.SerializeObject(typeof(T));

            var query = $"SELECT * FROM {defaultTable} where Type = '{serializedType}'";

            return executeSearchQuery<T>(query);
       }

        private IEnumerable<T> executeSearchQuery <T>(string query) {
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
                    Console.WriteLine("No data was found");
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

        private void executeVoidQuery(string query){ 
            databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            
            try
            {
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            } finally {
                databaseConnection.Close();
            }
        }
    }

}