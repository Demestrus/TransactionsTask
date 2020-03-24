using System.Data.Entity.Migrations;
using TransactionTask.Core.Models;

namespace TransactionTask.Core.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<TaskDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
    } 
}