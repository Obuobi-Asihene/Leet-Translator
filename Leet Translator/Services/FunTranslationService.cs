using Leet_Translator.Models;
using Leet_Translator.Services.Interfaces;
using Newtonsoft.Json;
using Serilog;

namespace Leet_Translator.Services
{
    public class FunTranslationService : IFunTranslationService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://api.funtranslations.com/translate/";

        public FunTranslationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> TranslateToLeetSpeak(string inputText)
        {
            try
            {
                Log.Information("Translation request initiated for input text: {InputText}", inputText);

                string endpoint = $"{BaseUrl}leet.json";
                var requestBody = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("text", inputText)
                });

                var response = await _httpClient.PostAsync(endpoint, requestBody);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var translationResponse = JsonConvert.DeserializeObject<FunTranslationResponse>(responseContent);

                    Log.Information("Translation successful. Translated text: {TranslatedText}", translationResponse.Contents.Translated);

                    return translationResponse.Contents.Translated;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();

                    Log.Error("Failed to translate. Error: {ErrorContent}", errorContent);

                    throw new Exception($"Failed to translate: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occured while translating");

                throw;
            }
        }
    }
}
