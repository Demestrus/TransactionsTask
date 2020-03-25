using System.Threading.Tasks;
using TransactionTask.Core.Dto;

namespace TransactionTask.Core.BusinessLogic
{
    public interface IUsersService
    {
        Task<UserResult> AddUser(string name, string surname);
    }
}