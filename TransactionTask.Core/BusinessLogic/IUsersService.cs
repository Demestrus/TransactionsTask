using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionTask.Core.Models;

namespace TransactionTask.Core.BusinessLogic
{
    public interface IUsersService
    {
        Task<User> AddUser(string name, string surname);

        IEnumerable<User> GetUsers();
    }
}