using System;
using System.Collections.Generic;
using Catlog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Catalog.Dtos;
using System.Threading.Tasks;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository repository;

        public ItemsController(IItemRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items = (await repository.GetItemsAsync()).Select(item => item.AsDto());
            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var item = await repository.GetItemAsync(id);
            return item is null ? NotFound() : Ok(item.AsDto());
        }
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemDtoAsync(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
            await repository.CreateItemAsync(item);
            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemDtoAsync(Guid id, UpdateItemDto itemDto)
        {
            var existsItem = await repository.GetItemAsync(id);
            if (existsItem is null)
            {
                return NotFound();
            }
            Item updateItem = existsItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };
            await repository.UpdateItemAsync(updateItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var deltedItem = await repository.GetItemAsync(id);
            if (deltedItem is null)
            {
                return NotFound();
            }
            await repository.DeleteItemAsync(deltedItem);
            return NoContent();
        }
    }
}