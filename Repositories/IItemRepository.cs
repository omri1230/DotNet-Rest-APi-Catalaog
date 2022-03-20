
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catlog.Entities;

namespace Catalog.Repositories
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetItemsAsync();
        Task<Item> GetItemAsync(Guid id);
        Task CreateItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(Item item);
    }
}