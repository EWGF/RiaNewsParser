using RiaNewsParser.DataRepresentation;
using RiaNewsParser.FileOutputServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiaNewsParser.DBServices
{
    class DBAcessHandler
    {
        public void SaveArticle(Article article)
        {
            //Console.WriteLine(Encoding.GetEncoding(article.ArticleText));
            string connectionString = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
            DirectoryChecker.CheckDirectory(ParserSettings.DBSavePath);

            CreateDatabaseIfNotExists(connectionString, ParserSettings.DBName);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.ChangeDatabase(ParserSettings.DBName);

                SqlCommand checkTable = new SqlCommand(CreateTableIfNotExists(), connection);
                checkTable.ExecuteNonQuery();

                SqlCommand addArticle = new SqlCommand(InsertArticle(article), connection);
                addArticle.ExecuteNonQuery();
                
                connection.Close();
                Console.WriteLine("Connection closed");
            }
        }

        private void CreateDatabaseIfNotExists(string connectionString, string dbName)
        {
            SqlCommand cmd = null;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (cmd = new SqlCommand($"If(db_id(N'{dbName}') IS NULL) CREATE DATABASE [{dbName}] ON ( NAME = {dbName}, FILENAME = '{ParserSettings.DBSavePath}\\{dbName}.mdf') COLLATE Cyrillic_General_CI_AI", connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private string CreateTableIfNotExists() => $"IF  NOT EXISTS (SELECT * FROM sys.tables WHERE name = N'{ParserSettings.DBTableName}' AND type = 'U')" +
                                                                           $"BEGIN " +
                                                                           $"CREATE TABLE {ParserSettings.DBTableName}" +
                                                                           $"(" +
                                                                                $"Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL," +
                                                                                $"ArticleTitle text NOT NULL," +
                                                                                $"ArticleText  text NOT NULL,"+
                                                                                $"ArticlePubDate text NOT NULL," +
                                                                                $"ArticleLinksCount INT," +
                                                                                $"ArticleImagesCount INT" +
                                                                           $")" +
                                                                           $"END";

        private string InsertArticle(Article article) =>  $"INSERT INTO {ParserSettings.DBTableName}(ArticleTitle, ArticleText, ArticlePubDate, ArticleLinksCount, ArticleImagesCount)" +
                                                          $" VALUES('" +
                                                                        $"{article.AtricleTitle}'," +
                                                                        $"'{article.ArticleText}'," +
                                                                        $"'{article.PublicationDate}'," +
                                                                        $"'{article.ArticleLinks.Count}'," +
                                                                        $"'{article.ArticleImages.Count}'" +
                                                                   $")";

    }
}
