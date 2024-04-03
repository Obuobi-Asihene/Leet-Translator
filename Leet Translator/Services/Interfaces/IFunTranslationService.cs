namespace Leet_Translator.Services.Interfaces
{
    public interface IFunTranslationService
    {
        Task<string> TranslateToLeetSpeak(string inputText);
    }
}
