using AutoCare.Core.Models.Common;
using AutoCare.Core.Models.Entity;
using Dapper;
using System.Data;

namespace AutoCare.Services.Repository.CustomerRepo;
public class CustomerRepository : BaseRepo, ICustomerRepository
{
    public CustomerRepository(IDbConnection connection) : base(connection)
    {
    }

    public async Task<DALResponse<int>> SaveCustomer(Customer customer)
    {
        var parameters = new
        {
            Name = customer.Name,
            PlateNo = customer.PlateNumber,
            PlateType = customer.PlateType,
            AddedTime = DateTime.Now,
            AddedBy = "Global",
            Phone = customer.Phone
        };

        return await ExecuteInsert("Customers", parameters, ErrorsCatalog.FaildToAddUser);
    }

    public async Task<DALResponse<int>> SaveService(ServiceRecord serviceRecord)
    {
        var parameters = new
        {
            CustomerId = serviceRecord.CustomerId,
            addedBy = "Global",
            addedTime = DateTime.Now,
            TotalPaid = serviceRecord.TotalPaid,
            Note = serviceRecord.Note,
            borc = serviceRecord.Debt,
            Other = serviceRecord.Other
        };

        return await ExecuteInsert("Services", parameters, ErrorsCatalog.FaildToAddService);
    }

    public async Task<DALResponse<int>> SaveServiceDetail(ServiceDetails serviceDetails)
    {
        var parameters = new
        {
            parentServiceId = serviceDetails.ParentServiceId,
            Name = serviceDetails.Name,
            Price = serviceDetails.Price,
            PriceFromSupplier = serviceDetails.PriceFromSupplier,
            PriceFromShop = serviceDetails.PriceFromShop
        };

        return await ExecuteInsert("service_details", parameters, ErrorsCatalog.FaildToAddServiceDetail);
    }

    public async Task<Customer> GetCustomerById(int cId)
    {
        var query = @"SELECT Customers.id, Customers.name, Customers.plateNo, Customers.plateType, Customers.addedTime, Users.username, Customers.phone
                      FROM Customers
                      INNER JOIN Users ON Customers.addedBy = Users.id
                      WHERE Customers.id = @customerId";

        var parameters = new { customerId = cId };

        var customer = await _connection.QueryFirstOrDefaultAsync<Customer>(query, parameters);

        return customer;
    }

    public async Task<ServiceRecord> GetServiceByCustomerId(int cId)
    {
        var query = @"SELECT Services.id AS serviceId, Services.addedTime AS addedDate, Services.totalPaid AS totalPaided, Services.note AS note, Services.borc AS borc, Services.other AS other
                      FROM Services
                      INNER JOIN service_details ON Services.id = service_details.parentServiceId
                      WHERE Services.customerId = @customerId
                      GROUP BY Services.id, Services.addedTime, Services.totalPaid, Services.note, Services.borc, Services.other";

        var parameters = new { customerId = cId };
        var service = await _connection.QueryFirstOrDefaultAsync<ServiceRecord>(query, parameters);

        return service;
    }

    public async Task<DALResponse<List<ServiceDetails>>> GetSubServiceDetails(int sId)
    {
        DALResponse<List<ServiceDetails>> response = new DALResponse<List<ServiceDetails>>();
        List<ServiceDetails> serviceList = new List<ServiceDetails>();

        try
        {
            var query = "SELECT * FROM service_details WHERE parentServiceId = @parentServiceId";
            var parameters = new { parentServiceId = sId };

            var result = await _connection.QueryAsync<ServiceDetails>(query, parameters);

            serviceList = result.ToList();
            response.Value = serviceList;
        }
        catch (Exception e)
        {
            response.Error = ErrorsCatalog.FaildToRetrieveServiceDetails;
            response.Error.Exception = e;
        }

        return response;
    }

    public async Task<DALResponse<bool>> DeleteSubServiceById(int sSId)
    {
        return await DeleteRecordById("service_details", "id", sSId, ErrorsCatalog.FaildToDeleteSubService);
    }

    public async Task<DALResponse<bool>> DeleteSubServicesByParentId(int pId)
    {
        return await DeleteRecordById("service_details", "parentServiceId", pId, ErrorsCatalog.FaildToDeleteSubServices);
    }

    public async Task<DALResponse<bool>> DeleteCustomer(int cId)
    {
        return await DeleteRecordById("Customers", "id", cId, ErrorsCatalog.FaildToDeleteCustomer);
    }

    public async Task<DALResponse<bool>> DeleteService(int sId)
    {
        return await DeleteRecordById("Services", "id", sId, ErrorsCatalog.FaildToDeleteService);
    }

    public async Task<DALResponse<bool>> UpdateSubServiceById(int sSId, ServiceDetails serviceDetails)
    {
        DALResponse<bool> response = new DALResponse<bool>();

        try
        {
            var query = "UPDATE service_details SET Name = @name, price = @Price, PriceFromSupplier = @fromSuplier, " +
                "PriceFromShop = @fromShop WHERE id = @id";
            var parameters = new
            {
                Name = serviceDetails.Name,
                price = serviceDetails.Price,
                priceFromSuplier = serviceDetails.PriceFromSupplier,
                priceFromShop = serviceDetails.PriceFromShop,
                id = sSId
            };

            var rowsAffected = await _connection.ExecuteAsync(query, parameters);

            response.Value = rowsAffected > 0;
        }
        catch (Exception e)
        {
            response.Error = ErrorsCatalog.FaildToUpdateSubService;
            response.Error.Exception = e;
        }

        return response;
    }

    public async Task<DALResponse<bool>> UpdateTotalPaidAndPriceBysId(int sId, ServiceRecord serviceRecord)
    {
        DALResponse<bool> response = new DALResponse<bool>();

        try
        {
            var query = "UPDATE Services SET totalPaid = @totalPaid, note = @note, borc = @borc, other = @other WHERE id = @id";
            var parameters = new
            {
                TotalPaid = serviceRecord.TotalPaid,
                Note = serviceRecord.Note,
                Borc = serviceRecord.Debt,
                Other = serviceRecord.Other,
                Id = sId
            };

            var rowsAffected = await _connection.ExecuteAsync(query, parameters);

            response.Value = rowsAffected > 0;
        }
        catch (Exception e)
        {
            response.Error = ErrorsCatalog.FaildToUpdateTotalPaidAndPrice;
            response.Error.Exception = e;
        }

        return response;
    }
}
