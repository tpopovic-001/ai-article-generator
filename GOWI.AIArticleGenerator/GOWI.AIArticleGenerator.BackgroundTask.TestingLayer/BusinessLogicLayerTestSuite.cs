namespace GOWI.AIArticleGenerator.BackgroundTask.TestingLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using GOWI.AIArticleGenerator.BusinessLogicLayer.Interfaces;
    using System.Runtime.CompilerServices;
    using GOWI.AIArticleGenerator.ServiceLayer.Helper_classes;
    using GOWI.AIArticleGenerator.DomainLayer.DTOs;

    [TestFixture]
    public class BusinessLogicLayerTestSuite
    {
        private Mock<IBusinessLogic> _mockBusinessLogic;

        [SetUp]
        public void Setup()
        {
            _mockBusinessLogic = new Mock<IBusinessLogic>();
        }

        [TearDown]
        public void Teardown()
        {
            _mockBusinessLogic = null;
        }

        [Test]
        public async Task GetArticlesAsyncTest()
        {
            // Arrange
            var firstChoice = new Choice
            {
                Text = "Sample text",
                Index = 1,
                Logprobs = new { Probability = 0.95 },
                FinishReason = "Stop",
            };

            var secondChoice = new Choice
            {
                Text = "Sample text",
                Index = 2,
                Logprobs = new { Probability = 0.97 },
                FinishReason = "Stop",
            };

            List<Choice> choices = new List<Choice>();
            choices.Add(firstChoice);
            choices.Add(secondChoice);
            var expectedResult = choices;

            _mockBusinessLogic.Setup(articles => articles.GetArticlesAsync())
                .ReturnsAsync(expectedResult);

            // Act
            var actualResult = await _mockBusinessLogic.Object.GetArticlesAsync();

            // Assert
            _mockBusinessLogic.Verify(method => method.GetArticlesAsync(), Times.AtLeastOnce());
            Assert.That(actualResult, Is.Not.Null);
            Assert.That(actualResult, Is.InstanceOf<List<Choice>>());
        }

        [Test]
        public void FormatArticlesTest()
        {
            // Arrange
            var firstChoice = new Choice
            {
                Text = "Sample text",
                Index = 1,
                Logprobs = new { Probability = 0.95 },
                FinishReason = "Stop",
                TransactionId = 1,
            };

            var secondChoice = new Choice
            {
                Text = "Sample text",
                Index = 2,
                Logprobs = new { Probability = 0.97 },
                FinishReason = "Stop",
                TransactionId= 2,
            };

            List<Choice> choices = new List<Choice>();
            choices.Add(firstChoice);
            choices.Add(secondChoice);

            var firstArticle = new DTOArticle
            {
                ArticleId = 1,
                Title="FirstArticle",
                ShortDescription= "Object with a formatted article data",
                FullDescription= "This object is returned with other objects of type DTOArticle in form of a list",
                TransactionId = 1,
            };

            var secondArticle = new DTOArticle
            {
                ArticleId = 2,
                Title = "FirstArticle",
                ShortDescription = "Object with a formatted article data",
                FullDescription = "This object is returned with other objects of type DTOArticle in form of a list",
                TransactionId = 2,
            };

            var formattedArticles = new List<DTOArticle>();
            formattedArticles.Add(firstArticle);
            formattedArticles.Add(secondArticle);

            _mockBusinessLogic.Setup(method => method.FormatArticles(choices)).Returns(formattedArticles);

            // Act
            var actualResult = _mockBusinessLogic.Object.FormatArticles(choices);

            // Assert
            _mockBusinessLogic.Verify(method => method.FormatArticles(choices), Times.AtLeastOnce);
            Assert.That(actualResult, Is.Not.Null);
            Assert.That(actualResult, Is.InstanceOf<List<DTOArticle>>());
        }

        [Test]
        public async Task SaveFormattedArticlesAsyncTest()
        {
            // Arrange
            var firstArticle = new DTOArticle
            {
                ArticleId = 1,
                Title = "FirstArticle",
                ShortDescription = "Object with a formatted article data",
                FullDescription = "This object is returned with other objects of type DTOArticle in form of a list",
                TransactionId = 1,
            };

            var secondArticle = new DTOArticle
            {
                ArticleId = 2,
                Title = "FirstArticle",
                ShortDescription = "Object with a formatted article data",
                FullDescription = "This object is returned with other objects of type DTOArticle in form of a list",
                TransactionId = 2,
            };

            var formattedArticles = new List<DTOArticle>();
            formattedArticles.Add(firstArticle);
            formattedArticles.Add(secondArticle);

            // Act
             await _mockBusinessLogic.Object.SaveFormattedArticlesAsync(formattedArticles);

            // Assert
            _mockBusinessLogic.Verify(method => method.SaveFormattedArticlesAsync(formattedArticles),
                                                                                    Times.AtLeastOnce);
        }
    }
}
