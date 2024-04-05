using Leet_Translator.Data;
using Leet_Translator.Models;
using Leet_Translator.Services;
using Leet_Translator.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Leet_Translator.Tests
{
    public class TranslationServiceTests
    {
        [Fact]
        public async Task GetTranslationRecords_ReturnsRecords()
        {
            // Arrange
            var userId = "user123";
            var testData = new List<TranslationRecord>
            {
                new TranslationRecord { Id = 1, InputText = "hello", TranslatedText = "h3ll0", UserId = userId },
                new TranslationRecord { Id = 2, InputText = "goodbye", TranslatedText = "900dbYe", UserId = userId }
            };

            var options = new DbContextOptionsBuilder<LeetTranslatorDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new LeetTranslatorDbContext(options))
            {
                dbContext.TranslationRecords.AddRange(testData);
                dbContext.SaveChanges();
            }

            using (var dbContext = new LeetTranslatorDbContext(options))
            {
                var translationService = new TranslationService(null, dbContext);

                // Act
                var result = await translationService.GetTranslationRecords(userId);

                // Assert
                Assert.Equal(2, result.Count());
            }
        }

        [Fact]
        public void SearchRecords_ReturnsFilteredRecords()
        {
            // Arrange
            var testData = new List<TranslationRecord>
            {
                new TranslationRecord { Id = 1, InputText = "hello", TranslatedText = "h3ll0", UserId = "user123" },
                new TranslationRecord { Id = 2, InputText = "goodbye", TranslatedText = "900dbYe", UserId = "user123" }
            };

            var options = new DbContextOptionsBuilder<LeetTranslatorDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new LeetTranslatorDbContext(options))
            {
                dbContext.TranslationRecords.AddRange(testData);
                dbContext.SaveChanges();
            }

            using (var dbContext = new LeetTranslatorDbContext(options))
            {
                var translationService = new TranslationService(null, dbContext);

                // Act
                var result = translationService.SearchRecords("hello", "user123");

                // Assert
                Assert.Single(result);
                Assert.Equal("hello", result.First().InputText);
            }
        }

        [Fact]
        public async Task TranslateToLeetSpeak_SavesTranslationRecord()
        {
            // Arrange
            var userId = "user123";
            var inputText = "hello";
            var translatedText = "h3ll0";

            var mockFunTranslationService = new Mock<IFunTranslationService>();
            mockFunTranslationService.Setup(m => m.TranslateToLeetSpeak(inputText)).ReturnsAsync(translatedText);

            // Use in-memory database
            var options = new DbContextOptionsBuilder<LeetTranslatorDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Initialize a real instance of LeetTranslatorDbContext with in-memory database
            using var dbContext = new LeetTranslatorDbContext(options);

            var translationService = new TranslationService(mockFunTranslationService.Object, dbContext);

            // Act
            var result = await translationService.TranslateToLeetSpeak(inputText, userId);

            // Assert
            var savedRecord = await dbContext.TranslationRecords.FirstOrDefaultAsync();
            Assert.NotNull(savedRecord);
            Assert.Equal(inputText, savedRecord.InputText);
            Assert.Equal(translatedText, savedRecord.TranslatedText);
            Assert.Equal(userId, savedRecord.UserId);
        }
    }
}
