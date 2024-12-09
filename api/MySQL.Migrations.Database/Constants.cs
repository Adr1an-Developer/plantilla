namespace MySQL.Migrations.Database
{
    internal static class DataType
    {
        internal const string citext = "citext";
    }

    internal static class Column
    {
        internal const string Id = "id";
        internal const string Name = "name";
        internal const string IsActive = "is_active";
        internal const string IsDeleted = "is_deleted";
        internal const string CreationDate = "creation_date";
        internal const string ModificationDate = "modification_date";
        internal static string CreateByUser = "create_by_user";
        internal static string UpdateByUser = "update_by_user";
        internal static string PostalCode = "postal_code";

        internal class Lenguage
        {
            internal const string Abbreviation = "abbreviation";
        }

        internal class Country
        {
            internal static string CountryCode = "country_code";
            internal static string Abbreviation = "abbreviation";
            internal static string NumericCode = "numeric_code";
            internal static string PhoneCode = "phone_code";
            internal static string Capital = "capital";
            internal static string Currency = "currency";
            internal static string CurrencyName = "currency_name";
            internal static string CurrencySymbol = "currency_symbol";
            internal static string Region = "region";
            internal static string SubRegion = "subregion";
            internal static string Latitude = "latitude";
            internal static string Longitude = "longitude";
            internal static string Emoji = "emoji";
            internal static string EmojiU = "emojiU";
        }

        internal class CountryTimezone
        {
            internal static string CountryCode = "country_code";
            internal static string Abbreviation = "abbreviation";
            internal static string ZoneName = "zone_name";
            internal static string GmtOffset = "gmtOffset";
            internal static string GmtOffsetName = "gmtOffsetName";
            internal static string TzName = "tzname";
        }

        internal class CountryTranslations
        {
            internal static string CountryCode = "country_code";
            internal static string pt = "pt";
            internal static string es = "es";
        }

        internal class State
        {
            internal static string CountryCode = "country_code";
            internal static string StateCode = "state_code";
            internal static string Abbreviation = "abbreviation";
            internal static string Latitude = "latitude";
            internal static string Longitude = "longitude";
        }

        internal class City
        {
            internal static string CityCode = "city_code";
            internal static string StateCode = "state_code";
            internal static string Latitude = "latitude";
            internal static string Longitude = "longitude";
        }

        internal class SystemMenu
        {
            internal static string Title = "menu_title";
            internal static string ParentMenuID = "parent_menu_id";
            internal static string Url = "menu_url";
            internal static string Icon = "menu_icon";
            internal static string Order = "menu_order";
            internal static string Group = "menu_group";
        }

        internal class MenuPermissions
        {
            internal static string MenuId = "menu_id";
            internal static string ProfileId = "profile_id";
            internal static string CanView = "can_view";
            internal static string CanAdd = "can_add";
            internal static string CanEdit = "can_edit";
            internal static string CanDelete = "can_delete";
            internal static string CanExport = "can_export";
            internal static string CanAuthorize = "can_authorize";
        }

        internal class Profile
        {
            internal static string Name = "name";
            internal static string Abbreviation = "abbreviation";
        }

        internal static class Users
        {
            internal static string UserName = "user_name";
            internal static string Password = "password";
            internal static string ProfileId = "profile_id";
            internal static string FirstName = "first_name";
            internal static string LastName = "last_name";
            internal static string Email = "email";
            internal static string IsFirstLogin = "is_first_login";
            internal const string ExternalCode = "external_code";
        }

        internal static class UserDetail
        {
            internal static string UserId = "user_id";
            internal static string LanguageId = "language_id";
            internal static string CityId = "city_id";
            internal static string Address = "address";
            internal static string Telefone = "telefone";
        }

        internal static class ExceptionLog
        {
            internal const string LogLevel = "log_level";
            internal const string Message = "message";
            internal const string StackTrace = "stack_trace";
            internal const string UserId = "user_id";
            internal const string ServiceName = "service_name";
        }

        internal static class Client
        {
            internal const string FirstName = "first_name";
            internal const string LastName = "last_name";
            internal const string CompanyName = "company_name";
            internal const string Email = "email";
            internal const string Phone = "phone";
            internal const string Address = "address";
            internal const string CityId = "city_id";
            internal const string PostalCode = "postal_code";
            internal const string Status = "status";
            internal const string UseClientTypeIdrId = "client_type_id";
            internal const string RegisterId = "register_id";
            internal const string Notes = "notes";
        }

        /// <summary>
        /// Format foreign key
        /// </summary>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        internal static string FormatFK(string table, string column)
        {
            return $"{table}_{column}";
        }

        /// <summary>
        /// Format Unique Key
        /// </summary>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        internal static string FormatUniqueKey(string table)
        {
            return $"uk_{table}";
        }

        /// <summary>
        /// Format composite Key
        /// </summary>
        /// <param name="table"></param>
        /// <param name="column1"></param>
        /// <param name="column2"></param>
        /// <returns></returns>
        internal static string FormatPKComposite(string table, string column1, string column2)
        {
            return $"pk_{table}_{column1}_{column2}";
        }

        /// <summary>
        /// Format composite Key
        /// </summary>
        /// <param name="table"></param>
        /// <param name="column1"></param>
        /// <param name="column2"></param>
        /// <param name="column3"></param>
        /// <returns></returns>
        internal static string FormatPKComposite(string table, string column1, string column2, string column3)
        {
            return $"pk_{table}_{column1}_{column2}_{column3}";
        }
    }

    internal static class Tables
    {
        internal const string VersionInfo = "version_info";
        internal const string schema = "default";
        internal const string schemaPublic = "public";
        internal const string Language = "language";
        internal const string Profiles = "profiles";
        internal const string User = "user";
        internal const string UserDetail = "user_detail";
        internal const string Country = "country";
        internal const string CountryTimezone = "country_timezone";
        internal const string CountryTranslations = "country_translations";
        internal const string State = "state";
        internal const string City = "city";
        internal const string SystemMenu = "system_menu";
        internal const string MenuPermissions = "menu_permissions";
        internal const string ExceptionLog = "exception_log";
        internal const string Client = "client";

        /// <summary>
        /// fk_my_table_my_column
        /// </summary>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        internal static string ForeignKey(string table, string column)
        {
            return $"fk_{table}_{column}";
        }

        /// <summary>
        /// ck_my_table_my_column_validation
        /// </summary>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <param name="validation"></param>
        /// <returns></returns>
        ///
        internal static string Check(string table, string column, string validation)
        {
            return $"ck_{table}_{column}_{validation}";
        }

        /// <summary>
        /// df_my_table_my_column
        /// </summary>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        internal static string Default(string table, string column)
        {
            return $"df_{table}_{column}";
        }

        /// <summary>
        /// pk_my_table
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        internal static string PrimaryKey(string table)
        {
            return $"pk_{table}";
        }

        /// <summary>
        /// idx_my_table_my_column
        /// </summary>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        internal static string Index(string table, string column)
        {
            return $"idx_{table}_{column}";
        }

        /// <summary>
        /// Restart PK value to 1
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        internal static string RestartPK(string schema, string table)
        {
            return $"ALTER SEQUENCE {schema}.{table}_id_seq RESTART WITH 1;";
        }
    }
}