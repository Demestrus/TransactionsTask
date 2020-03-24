using System.Threading.Tasks;

namespace TransactionTask.Core.BusinessLogic
{
    public interface IUsersService
    {
        Task AddUser(string name, string surname);
    }
}