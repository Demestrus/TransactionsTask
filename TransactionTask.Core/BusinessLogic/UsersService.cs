using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
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

        public ICollection<User> GetUsers()
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

        public async Task<User> GetUser(int id)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Suppress,
                new TransactionOptions()
                {
                    IsolationLevel = IsolationLevel.ReadUncommitted
                },
                TransactionScopeAsyncFlowOption.Enabled)
            )
            {
                var user = await _dbContext.Users.SingleOrDefaultAsync(s => s.Id == id);
                transactionScope.Complete();

                if (user == null)
                {
                    throw new NotFoundException($"Пользователь с id {id} не найден");
                }
                
                return user;
            }
        }
        
        public async Task<User> AddUser(string name, string surname)
        {
            try
            {
                using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var user = new User(name, surname);
                    
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

        public async Task<User> UpdateUser(int id, User modifiedUser)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var user = await GetUser(id);

                user.Name = modifiedUser.Name;
                user.Surname = modifiedUser.Surname;

                await _dbContext.SaveChangesAsync();
                transactionScope.Complete();

                return user;
            }
        }

        public async Task<int> RemoveUser(int id)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var user = await GetUser(id);

                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
                
                transactionScope.Complete();
                return id;
            }
        }
    }
}