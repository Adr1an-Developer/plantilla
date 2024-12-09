using FluentMigrator.Builders.Execute;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MySQL.Migrations.Database.FluentObjects
{
    internal class IndexBuilder
    {

        private string tableName;
        private IList<string> fields = new List<string>();
        private IList<string> includes = new List<string>();

        private IExecuteExpressionRoot expression;
        private string condition = null;

        internal IndexBuilder(IExecuteExpressionRoot expression, string tableName)
        {
            this.expression = expression;
            this.tableName = tableName;
        }


        public void Create()
        {
            string sql = BuildSqlCreate();

            if (!string.IsNullOrEmpty(condition))
                sql = CreateConditionalBlock(sql, condition);

            this.expression.Sql(sql);
        }

        public void Drop()
        {
            string indexName = GetIndexName();
            string sql = $"DROP INDEX IF EXISTS compusafe.{indexName};";

            if (!string.IsNullOrEmpty(condition))
                sql = CreateConditionalBlock(sql, condition);

            this.expression.Sql(sql);
        }

        public IndexBuilder SetCondition(string condition)
        {
            this.condition = condition;
            return this;
        }

        public IndexBuilder AddField(string field)
        {
            this.fields.Add(field);
            return this;
        }


        public IndexBuilder AddInclude(string include)
        {
            this.includes.Add(include);
            return this;
        }


        private string BuildSqlCreate()
        {
            string indexName = GetIndexName();
            string stringFields = string.Join(',', this.fields);

            string sql =
$"CREATE INDEX IF NOT EXISTS {indexName} ON default.{tableName} USING btree ({stringFields})";

            if(this.includes.Count > 0)
            {
                string stringIncludes = string.Join(',', this.includes);
                sql += $" INCLUDE ({stringIncludes})";
            }

            sql += ";";

            return sql;
        }




        private string GetIndexName()
        {
            const int maxLengthByField = 4;

            StringBuilder sb = new StringBuilder();

            sb.Append(this.tableName);
            PutListConcatenation(ref sb, this.fields, maxLengthByField);

            if (this.includes.Count > 0)
            {
                sb.Append("_with");
                PutListConcatenation(ref sb, this.includes, maxLengthByField);
            }

            sb.Append("_idx");

            return sb.ToString();
        }

        private void PutListConcatenation(ref StringBuilder sb, IList<string> collection, int maxLengthByField)
        {
            foreach (string name in collection)
            {
                IEnumerable<string> resumeList = name.Split('_').Select(a => new string(a.Take(maxLengthByField).ToArray()));
                string suffixField = string.Join("_", resumeList);

                sb.Append("_");
                sb.Append(suffixField);
            }
        }


        private string CreateConditionalBlock(string command, string condition)
        {
            string block = $@"
DO $$
BEGIN

    if {condition} THEN

        {command}

    end if;
END $$;";

            return block;
        }


    }
}
