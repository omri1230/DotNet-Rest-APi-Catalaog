using System;
using System.Collections.Generic;
using Catlog.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class MongoDbItemsRepository : IItemRepository
    {
        private const string databaseName = "catalog";
        private const string collectionName = "items";

        private readonly IMongoCollection<Item> itemsCollection;
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;
        public MongoDbItemsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            itemsCollection = database.GetCollection<Item>(collectionName);
        }
        public IEnumerable<Item> GetItems => itemsCollection.Find(new BsonDocument()).ToList();

        public void CreateItem(Item item)
        {
            itemsCollection.InsertOne(item);
        }

        public void DeleteItem(Item item)
        {
            var filter = filterBuilder.Eq(existsingItem => existsingItem.Id, item.Id);
            itemsCollection.DeleteOne(filter);
        }

        public Item GetItem(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            return itemsCollection.Find(filter).SingleOrDefault();
        }

        public void UpdateItem(Item item)
        {
            var filter = filterBuilder.Eq(existsingItem => existsingItem.Id, item.Id);
            itemsCollection.ReplaceOne(filter, item);
        }
    }
}