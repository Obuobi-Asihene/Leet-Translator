namespace Leet_Translator.Models
{
    public class TranslationRecord
    {
        public int Id { get; set; }
        public string InputText { get; set; }
        public string TranslatedText { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
