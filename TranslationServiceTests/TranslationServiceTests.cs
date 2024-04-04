using Xunit;
using Leet_Translator.Services;
using Leet_Translator.Services.Interfaces;
using Leet_Translator.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Leet_Translator.Data;

namespace Leet_Translator.Tests
{
    public class TranslationServiceTests
    {
        [Fact]
        public async Task TranslateToLeetSpeak_ShouldReturnTranslatedText()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<LeetTranslatorDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Seed the in-memory database with test data
            using (var context = new LeetTranslatorDbContext(options))
            {
                // Ensure that the required properties are provided
                var translationRecord = new TranslationRecord
                {
                    InputText = "Hello",
                    TranslatedText = "H3ll0" // Provide the TranslatedText
                };
                context.TranslationRecords.Add(translationRecord);
                context.SaveChanges();
            }

            // Create an instance of the TranslationService with a real DbContext
            using (var context = new LeetTranslatorDbContext(options))
            {
                var translationService = new TranslationService(new Mock<IFunTranslationService>().Object, context);

                // Act
                var translatedText = await translationService.TranslateToLeetSpeak("Hello");

                // Assert
                Assert.Equal("H3ll0", translatedText);
            }
        }
    }
}
