using Catalog.Dtos;
using Catlog.Entities;

namespace Catalog
{
    public static class Extension
    {
        public static ItemDto AsDto(this Item item) => new ItemDto
        {
            Id = item.Id,
            Name = item.Name,
            Price = item.Price,
            CreatedDate = item.CreatedDate
        };
    }
}
