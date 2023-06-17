using AutoCare.Core.Models.Common;
using AutoCare.Core.Models.Entity;

namespace AutoCare.Services.Repository.ItemRepo;

public interface IItemRepository
{
    Task<DALResponse<List<Item>>> GetAllItems();
    Task<DALResponse<bool>> SaveItem(Item item);
    Task<DALResponse<bool>> UpdateItem(int id, Item item);
    Task<DALResponse<bool>> DeleteItem(int id);
    Task<DALResponse<List<Item>>> SearchItem(string search);
}
