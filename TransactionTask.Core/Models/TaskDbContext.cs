using System.Data.Entity;

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
    }
}