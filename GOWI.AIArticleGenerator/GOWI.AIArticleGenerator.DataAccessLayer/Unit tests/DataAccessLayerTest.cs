namespace GOWI.AIArticleGenerator.DataAccessLayer.Unit_tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GOWI.AIArticleGenerator.DataAccessLayer.Interfaces;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class DataAccessLayerTest
    {
        private Mock<IDataAccess> _dataAccessMock;

        [SetUp]
        public void SetUp()
        {
            _dataAccessMock = new Mock<IDataAccess>();
        }

        [TearDown]
        public void TearDown()
        {
            _dataAccessMock = null;
        }

        [Test]
        public async Task GetTransactionsTest()
        {
            // act
            var actualResult = await _dataAccessMock.Object.GetTransactions();

            // assert
            Assert.That(actualResult, Is.Not.Null);
        }
    }
}
