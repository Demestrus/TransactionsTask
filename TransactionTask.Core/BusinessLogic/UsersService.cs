using System;
using System.Threading.Tasks;
using TransactionTask.Core.Dto;
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

        public async Task<UserResult> AddUser(string name, string surname)
        {
            using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
            {
                //тут могут быть некие действия в транзацкции, сопуствующие добавлению пользователя
                //в этом случае Rollback откатит их

                _dbContext.Users.Add(new User(name, surname));

                try
                {
                    await _dbContext.Database.ExecuteSqlCommandAsync("EXEC ValidateUser @p0, @p1", name, surname);
                    await _dbContext.SaveChangesAsync();
                    dbContextTransaction.Commit();
                    return new UserResult();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return new UserResult(false, ex.Message);
                }
            }
        }
    }
}