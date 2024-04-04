using Leet_Translator.Models;
using Microsoft.EntityFrameworkCore;

namespace Leet_Translator.Services.Interfaces
{
    public interface ITranslationService
    {
        Task<string> TranslateToLeetSpeak(string inputText);
        Task<IEnumerable<TranslationRecord>> GetTranslationRecords();

        IEnumerable<TranslationRecord> SearchRecords(string searchTerm);
    }
}
