using System.Collections.Generic;
using System;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using WebApplication1.Data.Tasks;
using System.Runtime.Serialization.Formatters;

namespace WebApplication1.ServerCode.DataAccess {
    public class MongoDataAccessor: IDataAccessor {
        
        private MongoClient client;
        private IMongoDatabase db;
        private IDictionary<string, IMongoCollection<BsonDocument>>collections;

        private string allItemsCollection = "allItems";

        public void connect() {
            client = new MongoClient("mongodb://localhost:27017");
            db = client.GetDatabase("default");
            collections = new Dictionary<string, IMongoCollection<BsonDocument>>();
        }

        public void insert<T>(T data){
            Type dataType = data.GetType(); 
            
            Console.WriteLine(dataType.ToString());

             if (!BsonClassMap.IsClassMapRegistered(dataType)) {
                BsonClassMap.RegisterClassMap<T>();
            } 

            var typeName = typeof(T).ToString();

            IMongoCollection<BsonDocument> cachedCollection;

            if (!collections.TryGetValue(typeName, out cachedCollection) || cachedCollection == null) {
                cachedCollection = db.GetCollection<BsonDocument>(typeName);
                collections.TryAdd(typeName, cachedCollection);
            }

            var serialized = data.ToBsonDocument();
            cachedCollection.InsertOne(serialized);

        }

        public T find <T> (Guid id){

            return default(T);
        }

        public IEnumerable<Guid> findAll(Type type){

            return null;
        }
    }
}