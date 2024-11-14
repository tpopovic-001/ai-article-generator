namespace GOWI.AIArticleGenerator.BackgroundTask.TestingLayer.DataAccessLayer.UnitTesting
{
    using GOWI.AIArticleGenerator.DataAccessLayer.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using GOWI.AIArticleGenerator.DomainLayer.DTOs;
    using GOWI.AIArticleGenerator.DataAccessLayer;

    [TestFixture]
    public class DataAccessLayerTest
    {
        private Mock<IDataAccess> _mockDataAccess;

        [SetUp]
        public void SetUp()
        {
            _mockDataAccess = new Mock<IDataAccess>();
        }

        [TearDown]
        public void TearDown()
        {
            _mockDataAccess = null;
        }

        [Test]
        public async Task GetTransactionsAsyncTest()
        {
            // Setup 
            var firstTransaction = new DTOTransaction
            {
                TransactionId = 1,
                Name = "FirstTransactionName",
                Value = Convert.ToDecimal(460.0000),
                Description = "Just testing a method for fetching database data",
                DraftedOn = DateTime.Now,
                SelectedCurrency= "USD",
                TrancheName = "SecondTransactionTrancheName",
                TrancheValue = Convert.ToDecimal(460.0000),
                CompanyName="Lufthansa",
            };

            var secondTransaction = new DTOTransaction
            {
                TransactionId = 2,
                Name = "SecondTransactionName",
                Value = Convert.ToDecimal(500.0000),
                Description = "Just testing a method for fetching database data",
                DraftedOn = DateTime.Now,
                SelectedCurrency = "USD",
                TrancheName = "SecondTransactionTrancheName",
                TrancheValue = Convert.ToDecimal(500.0000),
                CompanyName = "Fly Emirates",
            };

            List<DTOTransaction> transactions = new List<DTOTransaction>();
            transactions.Add(firstTransaction);
            transactions.Add(secondTransaction);
            var expectedResult = transactions;

            _mockDataAccess.Setup(dbData => dbData.GetTransactionsAsync()).ReturnsAsync(expectedResult);

            // Act
            var actualResult = await _mockDataAccess.Object.GetTransactionsAsync();

            // Assert
            Assert.That(actualResult, Is.Not.Null);
            Assert.That(actualResult,Is.EqualTo(expectedResult));
        }

        [Test]
        public async Task SaveFormattedArticlesAsyncTest()
        {
            // Setup
            var firstArticle = new DTOArticle
            {
                ArticleId = 1,
                Title = "TestForFirstArticle",
                ShortDescription = "This is just the test for first article.",
                FullDescription = "This test serves a purpose to verify do SaveFormatedArticlesAsync" +
                                  "method gets an argument of a correct type",
                TransactionId = 1,
            };

            var secondArticle = new DTOArticle
            {
                ArticleId = 2,
                Title = "TestForSecondArticle",
                ShortDescription = "This is just the test for second article.",
                FullDescription = "This test serves a purpose to verify do SaveFormatedArticlesAsync" +
                                  "method gets an argument of a correct type",
                TransactionId = 1,
            };
            List<DTOArticle> articles = new List<DTOArticle>();
            articles.Add(firstArticle);
            articles.Add(secondArticle);

            // Act
            await _mockDataAccess.Object.SaveFormattedArticlesAsync(articles);

            //Assert
            _mockDataAccess.Verify(method => method.SaveFormattedArticlesAsync(articles),Times.AtLeastOnce);

        }
    }
}
