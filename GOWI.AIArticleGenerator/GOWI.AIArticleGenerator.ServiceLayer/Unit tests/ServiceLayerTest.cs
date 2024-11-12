namespace GOWI.AIArticleGenerator.ServiceLayer.Unit_tests
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
    public class ServiceLayerTest
    {
        private Mock<IConverter> _converterMock;
        private Mock<IOpenAIService> _openAIServiceMock;

        [SetUp]
        public void SetUp()
        {
            _converterMock = new Mock<IConverter>();
            _openAIServiceMock= new Mock<IOpenAIService>();
        }

        [TearDown]
        public void TearDown()
        {
            _converterMock = null;
            _openAIServiceMock = null;
        }

        [Test]
        public async Task SerializeToJSONTest()
        {
            var transactionObject = new DTOTransaction
            {
                Name = "Air Serbia |11-01|$1.2m",
                Value = Convert.ToDecimal(1200.00000),
                Description = "<p>No description</p>",
                DraftedOn = DateTime.Now,
                SelectedCurrency = "USD",
                TrancheName = "$1.200.000 1.875% Senior Notes due 2025",
                TrancheValue = Convert.ToDecimal(1200.00000),
                CompanyName = "Barclays",
            };

            // act
            var actualResult =  _converterMock.Object.
                                        SerializeToJSON(transactionObject);

            // assert
            Assert.That(actualResult, Is.InstanceOf<Task<string>>());
        }

        [Test]
        public async Task GenerateArticleTest()
        {
            var transactionObject = new DTOTransaction
            {
                Name = "Air Serbia |11-01|$1.2m",
                Value = Convert.ToDecimal(1200.00000),
                Description = "<p>No description</p>",
                DraftedOn = DateTime.Now,
                SelectedCurrency = "USD",
                TrancheName = "$1.200.000 1.875% Senior Notes due 2025",
                TrancheValue = Convert.ToDecimal(1200.00000),
                CompanyName = "Barclays",
            };

            string testPrompt = "You are professional journalist. Make an article in format: title," +
                    "short description, full description. Here's the data to work with: ";

            // act
            var actualResult = await _openAIServiceMock.Object.
                                GenerateArticle(testPrompt, transactionObject);

            // assert
            Assert.That(actualResult, Is.InstanceOf<Task<APIResponse>>());
        }
    }
}
