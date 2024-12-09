using FluentMigrator;

namespace MySQL.Migrations.Database.Management
{
    [Migration(202409131540, "Creación de tabla de Logs")]
    public class _202409131540_Create_Table_ExceptionLog : Migration
    {
        public override void Down()
        {
            Delete.Table(Tables.ExceptionLog);
        }

        public override void Up()
        {
            createLogEntry();
        }

        private void createLogEntry()
        {
            var table = Create.Table(Tables.ExceptionLog);
            table.WithDescription("Table for Exception Logs");

            // FluentExtensions.CreatePK(table, Tables.ExceptionLog);

            table
                .WithColumn(Column.Id)
                  .AsCustom("bigint")
                  .PrimaryKey()
                  .Identity()
                  .WithColumnDescription("Primary Key");

            table
                .WithColumn(Column.ExceptionLog.LogLevel)
                     .AsCustom("MEDIUMTEXT")
                    .NotNullable()
                    .WithColumnDescription("log nivel");

            table
                .WithColumn(Column.ExceptionLog.Message)
                    .AsCustom("MEDIUMTEXT")
                    .NotNullable()
                    .WithColumnDescription("log nivel");

            table
                .WithColumn(Column.ExceptionLog.StackTrace)
                    .AsCustom("MEDIUMTEXT")
                    .NotNullable()
                    .WithColumnDescription("StackTrace");

            table
              .WithColumn(Column.ExceptionLog.UserId)
                  .AsString(36)
                  .NotNullable()
                  .WithColumnDescription("UserId");
            table
            .WithColumn(Column.ExceptionLog.ServiceName)
                .AsString(75)
                .NotNullable()
                .WithColumnDescription("ServiceName");

            FluentExtensions.CreateDefaultColumnsIsActive(table);
            FluentExtensions.CreateDefaultColumnsIsDeleted(table);
            FluentExtensions.CreateDefaultColumnsCreationDate(table);
           

            Create.Index("idx_ExceptionLog_creation_date")
                .OnTable(Tables.ExceptionLog)
                .OnColumn(Column.CreationDate)
                .Descending();

        }
    }
}
