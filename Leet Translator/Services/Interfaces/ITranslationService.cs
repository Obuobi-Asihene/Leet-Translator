using Leet_Translator.Models;

namespace Leet_Translator.Services.Interfaces
{
    public interface ITranslationService
    {
        Task<string> TranslateToLeetSpeak(string inputText);
        IEnumerable<TranslationRecord> GetTranslationRecords();
    }
}
