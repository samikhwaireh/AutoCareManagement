using AutoCare.Core.Models.Common;
using AutoCare.Core.Models.Entity;
using AutoCare.Services.Repository.CustomerRepo;
using Microsoft.AspNetCore.Mvc;

namespace AutoCare.WebApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpPost("customer")]
        public async Task<ActionResult<DALResponse<int>>> SaveCustomer([FromBody] Customer customer)
        {
            var response = await _customerRepository.SaveCustomer(customer);
            return GenerateResponse(response);
        }

        [HttpPost("service")]
        public async Task<ActionResult<DALResponse<int>>> SaveService([FromBody] ServiceRecord serviceRecord)
        {
            var response = await _customerRepository.SaveService(serviceRecord);
            return GenerateResponse(response);
        }

        [HttpPost("service/detail")]
        public async Task<ActionResult<DALResponse<int>>> SaveServiceDetail([FromBody] ServiceDetails serviceDetails)
        {
            var response = await _customerRepository.SaveServiceDetail(serviceDetails);
            return GenerateResponse(response);
        }

        [HttpGet("customer/{cId}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int cId)
        {
            var customer = await _customerRepository.GetCustomerById(cId);
            if (customer == null)
                return NotFound();

            return customer;
        }

        [HttpGet("customer/{cId}/service")]
        public async Task<ActionResult<ServiceRecord>> GetServiceByCustomerId(int cId)
        {
            var service = await _customerRepository.GetServiceByCustomerId(cId);
            if (service == null)
                return NotFound();

            return service;
        }

        [HttpGet("service/{sId}/details")]
        public async Task<ActionResult<DALResponse<List<ServiceDetails>>>> GetSubServiceDetails(int sId)
        {
            var response = await _customerRepository.GetSubServiceDetails(sId);
            return GenerateResponse(response);
        }

        [HttpDelete("service/detail/{sSId}")]
        public async Task<ActionResult<DALResponse<bool>>> DeleteSubServiceById(int sSId)
        {
            var response = await _customerRepository.DeleteSubServiceById(sSId);
            return GenerateResponse(response);
        }

        [HttpDelete("service/{pId}/details")]
        public async Task<ActionResult<DALResponse<bool>>> DeleteSubServicesByParentId(int pId)
        {
            var response = await _customerRepository.DeleteSubServicesByParentId(pId);
            return GenerateResponse(response);
        }

        [HttpDelete("customer/{cId}")]
        public async Task<ActionResult<DALResponse<bool>>> DeleteCustomer(int cId)
        {
            var response = await _customerRepository.DeleteCustomer(cId);
            return GenerateResponse(response);
        }

        [HttpDelete("service/{sId}")]
        public async Task<ActionResult<DALResponse<bool>>> DeleteService(int sId)
        {
            var response = await _customerRepository.DeleteService(sId);
            return GenerateResponse(response);
        }

        [HttpPut("service/detail/{sSId}")]
        public async Task<ActionResult<DALResponse<bool>>> UpdateSubServiceById(int sSId, [FromBody] ServiceDetails serviceDetails)
        {
            var response = await _customerRepository.UpdateSubServiceById(sSId, serviceDetails);
            return GenerateResponse(response);
        }

        [HttpPut("service/{sId}/totalpaid")]
        public async Task<ActionResult<DALResponse<bool>>> UpdateTotalPaidAndPriceBysId(int sId, [FromBody] ServiceRecord serviceRecord)
        {
            var response = await _customerRepository.UpdateTotalPaidAndPriceBysId(sId, serviceRecord);
            return GenerateResponse(response);
        }

        private ActionResult<DALResponse<T>> GenerateResponse<T>(DALResponse<T> response)
        {
            if (response.Error != null)
            {
                // Handle the error based on your application's logic
                // For example, log the error or return a specific error message
                return BadRequest(response.Error);
            }

            return response;
        }
    }
}