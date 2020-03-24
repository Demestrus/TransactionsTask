using System.Threading.Tasks;
using System.Web.Http;
using TransactionTask.Core.BusinessLogic;
using TransactionTask.WebApi.Models;

namespace TransactionTask.WebApi.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUsersService _service;

        public UsersController(IUsersService service)
        {
            _service = service;
        }
        
        [HttpPost]
        public async Task AddUser(UserDto user)
        {
            await _service.AddUser(user.Name, user.Surname);
        }
    }
}