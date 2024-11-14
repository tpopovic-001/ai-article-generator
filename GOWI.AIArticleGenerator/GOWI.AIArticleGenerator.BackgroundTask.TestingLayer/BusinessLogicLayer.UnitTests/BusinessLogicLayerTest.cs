namespace GOWI.AIArticleGenerator.BackgroundTask.TestingLayer.BusinessLogicLayer.UnitTesting
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

    [TestFixture]
    public class BusinessLogicLayerTest
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
            // Setup

            // Act

            // Assert

        }

        [Test]
        public void FormatArticlesTest()
        {
            // Setup

            // Act

            // Assert
        }

        [Test]
        public async Task SaveFormattedArticlesAsyncTest()
        {
            // Setup

            // Act

            // Assert
        }
    }
}
