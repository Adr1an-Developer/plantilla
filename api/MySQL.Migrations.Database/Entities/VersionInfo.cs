using FluentMigrator.Runner.VersionTableInfo;

namespace MySQL.Migrations.Database.Entities
{
    [VersionTableMetaData]
    public class VersionInfo : DefaultVersionTableMetaData
    {
        public override string SchemaName => Tables.schema;
        public override string TableName => Tables.VersionInfo;
    }
}
