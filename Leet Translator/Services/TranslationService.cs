using Leet_Translator.Data;
using Leet_Translator.Models;
using Leet_Translator.Services.Interfaces;
using Serilog;

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
            try
            {
                Log.Information("Retrieving translation records from the database.");
                return _dbContext.TranslationRecords.ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while retrieving translation records from the database.");
                throw;
            }
        }

        public IEnumerable<TranslationRecord> SearchRecords(string searchTerm)
        {
            return _dbContext.TranslationRecords
                .Where(record => record.InputText.Contains(searchTerm) || record.TranslatedText.Contains(searchTerm))
                .ToList();
        }

        public async Task<string> TranslateToLeetSpeak(string inputText)
        {
            try
            {
                Log.Information("Initiating translation to leetspeak for input text: {InputText}", inputText);

                string translatedText = await _funTranslationService.TranslateToLeetSpeak(inputText);

                Log.Information("Translation to leetspeak successful. Translated text: {TranslatedText}", translatedText);

                // Log database operation
                Log.Information("Adding translation record to the database.");

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
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while translating to leetspeak or saving translation record to the database.");
                throw;
            }
        }
    }
}
