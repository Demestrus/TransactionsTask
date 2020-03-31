using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionTask.Core.Models;

namespace TransactionTask.Core.BusinessLogic
{
    public interface IUsersService
    {
        ICollection<User> GetUsers();
        Task<User> GetUser(int id);
        Task<User> AddUser(string name, string surname);
        Task<User> UpdateUser(int id, User modifiedUser);
        Task<int> RemoveUser(int id);
    }
}