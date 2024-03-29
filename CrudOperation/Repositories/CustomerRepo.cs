using CrudOperation.Interface;
using CrudOperation.Models;
using CrudOperation.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudOperation.Repositories
{
    public class CustomerRepo : ICustomer
    {
        private readonly AppDbContext _adbContext;
        public CustomerRepo(AppDbContext adbContext)
        {
            _adbContext = adbContext;
        }
        public async Task Add(Customer customerPayload)
        {
            try
            {
                if (!_adbContext.Customers.Where(w => w.LoginUser.ToLower() == customerPayload.LoginUser.ToLower() && w.IsDeleted == false).Any())    //Login UserId shoud be Unique
                {
                    customerPayload.Password = EncryptionDecryption.EncryptValye(customerPayload.Password);
                    customerPayload.Email = EncryptionDecryption.EncryptValye(customerPayload.Email);
                    customerPayload.PhoneNumber = EncryptionDecryption.EncryptValye(customerPayload.PhoneNumber);
                    customerPayload.IsActive = true;
                    customerPayload.IsDeleted = false;
                    customerPayload.AddedDate = DateTime.Now;
                    _adbContext.Customers.Add(customerPayload);
                    await Task.FromResult(_adbContext.SaveChanges());
                }
                else
                {
                    throw new SystemExceptions(ErrrorMessageEnum.AlreadyExist, StatusCodes.Status409Conflict);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var vCustomer = _adbContext.Customers.Where(w => w.Id == id).FirstOrDefault();
                if (vCustomer != null)
                {
                    vCustomer.IsDeleted = true;
                    _adbContext.Update(vCustomer);
                    await Task.FromResult(_adbContext.SaveChanges());
                }
                else
                {
                    throw new SystemExceptions(ErrrorMessageEnum.DataNotFound, StatusCodes.Status204NoContent);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Edit(Customer customerPayload)
        {
            try
            {
                var vCustomer = _adbContext.Customers.Where(w => w.Id == customerPayload.Id).FirstOrDefault();
                if (vCustomer != null)
                {
                    if (!_adbContext.Customers.Where(w => w.LoginUser.ToLower() == customerPayload.LoginUser.ToLower() && w.IsDeleted == false && w.Id != customerPayload.Id).Any())
                    {
                        vCustomer.FirstName = customerPayload.FirstName;
                        vCustomer.LastName = customerPayload.LastName;
                        vCustomer.Email = EncryptionDecryption.EncryptValye(customerPayload.Email);
                        vCustomer.PhoneNumber = EncryptionDecryption.EncryptValye(customerPayload.PhoneNumber);
                        vCustomer.UpdatedDate = DateTime.Now;
                        _adbContext.Update(vCustomer);
                        await Task.FromResult(_adbContext.SaveChanges());
                    }
                    else
                    {
                        throw new SystemExceptions(ErrrorMessageEnum.AlreadyExist, StatusCodes.Status409Conflict);
                    }
                }
                else
                {
                    throw new SystemExceptions(ErrrorMessageEnum.DataNotFound, StatusCodes.Status204NoContent);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Customer> get(int Id)
        {
            try
            {
                return await Task.FromResult(_adbContext.Customers.Where(w => w.Id == Id && w.IsDeleted == false).Select(s => new Customer
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    LoginUser = s.LoginUser,
                    Password = "*****",
                    PhoneNumber = EncryptionDecryption.DecryptValye(s.PhoneNumber),
                    Email = EncryptionDecryption.DecryptValye(s.Email),
                    IsActive = s.IsActive,
                    IsDeleted = s.IsDeleted,
                    AddedDate = s.AddedDate
                }).FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Customer>> getall()
        {
            try
            {
                return await Task.FromResult(_adbContext.Customers.Where(w => w.IsDeleted == false).Select(s => new Customer
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    LoginUser = s.LoginUser,
                    Password = "*****",
                    PhoneNumber = EncryptionDecryption.DecryptValye(s.PhoneNumber),
                    Email = EncryptionDecryption.DecryptValye(s.Email),
                    IsActive = s.IsActive,
                    IsDeleted = s.IsDeleted,
                    AddedDate = s.AddedDate
                }).ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Customer> ValidateUser(string username, string Password)
        {
            try
            {
                return await Task.FromResult(_adbContext.Customers.Where(w => w.LoginUser.ToLower() == username.ToLower() && w.Password == EncryptionDecryption.EncryptValye(Password) && w.IsDeleted == false).Select(s => new Customer
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    LoginUser = s.LoginUser,
                    Password = "*****",
                    PhoneNumber = EncryptionDecryption.DecryptValye(s.PhoneNumber),
                    Email = EncryptionDecryption.DecryptValye(s.Email),
                    IsActive = s.IsActive,
                    IsDeleted = s.IsDeleted,
                    AddedDate = s.AddedDate
                }).FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> GetId(string username)
        {
            try
            {
                return await Task.FromResult(_adbContext.Customers.Where(w => w.LoginUser.ToLower() == username.ToLower() && w.IsDeleted == false).Select(s => s.Id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> UpdatePassword(ChangePasswordPayload customerPayload)
        {
            try
            {
                var vCustomer = _adbContext.Customers.Where(w => w.LoginUser.ToLower() == customerPayload.LoginUser.ToLower() && w.IsDeleted == false).FirstOrDefault();
                if (vCustomer != null)
                {
                    vCustomer.Password = EncryptionDecryption.EncryptValye(customerPayload.Password);
                    vCustomer.ConfirmPassword = EncryptionDecryption.EncryptValye(customerPayload.ConfirmPassword);
                    vCustomer.UpdatedDate = DateTime.Now;
                    _adbContext.Update(vCustomer);
                    _adbContext.SaveChanges();
                    return await Task.FromResult(EncryptionDecryption.DecryptValye(vCustomer.Email));
                }
                else
                {
                    throw new SystemExceptions(ErrrorMessageEnum.DataNotFound, StatusCodes.Status204NoContent);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddToken(int Id, string token)
        {
            try
            {
                var vCustomer = _adbContext.Customers.Where(w => w.Id == Id).FirstOrDefault();
                if (vCustomer != null)
                {
                    vCustomer.Token = token;
                    _adbContext.Update(vCustomer);
                    await Task.FromResult(_adbContext.SaveChanges());

                }
                else
                {
                    throw new SystemExceptions(ErrrorMessageEnum.DataNotFound, StatusCodes.Status204NoContent);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
