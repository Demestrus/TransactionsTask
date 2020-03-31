using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData;
using TransactionTask.Core.BusinessLogic;
using TransactionTask.Core.Models;
using TransactionTask.WebApi.Models;

namespace TransactionTask.WebApi.ODataControllers
{
    public class UsersController : ODataController
    {
        private readonly IUsersService _service;
        private readonly IMapper _mapper;

        public UsersController(IUsersService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        
        public IQueryable<ExistingUserDto> Get()
        {
            return _service.GetUsers()
                .AsQueryable()
                .ProjectTo<ExistingUserDto>(_mapper.ConfigurationProvider);
        }
        
        public async Task<SingleResult<ExistingUserDto>> Get([FromODataUri] int key)
        {
            var user = await _service.GetUser(key);
            return SingleResult.Create(new [] {_mapper.Map<ExistingUserDto>(user)}.AsQueryable());
        }

        public async Task<IHttpActionResult> Post(UserDto newUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var user = await _service.AddUser(newUser.Name, newUser.Surname);
            return Ok(_mapper.Map<ExistingUserDto>(user));
        }

        public async Task<IHttpActionResult> Put([FromODataUri] int key, UserDto update)
        {
            var user = await _service.UpdateUser(key, _mapper.Map<User>(update));

            return Ok(_mapper.Map<ExistingUserDto>(user));
        }

        public async Task<int> Delete([FromODataUri] int key)
        {
            return await _service.RemoveUser(key);
        }
    }
}