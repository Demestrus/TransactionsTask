namespace TransactionTask.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        private const string CreateTableSql =
            "CREATE TABLE [Users] (" +
            "    Id INT IDENTITY PRIMARY KEY, "+
            "    Name NVARCHAR(30) NOT NULL, " +
            "    Surname NVARCHAR(30) NOT NULL, " +
            "    CreateDate bigint NOT NULL, " +
            ")";

        private const string DropTableSql =
            "DROP TABLE [Users]";

        private const string ValidateUserProcedureSql =
            "CREATE PROC ValidateUser " +
            "    @Name NVARCHAR(30), " +
            "    @Surname NVARCHAR(30) " +
            "AS" +
            "    BEGIN" +
            "        IF EXISTS (SELECT Id FROM Users" +
            "                   WHERE [Name] = @Name AND [Surname] = @Surname)" +
            "            RAISERROR ('User with name \"%s %s\" exists.', 11, -1, @Name, @Surname)" +
            "    END ";

        private const string DropProcedureSql =
            "DROP PROC ValidateUser";
        
        public override void Up()
        {
            Sql(CreateTableSql);
            Sql(ValidateUserProcedureSql);
        }
        
        public override void Down()
        {
            Sql(DropProcedureSql);
            Sql(DropTableSql);
        }
    }
}
