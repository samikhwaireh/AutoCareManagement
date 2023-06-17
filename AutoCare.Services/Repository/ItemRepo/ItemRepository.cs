using AutoCare.Core.Models.Common;
using AutoCare.Core.Models.Entity;
using Dapper;
using System.Data;

namespace AutoCare.Services.Repository.ItemRepo;

public class ItemRepository : BaseRepo, IItemRepository
{
    public ItemRepository(IDbConnection connection) : base(connection)
    {
    }

    public async Task<DALResponse<List<Item>>> GetAllItems()
    {
        // TODO: use generic method to reduce code redundancy
        DALResponse<List<Item>> response = new DALResponse<List<Item>>();

        try
        {
            var query = "SELECT * FROM Items ORDER BY items.id DESC";
            var items = await _connection.QueryAsync<Item>(query);

            response.Value = items.ToList();
        }
        catch (Exception e)
        {
            response.Error = ErrorsCatalog.FailedToGetAllItems;
            response.Error.Exception = e;
        }

        return response;
    }

    public async Task<DALResponse<bool>> SaveItem(Item item)
    {
        // TODO: use generic method to reduce code redundancy
        DALResponse<bool> response = new DALResponse<bool>();

        try
        {
            var query = "INSERT INTO Items (Name, Qnt, No, Price, SalePrice) VALUES (@name, @qnt, @no, @price, @sellPrice)";
            var parameters = new
            {
                name = item.Name,
                qnt = item.Quantity,
                no = item.Number,
                price = item.Price,
                sellPrice = item.SellPrice
            };

            var rowsAffected = await _connection.ExecuteAsync(query, parameters);

            response.Value = rowsAffected > 0;
        }
        catch (Exception e)
        {
            response.Error = ErrorsCatalog.FailedToSaveItem;
            response.Error.Exception = e;
        }

        return response;
    }

    public async Task<DALResponse<bool>> UpdateItem(int id, Item item)
    {
        // TODO: use generic method to reduce code redundancy
        DALResponse<bool> response = new DALResponse<bool>();

        try
        {
            var query = "UPDATE Items SET Name = @name, Price = @price, Qnt = @qnt, No = @no, SalePrice = @sellPrice WHERE id = @id";
            var parameters = new
            {
                id,
                Name = item.Name,
                No = item.Number,
                Qnt = item.Quantity,
                Price = item.Price,
                SalePrice = item.SellPrice
            };

            var rowsAffected = await _connection.ExecuteAsync(query, parameters);

            response.Value = rowsAffected > 0;
        }
        catch (Exception e)
        {
            response.Error = ErrorsCatalog.FailedToUpdateItem;
            response.Error.Exception = e;
        }

        return response;
    }

    public async Task<DALResponse<bool>> DeleteItem(int id)
    {
        // TODO: use generic method to reduce code redundancy
        DALResponse<bool> response = new DALResponse<bool>();

        try
        {
            var query = "DELETE FROM Items WHERE Items.Id = @id";
            var parameters = new { id };

            var rowsAffected = await _connection.ExecuteAsync(query, parameters);

            response.Value = rowsAffected > 0;
        }
        catch (Exception e)
        {
            response.Error = ErrorsCatalog.FailedToDeleteItem;
            response.Error.Exception = e;
        }

        return response;
    }

    public async Task<DALResponse<List<Item>>> SearchItem(string search)
    {
        // TODO: use generic method to reduce code redundancy
        DALResponse<List<Item>> response = new DALResponse<List<Item>>();

        try
        {
            var query = "SELECT * FROM Items WHERE Name LIKE @name OR No LIKE @no";
            var parameters = new { Name = "%" + search + "%", No = "%" + search + "%" };

            var items = await _connection.QueryAsync<Item>(query, parameters);

            response.Value = items.ToList();
        }
        catch (Exception e)
        {
            response.Error = ErrorsCatalog.FailedToSearchItem;
            response.Error.Exception = e;
        }

        return response;
    }
}
