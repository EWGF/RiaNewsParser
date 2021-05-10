using RiaNewsParser.DataRepresentation;
using RiaNewsParser.Services.FileOutputServices;
using System.Configuration;
using System.Data.SqlClient;

namespace RiaNewsParser.Services.DBServices
{
    class DBAcessHandler
    {
        /// <summary>
        /// Saves an article to data base. Creates the data base and table if necessary.
        /// </summary>
        public void SaveArticle(Article article)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
            DirectoryChecker.CheckDirectory(AppSettings.DBSavePath);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                SqlCommand checkDatabase = new SqlCommand(CreateDatabaseIfNotExists(), connection);
                checkDatabase.ExecuteNonQuery();

                connection.ChangeDatabase(AppSettings.DBName);

                SqlCommand checkTable = new SqlCommand(CreateTableIfNotExists(), connection);
                checkTable.ExecuteNonQuery();

                SqlCommand addArticle = new SqlCommand(InsertArticle(article), connection);
                addArticle.ExecuteNonQuery();
                
                connection.Close();
            }
        }

        /// <summary>
        /// Returns a MS-SQL querry to check if database exists. Creates a new DB if not. DB name is based on ParserSettings.
        /// </summary>
        private string CreateDatabaseIfNotExists() =>
            $"If(db_id(N'{AppSettings.DBName}') IS NULL) CREATE DATABASE [{AppSettings.DBName}] ON ( NAME = {AppSettings.DBName}, FILENAME = '{AppSettings.DBSavePath}\\{AppSettings.DBName}.mdf') COLLATE Cyrillic_General_CI_AI";

        /// <summary>
        /// Returns a MS-SQL querry to check if table exists. Creates a new table if not. Table name is based on ParserSettings.
        /// </summary>
        private string CreateTableIfNotExists() => $"IF  NOT EXISTS (SELECT * FROM sys.tables WHERE name = N'{AppSettings.DBTableName}' AND type = 'U')" +
                                                                           $"BEGIN " +
                                                                           $"CREATE TABLE {AppSettings.DBTableName}" +
                                                                           $"(" +
                                                                                $"Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL," +
                                                                                $"ArticleTitle text NOT NULL," +
                                                                                $"ArticleText  text NOT NULL,"+
                                                                                $"ArticlePubDate text NOT NULL," +
                                                                                $"ArticleLinksCount INT," +
                                                                                $"ArticleImagesCount INT" +
                                                                           $")" +
                                                                           $"END";

        /// <summary>
        /// Returns a MS-SQL querry to insert article into table
        /// </summary>
        private string InsertArticle(Article article) =>  $"INSERT INTO {AppSettings.DBTableName}(ArticleTitle, ArticleText, ArticlePubDate, ArticleLinksCount, ArticleImagesCount)" +
                                                          $" VALUES('" +
                                                                        $"{article?.AtricleTitle}'," +
                                                                        $"'{article?.ArticleText}'," +
                                                                        $"'{article?.PublicationDate}'," +
                                                                        $"'{article?.ArticleLinks.Count}'," +
                                                                        $"'{article?.ArticleImages.Count}'" +
                                                                   $")";

    }
}
