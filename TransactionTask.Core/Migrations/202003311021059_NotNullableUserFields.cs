namespace TransactionTask.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotNullableUserFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Surname", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Surname", c => c.String());
            AlterColumn("dbo.Users", "Name", c => c.String());
        }
    }
}
