/*/using System.Collections.Generic;
using System;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization; 
using Newtonsoft.Json;

namespace WebApplication1.ServerCode.DataAccess {
    public class MongoDataAccessor: IGenericDataAccessor {
        
        private MongoClient client;
        private IMongoDatabase db;
        private IDictionary<string, IMongoCollection<SavedDataDto<T>>>collections;

        private string allItemsCollection = "allItems";

        public void connect() {
            client = new MongoClient("mongodb://localhost:27017");
            db = client.GetDatabase("default");
            collections = new Dictionary<string, IMongoCollection<SavedDataDto>>();
        }

        public void insert<T>(SavedDataDto<T> data){
            Type dataType = data.GetType(); 
            
            Console.WriteLine(dataType.ToString());

            var typeName = typeof(T).ToString();

            IMongoCollection<SavedDataDto<T>> cachedCollection;

            if (!collections.TryGetValue(typeName, out cachedCollection) || cachedCollection == null) {
                cachedCollection = db.GetCollection<SavedDataDto<T>>(typeName);
                collections.TryAdd(typeName, cachedCollection);
            } 

            var serialized = JsonConvert.SerializeObject(data);

            var toSerialize = new SavedDataDto {
                Type = typeof(T),
                SerializedObject = serialized
            };
            
            cachedCollection.InsertOne(toSerialize); 
        }

        public T find <T> (Guid id){

            return default(T);
        }

        public IEnumerable<Guid> findAll(Type type){

            return null;
        }
    }
}

*/