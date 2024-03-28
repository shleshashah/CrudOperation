using CrudOperation.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudOperation.Interface
{
    public interface ICustomer
    {
        Task<Customer> get(int Id);
        Task Add(Customer customerPayload);
        Task Edit(Customer customerPayload);
        Task Delete(int id);
        Task<List<Customer>> getall();
        Task<Customer> ValidateUser(string username, string Password);
        Task<int> GetId(string username);
        Task<string> UpdatePassword(ChangePasswordPayload changePasswordPayload);

    }
}
