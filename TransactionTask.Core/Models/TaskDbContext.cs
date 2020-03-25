using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace TransactionTask.Core.Models
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext()
        {
            
        }

        public TaskDbContext(string nameOfConnectionString) : base(nameOfConnectionString)
        {
            
        }
        
        public DbSet<User> Users { get; set; }

        public override int SaveChanges()
        {
            PreSaving();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            PreSaving();
            return base.SaveChangesAsync();
        }

        private void PreSaving()
        {
            foreach (var userEntry in ChangeTracker.Entries<User>())
            {
                if (userEntry.State != EntityState.Added) continue;
                
                userEntry.Entity.CreateDate = DateTime.UtcNow.Ticks;
            }
        }
    }
}