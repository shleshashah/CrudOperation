using CrudOperation.Interface;
using CrudOperation.Models;
using CrudOperation.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public CustomersAPIController(ICustomer customers)
        {
            _Customer = customers;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomer()
        {
            ResponseHelper objHelper = new ResponseHelper();
            try
            {
                List<Customer> vCustomer = await _Customer.getall();
                objHelper.Status = StatusCodes.Status200OK;
                objHelper.Message = ErrrorMessageEnum.GetSuccessfully;
                objHelper.Data = vCustomer;

                return Ok(objHelper);
            }
            catch (Exception ex)
            {
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
                objHelper.Status = SystemExceptions.InternalExcep(Convert.ToInt16(ex.Data["ErrorCode"]));
                objHelper.Message = ex.Message;
                return StatusCode(objHelper.Status, objHelper);
            }
        }

        //[HttpPost]
        //public async Task<ActionResult<Customer>> AddCustomer(Customer customer)
        //{
        //    try
        //    {
        //        _context.Customers.Add(customer);
        //        await _context.SaveChangesAsync();

        //        return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception
        //        return StatusCode(500, "Internal server error");
        //    }
        //}

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
                objHelper.Status = SystemExceptions.InternalExcep(Convert.ToInt16(ex.Data["ErrorCode"]));
                objHelper.Message = ex.Message;
                return StatusCode(objHelper.Status, objHelper);
            }
        }
    }
}
