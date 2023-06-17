using AutoCare.Core.Models.Common;
using AutoCare.Core.Models.Entity;
using AutoCare.Services.Repository.ItemRepo;
using Microsoft.AspNetCore.Mvc;

namespace AutoCare.WebApi.Controllers;
[ApiController]
[Route("api/item")]
public class ItemController : ControllerBase
{
    private readonly IItemRepository _itemRepository;

    public ItemController(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    private IActionResult HandleResponse<T>(DALResponse<T> response)
    {
        if (response.Error != null)
        {
            // Handle the error
            return StatusCode(500, response.Error);
        }

        return Ok(response.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllItems()
    {
        var response = await _itemRepository.GetAllItems();
        return HandleResponse(response);
    }

    [HttpPost]
    public async Task<IActionResult> SaveItem([FromBody] Item item)
    {
        var response = await _itemRepository.SaveItem(item);
        return HandleResponse(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateItem(int id, [FromBody] Item item)
    {
        var response = await _itemRepository.UpdateItem(id, item);
        return HandleResponse(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItem(int id)
    {
        var response = await _itemRepository.DeleteItem(id);
        return HandleResponse(response);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchItem([FromQuery] string search)
    {
        var response = await _itemRepository.SearchItem(search);
        return HandleResponse(response);
    }
}
