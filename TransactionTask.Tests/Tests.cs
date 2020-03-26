using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using TransactionTask.Core.BusinessLogic;
using TransactionTask.Core.Models;

namespace TransactionTask.Tests
{
    [TestFixture]
    public class Tests : IDisposable
    {
        public Tests()
        {
            using (var db = new TaskDbContext())
            {
                db.Database.Initialize(true);
            }
        }

        [Test]
        public async Task AddUserTest()
        {
            Func<UsersService> serviceFactory = () => new UsersService(new TaskDbContext());

            var user = await serviceFactory.Invoke().AddUser("Иван", "Иванов");
            Assert.NotNull(user);

            user = await serviceFactory.Invoke().AddUser("Иван", "Петров");
            Assert.NotNull(user);

            user = await serviceFactory.Invoke().AddUser("Петр", "Иванов");
            Assert.NotNull(user);

            Assert.CatchAsync(() => serviceFactory.Invoke().AddUser("Иван", "Иванов"));

            using (var db = new TaskDbContext())
            {
                var usersCount = db.Users.Count();
                Assert.AreEqual(usersCount, 3);
            }
        }

        public void Dispose()
        {
            using (var db = new TaskDbContext())
            {
                db.Database.Delete();
            }
        }
    }
}