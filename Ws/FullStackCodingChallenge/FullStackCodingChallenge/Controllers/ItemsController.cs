using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FullStackCodingChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        //private readonly ItemService _itemService;

        //public ItemsController(ItemService itemService)
        //{
        //    _itemService = itemService;
        //}

        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)  // ✅ Constructor usando la interfaz
        {
            _itemService = itemService;
        }

        // 🔹 GET: api/items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetAllItems()
        {
            var items = await _itemService.GetAllItemsAsync();
            return Ok(items);
        }

        // 🔹 GET: api/items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItemById(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id);

            if (item == null)
                return NotFound(new { message = "Item no encontrado" });

            return Ok(item);
        }

        // 🔹 POST: api/items
        [HttpPost]
        public async Task<ActionResult<int>> AddItem([FromBody] Item item)
        {
            if (item == null)
                return BadRequest(new { message = "Datos inválidos" });

            var insertedId = await _itemService.AddItemAsync(item);
            return CreatedAtAction(nameof(GetItemById), new { id = insertedId }, item);
        }

        // 🔹 PUT: api/items/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] Item item)
        {
            if (id != item.Id)
                return BadRequest(new { message = "El ID del cuerpo no coincide con el de la URL" });

            var updated = await _itemService.UpdateItemAsync(item);
            if (!updated)
                return NotFound(new { message = "Item no encontrado para actualizar" });

            return NoContent();
        }

        // 🔹 DELETE: api/items/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var deleted = await _itemService.DeleteItemAsync(id);
            if (!deleted)
                return NotFound(new { message = "Item no encontrado para eliminar" });

            return NoContent();
        }
    }
}
