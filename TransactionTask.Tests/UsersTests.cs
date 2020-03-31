using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using TransactionTask.Core.BusinessLogic;
using TransactionTask.Core.Exceptions;
using TransactionTask.Core.Models;

namespace TransactionTask.Tests
{
    [TestFixture]
    public class UsersTests : IDisposable
    {
        private readonly Func<UsersService> _serviceFactory = () => new UsersService(new TaskDbContext());
        
        public UsersTests()
        {
            using (var db = new TaskDbContext())
            {
                db.Database.Initialize(true);
            }
        }

        [Test]
        public async Task GetUserTest()
        {
            const int userId = (int) UserIdPool.GetId;
            const int fakeId = (int) UserIdPool.FakeUserId;
            
            await AddUser(new User
            {
                Id = userId,
                Name = "Иван",
                Surname = "Иванов",
            });
            
            var user = await _serviceFactory.Invoke().GetUser(userId);
            Assert.NotNull(user);
            Assert.CatchAsync<NotFoundException>(() => _serviceFactory.Invoke().GetUser(fakeId));
        }
        
        [Test]
        public async Task AddUserTest()
        {
            var user = await _serviceFactory.Invoke().AddUser("Иван", "Иванов");
            Assert.NotNull(user);

            user = await _serviceFactory.Invoke().AddUser("Иван", "Петров");
            Assert.NotNull(user);

            user = await _serviceFactory.Invoke().AddUser("Петр", "Иванов");
            Assert.NotNull(user);

            Assert.CatchAsync(() => _serviceFactory.Invoke().AddUser("Иван", "Иванов"));

            using (var db = new TaskDbContext())
            {
                var usersCount = db.Users.Count();
                Assert.AreEqual(usersCount, 3);
            }
        }

        [Test]
        public async Task UpdateUserTest()
        {
            const int userId = (int) UserIdPool.UpdateId;

            await AddUser(new User
            {
                Id = userId,
                Name = "Иван",
                Surname = "Иванов",
            });

            var updatedUser = new User
            {
                Name = "Петр",
                Surname = "Петров"
            };

            var user = await _serviceFactory.Invoke().UpdateUser(userId, updatedUser);
            
            Assert.AreEqual(user.Name, updatedUser.Name);
            Assert.AreEqual(user.Surname, updatedUser.Surname);
            Assert.AreNotEqual(user.Id, updatedUser.Id);
            Assert.AreNotEqual(user.CreateDate, updatedUser.CreateDate);
        }

        [Test]
        public async Task RemoveUserTest()
        {
            const int userId = (int) UserIdPool.RemoveId;

            await AddUser(new User
            {
                Id = userId,
                Name = "Иван",
                Surname = "Иванов",
            });
            
            var returnedId = await _serviceFactory.Invoke().RemoveUser(userId);
            
            User user;
            using (var db = new TaskDbContext())
            {
                user = await db.Users.FindAsync(userId);
            }
            
            Assert.IsNull(user);
            Assert.AreEqual(userId, returnedId);
        }
        
        public void Dispose()
        {
            using (var db = new TaskDbContext())
            {
                db.Database.Delete();
            }
        }

        private static async Task AddUser(User user)
        {
            using (var db = new TaskDbContext())
            {
                db.Users.Add(user);
                await db.SaveChangesAsync();
            }
        }

        private enum UserIdPool
        {
            FakeUserId = -1,
            GetId = 1,
            UpdateId = 2,
            RemoveId = 3
        }
    }
}