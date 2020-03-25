using System;
using System.Threading.Tasks;
using TransactionTask.Core.Models;

namespace TransactionTask.Core.BusinessLogic
{
    public class UsersService : IUsersService
    {
        private readonly TaskDbContext _dbContext;

        public UsersService(TaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUser(string name, string surname)
        {
            using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    await _dbContext.Database.ExecuteSqlCommandAsync("EXEC ValidateUser @p0, @p1", name, surname);

                    _dbContext.Users.Add(new User(name, surname));

                    await _dbContext.SaveChangesAsync();

                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    
                    //TODO return error response
                }
            }
        }
    }
}