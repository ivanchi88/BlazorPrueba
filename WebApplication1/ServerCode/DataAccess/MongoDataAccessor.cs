using System.Collections.Generic;
using System;
using MongoDB.Driver;
using WebApplication1.Data.Tasks;

namespace WebApplication1.ServerCode.DataAccess {
    public class MongoDataAccessor {
        
        private MongoClient client;
        private MongoDatabase db;
        private IDictionary<string, MongoCollection> collections;

        private string allItemsCollection = "allItems";

        public void connect() {
            client = new MongoClient("localhost:mongodb://localhost:27017");
            db = client.GetDatabase("default");
            collections = new Dictionary<string, MongoCollection>();
        }

        public void insert<T>(T data){
 
            if (!BsonClassMap.IsClassMapRegistered(typeof(MyClass))) {
                BsonClassMap.RegisterClassMap<T>();
            } 

            MopngoCollection cachedCollection;
            if (!db.TryGetValue(allItemsDb, cachedCollection)) {
                cachedCollection = db.GetCollection<BsonDocument>(allItemsCollection);
                db.Add(allItemsCollection, cachedCollection);
            }

            MemoryStream ms = new MemoryStream();
            using (BsonWriter writer = new BsonWriter(ms))
            {
                JsonSerializer serializer = new JsonSerializer();
  
                serializer.Serialize(writer, e);
             
                cachedCollection.insertOne()
            }
        }

        public T find <T> (Guid id){

            return default(T);
        }

        public IEnumerable<Guid> findAll(Type type){

            return null;
        }
    }
}