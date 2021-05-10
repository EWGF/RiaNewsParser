namespace RiaNewsParser
{
    public static class ParserSettings
    {
        //Default file saving paths and names
        public static string FileSavePath { get; set; } = @"C:\RiaNewsParser\Articles";
        public static string DBSavePath { get; set; } = @"C:\RiaNewsParser";
        public static string DBName { get; set; } = "riaArticlesDB";
        public static string DBTableName { get; set; } = "articles";

        //Tags for parsing 
        public static string ArticleTitleParseTag { get; set; } = "article__title";
        public static string ArticleTextParseTag { get; set; } = "article__text";
        public static string ArticlePublicationDateParseTag { get; set; } = "article__info-date";
        public static string ArticleLinksParseTag { get; set; } = "article__body js-mediator-article mia-analytics";
        public static string ArticleImagesParseTag { get; set; } = "photoview__open";

    }
}
