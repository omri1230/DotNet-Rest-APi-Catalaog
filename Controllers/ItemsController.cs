using System;
using System.Collections.Generic;
using Catlog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Catalog.Dtos;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("[controller]")]//GET / Items
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository repository;

        public ItemsController(IItemRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]//GET /Items
        public IEnumerable<ItemDto> GetItems() => repository.GetItems.Select(item => item.AsDto());

        [HttpGet("{id}")]//GET /item/{id}
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = repository.GetItem(id);
            return item is null ? NotFound() : Ok(item.AsDto());
        }
        [HttpPost]// POST /items
        public ActionResult<ItemDto> CreateItemDto(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
            repository.CreateItem(item);
            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.AsDto());
        }

        [HttpPut("{id}")]//PUT /items/{id}
        public ActionResult UpdateItemDto(Guid id, UpdateItemDto itemDto)
        {
            var existsItem = repository.GetItem(id);
            if (existsItem is null)
            {
                return NotFound();
            }
            Item updateItem = existsItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };
            repository.UpdateItem(updateItem);
            return NoContent();
        }

        [HttpDelete("{id}")]//DELETE //items/{id}
        public ActionResult DeleteItem(Guid id)
        {
            var deltedItem = repository.GetItem(id);
            if (deltedItem is null)
            {
                return NotFound();
            }
            repository.DeleteItem(deltedItem);
            return NoContent();
        }
    }
}