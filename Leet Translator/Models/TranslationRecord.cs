using Leet_Translator.Areas.Identity.Data;

namespace Leet_Translator.Models
{
    public class TranslationRecord
    {
        public int Id { get; set; }
        public string InputText { get; set; }
        public string TranslatedText { get; set; }
        public DateTime TimeStamp { get; set; }

        public string UserId { get; set; }
        public AppUser appUser { get; set; }
    }
}
