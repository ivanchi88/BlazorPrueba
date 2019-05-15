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
            var globalType = JsonConvert.SerializeObject(data.GetType()); 
            var serializedUid = JsonConvert.SerializeObject(data.Uid);

            var query = $"INSERT INTO {defaultTable} (Uid, Type, Data) VALUES ( '{serializedUid}', '{globalType}', '{serialized}');"; 

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
            var serializedUid = JsonConvert.SerializeObject(id);

            var query = $"SELECT * FROM {defaultTable} where Uid = '{serializedUid}'";
            
            databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            // A consultar !
            try
            {
                // Abre la base de datos
                databaseConnection.Open();

                // Ejecuta la consultas
                reader = commandDatabase.ExecuteReader();

                // Hasta el momento todo bien, es decir datos obtenidos

                // IMPORTANTE :#
                // Si tu consulta retorna un resultado, usa el siguiente proceso para obtener datos
                
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // En nuestra base de datos, el array contiene:  ID 0, Type 1 , Data 3
                        // Hacer algo con cada fila obtenida
                        string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2)};

                        var uid = JsonConvert.DeserializeObject<Guid>(row[0]);
                        Type type = JsonConvert.DeserializeObject<Type>(row[1]);

                        var data = JsonConvert.DeserializeObject<SavedDataDto<T>>(row[2]);

                        if (typeof(T) !=  data.Type) {
                            throw new Exception($"Invalid type {typeof(T)} is not equal to {data.Type}"  );
                        }
                        
                        Console.WriteLine(data);
                        Console.WriteLine(data.Data);

                        var result = data.Data;

                        return result;
                    }
                }
                else
                {
                    Console.WriteLine("No se encontraron datos.");
                }

                // Cerrar la conexión
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                // Mostrar cualquier excepción
                Console.WriteLine(ex.Message);
            }

            return default(T);
        }

        public IEnumerable<Guid> findAll(Type type){
            return null;
        }
    }

}