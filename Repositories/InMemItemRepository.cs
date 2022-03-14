using System;
using System.Collections.Generic;
using Catlog.Entities;


namespace Catalog.Repositories
{
    public class InMemItemRepository : IItemRepository
    {
        private readonly List<Item> items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 18, CreatedDate = DateTimeOffset.UtcNow },
        };

        public IEnumerable<Item> GetItems => items;

        public Item GetItem(Guid id)
        {
            return items.Find(item => item.Id == id);
        }
        public void CreateItem(Item item)
        {
            items.Add(item);
        }

        public void UpdateItem(Item item)
        {
            var index = items.FindIndex(exisitngItem => exisitngItem.Id == item.Id);
            items[index] = item;
        }
        public void DeleteItem(Item item)
        {
            var index = items.FindIndex(exisitngItem => exisitngItem.Id == item.Id);
            items.RemoveAt(index);
        }
    }
}
