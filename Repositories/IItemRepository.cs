
using System;
using System.Collections.Generic;
using Catlog.Entities;

namespace Catalog.Repositories
{
    public interface IItemRepository
    {
        IEnumerable<Item> GetItems { get; }
        Item GetItem(Guid id);
        void CreateItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(Item item);
    }
}