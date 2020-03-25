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

            var result = await serviceFactory.Invoke().AddUser("Иван", "Иванов");
            Assert.True(result.Success);

            result = await serviceFactory.Invoke().AddUser("Иван", "Петров");
            Assert.False(result.Success);
            Console.WriteLine(result.ErrorMsg);

            result = await serviceFactory.Invoke().AddUser("Петр", "Иванов");
            Assert.False(result.Success);
            Console.WriteLine(result.ErrorMsg);

            result = await serviceFactory.Invoke().AddUser("Петр", "Петров");
            Assert.True(result.Success);

            using (var db = new TaskDbContext())
            {
                var usersCount = db.Users.Count();
                Assert.AreEqual(usersCount, 2);
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