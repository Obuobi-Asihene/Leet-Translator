using Leet_Translator.Data;
using Leet_Translator.Models;
using Leet_Translator.Services.Interfaces;

namespace Leet_Translator.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly IFunTranslationService _funTranslationService;
        private readonly LeetTranslatorDbContext _dbContext;

        public TranslationService(IFunTranslationService funTranslationService, LeetTranslatorDbContext dbContext)
        {
            _funTranslationService = funTranslationService;
            _dbContext = dbContext;
        }

        public IEnumerable<TranslationRecord> GetTranslationRecords()
        {
            return _dbContext.TranslationRecords.ToList();
        }

        public async Task<string> TranslateToLeetSpeak(string inputText)
        {
            string translatedText = await _funTranslationService.TranslateToLeetSpeak(inputText);

            //logging API call & result to DB
            var translationRecord = new TranslationRecord
            {
                InputText = inputText,
                TranslatedText = translatedText,
                TimeStamp = DateTime.UtcNow
            };

            _dbContext.TranslationRecords.Add(translationRecord);
            await _dbContext.SaveChangesAsync();

            return translatedText;
        }
    }
}
