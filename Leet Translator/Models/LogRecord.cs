namespace Leet_Translator.Models
{
    public class LogRecord
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
        public DateTime Timestamp { get; set; }
        public string InputText { get; set; }
        public string TranslatedText { get; set; }
    }
}
