namespace GOWI.AIArticleGenerator.BackgroundTask.TestingLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using GOWI.AIArticleGenerator.DomainLayer.DTOs;
    using GOWI.AIArticleGenerator.ServiceLayer.Helper_classes;
    using GOWI.AIArticleGenerator.ServiceLayer.Interfaces;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class ServiceLayerTestSuite
    {
        private Mock<IOpenAIService> _mockOpenAIService;
        private Mock<IConverter> _mockConverter;

        [SetUp]
        public void SetUp()
        {
            _mockOpenAIService = new Mock<IOpenAIService>();
            _mockConverter = new Mock<IConverter>();
        }

        [TearDown]
        public void TearDown()
        {
            _mockOpenAIService = null;
            _mockConverter = null;
        }

        [Test]
        public async Task GenerateArticlesAsyncTest()
        {
            // Arrange
            string prompt = "Always create an article in this format:" +
                            "\r\n Article title \r\n" +
                            "Article short description: \r\n" +
                            "Article full description:\r\n" +
                            "For this article creation use provided data only!";

            var transaction = new DTOTransaction
            {
                TransactionId = 1,
                Name = "TransactionName",
                Value = Convert.ToDecimal(200.0000),
                Description = "This transaction is passed as an argument to GenerateArticlesAsync method!",
                DraftedOn = DateTime.Now,
                SelectedCurrency = "RSD",
                TrancheName = "TransactionTrancheName",
                TrancheValue = Convert.ToDecimal(200.0000),
                CompanyName = "Air Serbia",
            };

            // Act
            var actualResult = await _mockOpenAIService.Object.GenerateArticlesAsync(prompt, transaction);

            // Assert
            _mockOpenAIService.Verify(method => method.GenerateArticlesAsync(prompt, transaction), Times.AtLeastOnce);

        }

        [Test]
        public void SerializeToJSONTest()
        {
            // Arrange
            var transaction = new DTOTransaction
            {
                TransactionId = 1,
                Name = "TransactionName",
                Value = Convert.ToDecimal(200.0000),
                Description = "This transaction is passed as an argument to SerializeToJSON method!",
                DraftedOn = DateTime.Now,
                SelectedCurrency = "USD",
                TrancheName = "TransactionTrancheName",
                TrancheValue = Convert.ToDecimal(200.0000),
                CompanyName = "Japan Airlines",
            };

            _mockConverter.Setup(serializedObject => serializedObject.SerializeToJSON(transaction))
                    .Returns((DTOTransaction transaction) => JsonSerializer.Serialize(transaction));

            // Act
            var actualResult = _mockConverter.Object.SerializeToJSON(transaction);

            // Assert
            _mockConverter.Verify(method => method.SerializeToJSON(transaction), Times.AtLeastOnce);
            Assert.That(actualResult, Is.Not.Null);
            Assert.IsInstanceOf<string>(actualResult);
        }

        [Test]
        public void DeserializeToJSONTest()
        {
            // Arrange
            var response = new APIResponse
            {
                Id = "12345",
                Object = "someObject",
                Created = 1672531200,
                Model = "gpt-4",
                Choices = new List<Choice>
                {
                    new Choice
                    {
                        Text = "Option 1",
                        Index = 0,
                        Logprobs = null,
                        FinishReason = "stop"
                    },
                    new Choice
                    {
                        Text = "Option 2",
                        Index = 1,
                        Logprobs = null,
                        FinishReason = "length",
                    }
                }
            };

            var jsonResponse = JsonSerializer.Serialize(response);

            _mockConverter.Setup(deserializedObject => deserializedObject.DeserializeJSON(jsonResponse))
                    .Returns((string jsonResponse) => JsonSerializer.Deserialize<APIResponse>(jsonResponse));

            // Act
            var actualResult = _mockConverter.Object.DeserializeJSON(jsonResponse);

            // Assert            
            _mockConverter.Verify(method => method.DeserializeJSON(jsonResponse), Times.AtLeastOnce);
            Assert.That(actualResult, Is.Not.Null);
            Assert.IsInstanceOf<APIResponse>(actualResult);
        }
    }
}
