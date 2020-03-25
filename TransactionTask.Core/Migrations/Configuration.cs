
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace TransactionTask.Core.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<TransactionTask.Core.Models.TaskDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
    } 
}