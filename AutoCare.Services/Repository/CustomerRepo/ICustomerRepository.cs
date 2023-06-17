using AutoCare.Core.Models.Common;
using AutoCare.Core.Models.Entity;

namespace AutoCare.Services.Repository.CustomerRepo;

public interface ICustomerRepository
{
    Task<DALResponse<int>> SaveCustomer(Customer customer);
    Task<DALResponse<int>> SaveService(ServiceRecord serviceRecord);
    Task<DALResponse<int>> SaveServiceDetail(ServiceDetails serviceDetails);
    Task<Customer> GetCustomerById(int cId);
    Task<ServiceRecord> GetServiceByCustomerId(int cId);
    Task<DALResponse<List<ServiceDetails>>> GetSubServiceDetails(int sId);
    Task<DALResponse<bool>> DeleteSubServiceById(int sSId);
    Task<DALResponse<bool>> DeleteSubServicesByParentId(int pId);
    Task<DALResponse<bool>> DeleteCustomer(int cId);
    Task<DALResponse<bool>> DeleteService(int sId);
    Task<DALResponse<bool>> UpdateSubServiceById(int sSId, ServiceDetails serviceDetails);
    Task<DALResponse<bool>> UpdateTotalPaidAndPriceBysId(int sId, ServiceRecord serviceRecord);
}
