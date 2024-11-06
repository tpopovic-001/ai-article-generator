namespace GOWI.AIArticleGenerator.BusinessLogicLayer.Unit_tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GOWI.AIArticleGenerator.BusinessLogicLayer.Interfaces;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class BusinessLogicLayerTest
    {
        private Mock<IBusinessLogic> _businessLogicMock;

        [SetUp]
        public void SetUp()
        {
            _businessLogicMock = new Mock<IBusinessLogic>();
        }

        [TearDown]
        public void TearDown()
        {
            _businessLogicMock = null;
        }

        [Test]
        public async Task GetArticlesTest()
        {
            // act
            var actualResult = await _businessLogicMock.Object.GetArticles();

            // assert
            Assert.That(actualResult, Is.TypeOf<Task<string>>());
        }
    }
}
