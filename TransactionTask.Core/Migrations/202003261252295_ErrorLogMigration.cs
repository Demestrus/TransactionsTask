namespace TransactionTask.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ErrorLogMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogEntries",
                c => new
                    {
                        TimeStamp = c.Long(nullable: false),
                        ErrorMsg = c.String(),
                        StackTrace = c.String(),
                    })
                .PrimaryKey(t => t.TimeStamp);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LogEntries");
        }
    }
}
