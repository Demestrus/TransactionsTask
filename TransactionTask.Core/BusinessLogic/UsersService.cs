using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using TransactionTask.Core.Dto;
using TransactionTask.Core.Exceptions;
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

        public async Task<User> AddUser(string name, string surname)
        {
            try
            {
                using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var user = new User(name, surname);
                    
                    //тут могут быть некие действия в транзацкции, сопуствующие добавлению пользователя
                    //в этом случае Rollback откатит их

                    _dbContext.Users.Add(user);

                    await _dbContext.Database.ExecuteSqlCommandAsync("EXEC ValidateUser @p0, @p1", name, surname);
                    await _dbContext.SaveChangesAsync();
                    transactionScope.Complete();
                    
                    return user;
                }
            }
            catch (SqlException ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        public IEnumerable<User> GetUsers()
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions()
                {
                    IsolationLevel = IsolationLevel.ReadUncommitted
                })
            )
            {
                var users= _dbContext.Users.ToList();
                transactionScope.Complete();
                return users;
            }
        }
    }
}