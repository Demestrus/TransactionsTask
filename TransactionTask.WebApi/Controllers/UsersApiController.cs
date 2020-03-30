using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using TransactionTask.Core.BusinessLogic;
using TransactionTask.WebApi.Models;

namespace TransactionTask.WebApi.Controllers
{
    [RoutePrefix("api/Users")]
    public class UsersApiController : ApiController
    {
        private readonly IUsersService _service;
        private readonly IMapper _mapper;

        public UsersApiController(IUsersService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<ExistingUserDto> GetUsers()
        {
            var users = _service.GetUsers();
            return _mapper.Map<IEnumerable<ExistingUserDto>>(users);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> AddUser(UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var user = await _service.AddUser(userDto.Name, userDto.Surname);

            return Ok(_mapper.Map<ExistingUserDto>(user));
        }
    }
}