using Newtonsoft.Json;
using RiaNewsParser.DataRepresentation;
using System.IO;
using System.Xml.Serialization;


namespace RiaNewsParser.FileOutputServices
{
    public class ArticleSaveHandler
    {
        public ArticleSaveHandler(Article article, string basePath)
        {
            _article = article;
            _basePath = basePath;
            _articleName = GetFileName();
        }

        private Article _article;
        private string _articleName;
        private string _basePath;

        public void SaveArticle()
        {
            string savePath = _basePath + $"/{_articleName}";
            DirectoryChecker.CheckDirectory(savePath);
            SaveArticleXML(savePath);
            SaveArticleJSON(savePath);
        }

        private void SaveArticleXML(string path)
        {
            string fileNameXml = _articleName + ".xml";
            using (StreamWriter sw = new StreamWriter($"{path}/{fileNameXml}"))
            {
                var serrializer = new XmlSerializer(typeof(Article));
                serrializer.Serialize(sw, _article);
            }
        }

        private void SaveArticleJSON (string path)
        {
            string fileNameJson = _articleName + ".json";
            string articleJson = JsonConvert.SerializeObject(_article);
            using (StreamWriter sw = new StreamWriter($"{path}/{fileNameJson}"))
            {
                sw.Write(articleJson);
            }
        }

        private string GetFileName() => _article.AtricleTitle.Length < 70 ? _article.AtricleTitle.Replace(' ', '_') : _article.AtricleTitle.Remove(69).Replace(' ', '_');
    }
}
