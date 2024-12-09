using FluentMigrator;
using FluentMigrator.Builders.Alter.Table;
using FluentMigrator.Builders.Create.Table;
using FluentMigrator.Builders.Execute;
using FluentMigrator.Infrastructure;
using MySQL.Migrations.Database.FluentObjects;

namespace MySQL.Migrations.Database
{
    internal static class FluentExtensions
    {
        /// <summary>
        /// Create Primary Key
        /// </summary>
        /// <param name="table"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static IFluentSyntax CreatePK(ICreateTableWithColumnOrSchemaOrDescriptionSyntax table, string tableName)
        {
            return table.WithColumn(Column.Id)
                .AsString(36)
                .PrimaryKey(Tables.PrimaryKey(tableName))
                .NotNullable()
                //.Identity()
                .WithColumnDescription("Primary key");
        }

        /// <summary>
        /// Create Primary Key
        /// </summary>
        /// <param name="table"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static IFluentSyntax CreatePK_Long(ICreateTableWithColumnOrSchemaOrDescriptionSyntax table, string tableName)
        {
            return table.WithColumn(Column.Id)
                .AsString(36)
                .PrimaryKey(Tables.PrimaryKey(tableName))
                .NotNullable()
                //.Identity()
                .WithColumnDescription("Primary key");
        }

        /// <summary>
        /// Create Default columns
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static IFluentSyntax CreateDefaultColumns(ICreateTableWithColumnOrSchemaOrDescriptionSyntax table)
        {
            return table
                .WithColumn(Column.IsActive)
                  .AsBoolean()
                  .WithDefaultValue(true)
                  .WithColumnDescription("Active or Inactive")
                .WithColumn(Column.IsDeleted)
                  .AsBoolean()
                  .WithDefaultValue(false)
                  .WithColumnDescription("Record Logically Deleted")
                .WithColumn(Column.CreateByUser)
                      .AsString(36)
                        .NotNullable()
                        .WithColumnDescription("user ID")
                .WithColumn(Column.CreationDate)
                  .AsDateTime()
                  .WithDefault(SystemMethods.CurrentDateTime)
                  .WithColumnDescription("Creation date")
                  .WithColumn(Column.UpdateByUser)
                      .AsString(36)
                        .Nullable()
                        .WithColumnDescription("user ID")
                .WithColumn(Column.ModificationDate)
                  .AsDateTime()
                  .Nullable()
                  .WithColumnDescription("Date of last change");
        }

        /// <summary>
        /// Create Default Columns
        /// </summary>
        /// <param name="table"></param>
        /// <param name="schema"></param>
        /// <returns></returns>
        public static IFluentSyntax CreateDefaultColumn(IAlterTableAddColumnOrAlterColumnOrSchemaOrDescriptionSyntax table, string schema = "")
        {
            return table.InSchema(schema)
                .AddColumn(Column.IsActive)
                  .AsBoolean()
                  .WithDefaultValue(true)
                  .WithColumnDescription("Active or Inactive")
                .AddColumn(Column.IsDeleted)
                  .AsBoolean()
                  .WithDefaultValue(false)
                  .WithColumnDescription("Record Logically Deleted")
                .AddColumn(Column.CreateByUser)
                      .AsString(36)
                        .NotNullable()
                        .WithColumnDescription("user ID")
                .AddColumn(Column.CreationDate)
                  .AsDateTime()
                  .WithDefault(SystemMethods.CurrentDateTime)
                  .WithColumnDescription("Creation date")
                 .AddColumn(Column.UpdateByUser)
                      .AsString(36)
                        .Nullable()
                        .WithColumnDescription("user ID")
                .AddColumn(Column.ModificationDate)
                  .AsDateTime()
                  .Nullable()
                  .WithColumnDescription("Date of last change");
        }

        /// <summary>
        /// Create Default column Is Deleted
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static IFluentSyntax CreateDefaultColumnsIsActive(ICreateTableWithColumnOrSchemaOrDescriptionSyntax table)
        {
            return table
                .WithColumn(Column.IsActive)
                  .AsBoolean()
                  .WithDefaultValue(true)
                  .WithColumnDescription("Record Logically Active");
        }

        /// <summary>
        /// Create Default column Is Deleted
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static IFluentSyntax CreateDefaultColumnsIsDeleted(ICreateTableWithColumnOrSchemaOrDescriptionSyntax table)
        {
            return table
                .WithColumn(Column.IsDeleted)
                  .AsBoolean()
                  .WithDefaultValue(false)
                  .WithColumnDescription("Record Logically Deleted");
        }

        /// <summary>
        /// Create Default column Creation date
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static IFluentSyntax CreateDefaultColumnsCreationDate(ICreateTableWithColumnOrSchemaOrDescriptionSyntax table)
        {
            return table
                .WithColumn(Column.CreationDate)
                  .AsDateTime()
                  .WithDefault(SystemMethods.CurrentDateTime)
                  .WithColumnDescription("Creation date");
        }

        /// <summary>
        /// Create Default column Date of last change
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static IFluentSyntax CreateDefaultColumnsDateOfLastChange(ICreateTableWithColumnOrSchemaOrDescriptionSyntax table)
        {
            return table
                .WithColumn(Column.ModificationDate)
                  .AsDateTime()
                  .Nullable()
                  .WithColumnDescription("Date of last change");
        }

        /// <summary>
        /// Extension method for create statement to reset sequence of primary key
        /// </summary>
        /// <param name="expression">Expression reference to fluent api</param>
        /// <param name="tableName">The table name to apply reset sequence</param>
        public static void RestartSequence(this IExecuteExpressionRoot expression, string tableName)
        {
            SetSequence(expression, tableName, 1);
        }

        /// <summary>
        /// Extension method for create statement to reset sequence of primary key
        /// </summary>
        /// <param name="expression">Expression reference to fluent api</param>
        /// <param name="tableName">The table name to apply reset sequence</param>
        public static void ReSequence(this IExecuteExpressionRoot expression, string tableName)
        {
            SetSequence(expression, tableName);
        }

        /// <summary>
        /// Extension method for create statement to set sequence of primary key
        /// </summary>
        /// <param name="expression">Expression reference to fluent api</param>
        /// <param name="tableName">The table name to apply reset sequence</param>
        /// <param name="sequenceValue">The sequence value to set</param>
        public static void SetSequence(this IExecuteExpressionRoot expression, string tableName, int? sequenceValue = null)
        {
            var command = $"ALTER SEQUENCE {Tables.schema}.{tableName}_id_seq RESTART";
            if (sequenceValue.HasValue)
                command += $" WITH {sequenceValue.Value};";

            expression.Sql(command);
        }

        /// <summary>
        /// Extension method for add default primary key statement
        /// </summary>
        /// <param name="expression">Expression reference of fluent api</param>
        /// <param name="tableName">The table name to crete primary key</param>
        /// <returns>The instance with the primary key</returns>
        public static ICreateTableWithColumnSyntax WithDefaultPrimaryKey(this ICreateTableWithColumnSyntax expression, string tableName)
        {
            return expression.WithColumn(Column.Id)
              .AsString(36)
              .PrimaryKey(Tables.PrimaryKey(tableName))
              .NotNullable()
              .Indexed(Tables.Index(tableName, Column.Id))
              //.Identity()
              .WithColumnDescription("Primary key");
        }

        /// <summary>
        /// Extension method for add creation date column statement
        /// </summary>
        /// <param name="expression">Expression reference of fluent api</param>
        /// <returns>The instance with creation date column</returns>
        public static ICreateTableWithColumnSyntax WithCreationDateColumn(this ICreateTableWithColumnSyntax expression)
        {
            return expression.WithColumn(Column.CreationDate)
              .AsDateTime()
              .WithDefault(SystemMethods.CurrentDateTime)
              .WithColumnDescription("Creation date");
        }

        /// <summary>
        /// Extension method for add creation date column statement
        /// </summary>
        /// <param name="expression">Expression reference of fluent api</param>
        /// <returns>The instance with creation date column</returns>
        public static IAlterTableAddColumnOrAlterColumnSyntax AddCreationDateColumn(this IAlterTableAddColumnOrAlterColumnSyntax expression)
        {
            return expression.AddColumn(Column.CreationDate)
              .AsDateTime()
              .WithDefault(SystemMethods.CurrentDateTime)
              .WithColumnDescription("Creation date");
        }

        /// <summary>
        /// Extension method for create index
        /// </summary>
        /// <param name="expression">Expression reference to fluent api</param>
        /// <param name="tableName">The table name to create index</param>
        public static IndexBuilder BuildIndex(this IExecuteExpressionRoot expression, string tableName)
        {
            IndexBuilder indexBuilder = new IndexBuilder(expression, tableName);
            return indexBuilder;
        }

        public static void ExecuteEmbeddedScript(this IExecuteExpressionRoot expression, string scriptName, int version)
        {
            string script = Utils.returnScriptNameVersion(scriptName, version);

            expression.EmbeddedScript(script);
        }

        public static void ExecuteEmbeddedScript(this IExecuteExpressionRoot expression, string scriptName, string version)
        {
            string script = Utils.ReturnScriptNameByStringVersion(scriptName, version);

            expression.EmbeddedScript(script);
        }

        public static void ResetSequenceMaxId(this IExecuteExpressionRoot expression, string tableName, string schema = "compusafe")
        {
            //expression.Sql($@"
            //          select setval(
            //              pg_get_serial_sequence('{schema}.""{tableName}""', 'id'),
            //              (select id from {schema}.""{tableName}"" order by id desc limit 1)
            //          );
            //      ");
        }

        /// <summary>
        /// Insert Child Grants <br/>
        /// <paramref name="parentGrant"/> is used to get field parent_id <br/>
        /// <paramref name="childGrant"/> is a dynamic string array of Childs to insert <br/>
        /// <code>Example:
        /// Insert.InsertChildGrant(
        ///     "ReportFile",
        ///     "History",
        ///     "outro",
        ///     "teste");</code><br/>
        /// Will insert History, outro and teste to ReportFile parent Grant
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="parentGrant"></param>
        /// <param name="childGrant"></param>
        //public static void InsertChildGrant(
        //    this IInsertExpressionRoot expression,
        //    string parentGrant,
        //    params string[] childGrant)
        //{
        //    string sqlGetParentGrantID = $"(select id from compusafe.\"grant\" where name = '{parentGrant}' and parent_id is null and is_active=true and is_deleted=false order by id limit 1)";

        //    var rowsToCreate =
        //        from item in childGrant
        //        select new
        //        {
        //            action = item,
        //            name = parentGrant,
        //            parent_id = RawSql.Insert(sqlGetParentGrantID)
        //        };

        //    foreach (var row in rowsToCreate)
        //    {
        //        expression
        //            .IntoTable(Tables.grant)
        //            .InSchema(Tables.schema)
        //            .Row(row);
        //    }
        //}

        /// <summary>
        /// Delete Child Grants <br/>
        /// <paramref name="parentGrant"/> is used to get field parent_id <br/>
        /// <paramref name="childGrant"/> is a dynamic string array of Childs to delete <br/>
        /// <code>Example:
        /// Delete.DeleteChildGrant(
        ///     "ReportFile",
        ///     "History",
        ///     "outro",
        ///     "teste");</code><br/>
        /// Will delete History, outro and teste to ReportFile parent Grant
        //      public static void DeleteChildGrant(
        //         this IDeleteExpressionRoot expression,
        //         string parentGrant,
        //         params string[] childGrant)
        //      {
        //          string sqlGetParentGrantID = $"(select id from compusafe.\"grant\" where name = '{parentGrant}' and parent_id is null and is_active=true and is_deleted=false order by id limit 1)";

        //          var rowsToDelete =
        //              from item in childGrant
        //              select new
        //              {
        //                  action = item,
        //                  name = parentGrant,
        //                  parent_id = RawSql.Insert(sqlGetParentGrantID)
        //              };

        //          foreach (var row in rowsToDelete)
        //          {
        //              expression
        //                  .FromTable(Tables.grant)
        //                  .InSchema(Tables.schema)
        //                  .Row(row);
        //          }
        //}

        public static void ResetTableSequence(this IExecuteExpressionRoot expression, string tableWithSchema)
        {
            //expression.Sql($"select setval(pg_get_serial_sequence('{tableWithSchema}', 'id'), (select max(id) from {tableWithSchema}));");
        }
    }
}