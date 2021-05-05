namespace RiaNewsParser.DataRepresentation
{
    public class Links
    {
        public string LinkName { get; set; }
        public string LinkUrl { get; set; }

        public override string ToString()
        {
            return $"{LinkName} {LinkUrl}";
        }
    }
}
