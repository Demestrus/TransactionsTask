using System;
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
        public async Task<IHttpActionResult> AddUser(UserDto user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var result = await _service.AddUser(user.Name, user.Surname);

            if (!result.Success)
                return BadRequest(result.ErrorMsg);

            return Ok(new CreatedUserDto
            {
                Name = result.User.Name,
                Surname = result.User.Surname,
                CreateDate = new DateTime(result.User.CreateDate)
                    .ToLocalTime() //в идеале преобразование в локальное время следует делать
                                   //на фронтэнде, чтобы учесть часовой пояс клиента
            });
        }
    }
}