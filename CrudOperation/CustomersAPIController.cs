using CrudOperation.Interface;
using CrudOperation.Models;
using CrudOperation.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudOperation
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomersAPIController : ControllerBase
    {
        private readonly ICustomer _Customer;
        private readonly ILogger<CustomersAPIController> _logger;
        public CustomersAPIController(ICustomer customers, ILogger<CustomersAPIController> logger)
        {
            _Customer = customers;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomer()
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                _logger.LogInformation("GET request received");
                List<Customer> vCustomer = await _Customer.getall();
                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = ErrrorMessageEnum.GetSuccessfully;
                objHelper.Data = vCustomer;

                return Ok(objHelper);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetALL Error: " + ex.Message);
                objHelper.Status = SystemExceptions.InternalExcep(Convert.ToInt16(ex.Data["ErrorCode"]));
                objHelper.Message = ex.Message;
                return StatusCode(objHelper.Status, objHelper);
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCustomer(int Id)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                Customer vCustomer = await _Customer.get(Id);
                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = ErrrorMessageEnum.GetSuccessfully;
                objHelper.Data = vCustomer;

                return Ok(objHelper);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get Error: " + ex.Message);
                objHelper.Status = SystemExceptions.InternalExcep(Convert.ToInt16(ex.Data["ErrorCode"]));
                objHelper.Message = ex.Message;
                return StatusCode(objHelper.Status, objHelper);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> AddCustomer(Customer customer)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                await _Customer.Add(customer);
                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = ErrrorMessageEnum.SavedSuccessfully;
                objHelper.Data = null;

                return Ok(objHelper);
            }
            catch (Exception ex)
            {
                _logger.LogError("Add Error: " + ex.Message);
                objHelper.Status = SystemExceptions.InternalExcep(Convert.ToInt16(ex.Data["ErrorCode"]));
                objHelper.Message = ex.Message;
                return StatusCode(objHelper.Status, objHelper);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditCustomer(Customer customer)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                await _Customer.Edit(customer);
                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = ErrrorMessageEnum.UpdatedSuccessfully;
                objHelper.Data = null;

                return Ok(objHelper);
            }
            catch (Exception ex)
            {
                _logger.LogError("Edit Error: " + ex.Message);
                objHelper.Status = SystemExceptions.InternalExcep(Convert.ToInt16(ex.Data["ErrorCode"]));
                objHelper.Message = ex.Message;
                return StatusCode(objHelper.Status, objHelper);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCustomer(int Id)
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                await _Customer.Delete(Id);
                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = ErrrorMessageEnum.DeletedSuccessfully;
                objHelper.Data = null;

                return Ok(objHelper);
            }
            catch (Exception ex)
            {
                _logger.LogError("Delete Error: " + ex.Message);
                objHelper.Status = SystemExceptions.InternalExcep(Convert.ToInt16(ex.Data["ErrorCode"]));
                objHelper.Message = ex.Message;
                return StatusCode(objHelper.Status, objHelper);
            }
        }
    }
}
