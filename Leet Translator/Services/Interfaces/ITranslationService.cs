using Leet_Translator.Models;
using Microsoft.EntityFrameworkCore;

namespace Leet_Translator.Services.Interfaces
{
    public interface ITranslationService
    {
        Task<string> TranslateToLeetSpeak(string inputText, string userId);
        Task<IEnumerable<TranslationRecord>> GetTranslationRecords(string userId);

        IEnumerable<TranslationRecord> SearchRecords(string searchTerm);
    }
}
