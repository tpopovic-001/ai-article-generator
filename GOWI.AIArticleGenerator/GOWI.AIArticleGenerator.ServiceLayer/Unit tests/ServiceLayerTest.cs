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
            var transactionObjectOne = new DTOTransaction
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

            var transactionObjectTwo = new DTOTransaction
            {
                Name = "Air Serbia |11-05|$4m",
                Value = Convert.ToDecimal(400.00000),
                Description = "<p>No description</p>",
                DraftedOn = DateTime.Now,
                SelectedCurrency = "USD",
                TrancheName = "$400.000.000 3.375% Senior Notes due 2026",
                TrancheValue = Convert.ToDecimal(1200.00000),
                CompanyName = "Barclays",
            };

            var testList = new List<DTOTransaction>();
            testList.Add(transactionObjectOne);
            testList.Add(transactionObjectTwo);

            // act
            var actualResult = await _converterMock.Object.SerializeToJSON(testList);

            // assert
            Assert.That(actualResult, Is.InstanceOf<Task<string>>());
        }

        [Test]
        public async Task DeserializeJSONTest()
        {
            var choiceOne = new Choice
            {
                Text = "answer 1",
                Index = 0,
                Logprobs = null,
                FinishReason = "stop",
            };

            var choiceTwo = new Choice
            {
                Text = "answer 2",
                Index = 1,
                Logprobs = null,
                FinishReason = "length",
            };

            var testList = new List<Choice>();
            testList.Add(choiceOne);
            testList.Add(choiceTwo);
            var testJSONResponse = JsonSerializer.Serialize(testList);

            // act
            var actualResult = await _converterMock.Object.DeserializeJSON(testJSONResponse);

            // assert
            Assert.That(actualResult, Is.InstanceOf<Task<APIResponse>>());
        }

        [Test]
        public async Task GenerateArticlesTest()
        {
            var transactionObjectOne = new DTOTransaction
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

            var transactionObjectTwo = new DTOTransaction
            {
                Name = "Air Serbia |11-05|$4m",
                Value = Convert.ToDecimal(400.00000),
                Description = "<p>No description</p>",
                DraftedOn = DateTime.Now,
                SelectedCurrency = "USD",
                TrancheName = "$400.000.000 3.375% Senior Notes due 2026",
                TrancheValue = Convert.ToDecimal(1200.00000),
                CompanyName = "Barclays",
            };

            var testList = new List<DTOTransaction>();
            testList.Add(transactionObjectOne);
            testList.Add(transactionObjectTwo);
            var testPrompt = "Based on this JSON transaction data,I want you to create articles with " +
                "Article title, short description and Full description for each object.";

            // act
            var actualResult = _openAIServiceMock.Object.GenerateArticles(testPrompt, testList);

            // assert
            Assert.That(actualResult, Is.InstanceOf<Task<string>>());
        }
    }
}
