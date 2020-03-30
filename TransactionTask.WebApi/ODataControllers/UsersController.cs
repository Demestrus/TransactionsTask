using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.OData;
using TransactionTask.Core.Models;

namespace TransactionTask.WebApi.ODataControllers
{
    public class UsersController : ODataController
    {
        private readonly TaskDbContext _db;

        public UsersController(TaskDbContext db)
        {
            _db = db;
        }
        
        public IQueryable<User> Get()
        {
            return _db.Users;
        }
        
        public SingleResult<User> Get([FromODataUri] int id)
        {
            var result = _db.Users.Where(p => p.Id == id);
            return SingleResult.Create(result);
        }
    }
}