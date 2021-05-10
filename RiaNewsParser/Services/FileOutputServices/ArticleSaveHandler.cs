using Newtonsoft.Json;
using RiaNewsParser.DataRepresentation;
using System;
using System.IO;
using System.Xml.Serialization;


namespace RiaNewsParser.Services.FileOutputServices
{
    /// <summary>
    /// Handles the article saving 
    /// </summary>
    public class ArticleSaveHandler
    {
        public ArticleSaveHandler(Article article, string basePath)
        {
            _article = article;
            _basePath = basePath;
        }

        private Article _article;
        private string _articleName;
        private string _basePath;

        /// <summary>
        /// Saving the article converted both to XML and JSON formats
        /// </summary>
        public void SaveArticle()
        {
            if(_article is null)
            {
                throw new ArgumentNullException("Article is null", new Exception("Cannot find the parsed article."));
            }

            _articleName = GenerateArticleName();
            string savePath = _basePath + $"/{_articleName}";
            DirectoryChecker.CheckDirectory(savePath);
            SaveArticleXML(savePath);
            SaveArticleJSON(savePath);
        }

        /// <summary>
        /// Converts article to XML and saves it to following path
        /// </summary>
        private void SaveArticleXML(string path)
        {
            string fileNameXml = _articleName + ".xml";
            using (StreamWriter sw = new StreamWriter($"{path}/{fileNameXml}"))
            {
                var serrializer = new XmlSerializer(typeof(Article));
                serrializer.Serialize(sw, _article);
            }
        }

        /// <summary>
        /// Converts article to JSON and saves it to following path
        /// </summary>
        private void SaveArticleJSON (string path)
        {
            string fileNameJson = _articleName + ".json";
            string articleJson = JsonConvert.SerializeObject(_article);
            using (StreamWriter sw = new StreamWriter($"{path}/{fileNameJson}"))
            {
                sw.Write(articleJson);
            }
        }

        /// <summary>
        /// Generates the file name based on article title
        /// </summary>
        private string GenerateArticleName() => _article.AtricleTitle.Length < 70 ? _article.AtricleTitle.Replace(' ', '_') : _article.AtricleTitle.Remove(69).Replace(' ', '_');
    }
}
