using Newtonsoft.Json;
using RiaNewsParser.DataRepresentation;
using System.IO;
using System.Xml.Serialization;


namespace RiaNewsParser.FileOutputServices
{
    public class FileSaveHandler
    {
        public void SaveArticleXML(string path, Article article)
        {
            string fileNameXml = article.AtricleTitle.Replace(' ', '_') + ".xml";
            using (StreamWriter sw = new StreamWriter($"{path}/{fileNameXml}"))
            {
                var serrializer = new XmlSerializer(typeof(Article));
                serrializer.Serialize(sw, article);
            }
        }

        public void SaveArticleJSON (string path, Article article)
        {
            string fileNameJson = article.AtricleTitle.Replace(' ', '_') + ".json";
            string articleJson = JsonConvert.SerializeObject(article);
            using (StreamWriter sw = new StreamWriter($"{path}/{fileNameJson}"))
            {
                sw.Write(articleJson);
            }
        }
    }
}
