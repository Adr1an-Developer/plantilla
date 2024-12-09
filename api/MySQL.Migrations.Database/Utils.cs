namespace MySQL.Migrations.Database
{
    public static class Utils
    {
        /// <summary>
        /// Returns the script Name using literal string <paramref name="version"/><br/>
        /// The result <strong>MUST</strong> be a valid .sql file with the returned name that's embedded through Migrations.csproj<br/><br/>
        /// <code>Example: string script = ReturnScriptNameByStringVersion("occurrence_data_source", "932217");<br/>Returns: "v932217_occurrence_data_source"</code><br/><br/>
        /// <code>Example: string script = ReturnScriptNameByStringVersion("occurrence_data_source", "999999_ABC");<br/>Returns: "v999999_ABC_occurrence_data_source"</code><br/><br/>
        /// <code>Example: string script = ReturnScriptNameByStringVersion("occurrence_data_source", "9_ABC");<br/>Returns: "v9_ABC_occurrence_data_source"</code>
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="version"></param>
        /// <returns>"v{<paramref name="version"/>}_{<paramref name="fileName"/>}"</returns>
        public static string ReturnScriptNameByStringVersion(string fileName, string version)
        {
            return $"v{ version }_{ fileName }";
        }

        /// <summary>
        /// Returns the script Name with <paramref name="version"/> formated as 000<br/>
        /// The result <strong>MUST</strong> be a valid .sql file with the returned name that's embedded through Migrations.csproj<br/><br/>
        /// <code>Example: string script = returnScriptNameVersion("occurrence_data_source", 1);<br/>Returns: "v001_occurrence_data_source"</code><br/><br/>
        /// <code>Example: string script = returnScriptNameVersion("occurrence_data_source", 20);<br/>Returns: "v020_occurrence_data_source"</code>
        /// <code>Example: string script = returnScriptNameVersion("occurrence_data_source", 999);<br/>Returns: "v999_occurrence_data_source"</code><br/><br/>
        /// <code>Example: string script = returnScriptNameVersion("occurrence_data_source", 9990);<br/>Returns: "v9990_occurrence_data_source"</code><br/><br/>
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="version"></param>
        /// <returns>"v{<paramref name="version"/>.ToString("000")}_{<paramref name="fileName"/>}"</returns>
        public static string returnScriptNameVersion(string fileName, int version)
        {
            return $"v{ version:000}_{ fileName }";
        }
    }
}
