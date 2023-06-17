using AutoCare.Core.Models.Common;
using Dapper;
using System.Data;

namespace AutoCare.Services.Repository;

public class BaseRepo
{
    protected readonly IDbConnection _connection;

    public BaseRepo(IDbConnection connection)
    {
        _connection = connection;
    }
    public async Task<DALResponse<bool>> DeleteRecordById(string tableName, string columnName, int recordId, ErrorsCatalog errorCatalog)
    {
        DALResponse<bool> response = new DALResponse<bool>();

        try
        {
            var query = $"DELETE FROM {tableName} WHERE {columnName} = @id";
            var parameters = new { id = recordId };

            var rowsAffected = await _connection.ExecuteAsync(query, parameters);

            response.Value = rowsAffected > 0;
        }
        catch (Exception e)
        {
            response.Error = errorCatalog;
            response.Error.Exception = e;
        }

        return response;
    }

    protected async Task<DALResponse<int>> ExecuteInsert(string tableName, object parameters, ErrorsCatalog errorCatalog)
    {
        DALResponse<int> response = new DALResponse<int>();

        try
        {
            string query = $"INSERT INTO {tableName} {GetInsertColumns(parameters)} VALUES {GetInsertValues(parameters)}";

            var rowsAffected = await _connection.ExecuteAsync(query, parameters);

            if (rowsAffected > 0)
            {
                response.Value = (int)rowsAffected;
            }
            else
            {
                response.Error = errorCatalog;
                response.Value = rowsAffected;
            }
        }
        catch (Exception e)
        {
            response.Error = errorCatalog;
            response.Error.Exception = e;
        }

        return response;
    }

    private string GetInsertColumns(object parameters)
    {
        var properties = parameters.GetType().GetProperties();
        var columns = properties.Select(property => property.Name);
        return $"({string.Join(", ", columns)})";
    }

    private string GetInsertValues(object parameters)
    {
        var properties = parameters.GetType().GetProperties();
        var values = properties.Select(property => $"@{property.Name}");
        return $"({string.Join(", ", values)})";
    }
}
