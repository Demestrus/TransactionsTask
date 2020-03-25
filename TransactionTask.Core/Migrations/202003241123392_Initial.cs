using System.Data.Entity.Migrations;

namespace TransactionTask.Core.Migrations
{
    public partial class Initial : DbMigration
    {
        private const string CreateTableSql =
            "CREATE TABLE [Users] (" +
            "    Id INT IDENTITY PRIMARY KEY, "+
            "    Name NVARCHAR(30), " +
            "    Surname NVARCHAR(30)" +
            ")";

        private const string DropTableSql =
            "DROP TABLE [Users]";

        private const string ValidateUserProcedureSql =
            "CREATE PROC ValidateUser" +
            "    @Name NVARCHAR(30)," +
            "    @Surname NVARCHAR(30)" +
            "AS" +
            "    BEGIN" +
            "        IF EXISTS (SELECT Id FROM Users" +
            "                   WHERE [Name] = @Name)" +
            "            RAISERROR ('User with Name \"%s\" exists.', 15, -1, @Name)" +
            "        IF EXISTS (SELECT Id FROM Users" +
            "                   WHERE [Surname] = @Surname)" +
            "            RAISERROR ('User with Surname \"%s\" exists.', 15, -1, @Surname)" +
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
