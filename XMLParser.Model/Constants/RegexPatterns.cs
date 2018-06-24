namespace XMLParser.Model.Constants {
    public class RegexPatterns {
        public const string XmlNodePattern = "<\\s*(\\w+).*?<\\s*\\/\\s*\\1>";
        public const string XmlCloseTagPattern = "<[/].*?>";
        public const string XmlOpenTagPattern = "<(?![/]).*?>";
    }
}