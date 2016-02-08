using System;
using System.Collections.Generic;
using System.Linq;
using Domain.DAL;
using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service.Entity;

namespace ServiceTests
{
    [TestClass]
    public class OwnershipServiceTests
    {
        private Mock<IRepository> _mockrepository;

        [TestInitialize]
        public void Initialize()
        {
            _mockrepository = new Mock<IRepository>();
        }
        [TestMethod]
        public void WhenGettingNetIncome_ReturnsCorrectAmount()
        {
            //Arrange
            _mockrepository.Setup(i => i.Get<Ownership>(It.IsAny<int>(), false)).Returns(new Ownership());
            var incoming = new List<Transaction>
            {
                new Transaction
                {
                    Amount = 100
                },
                new Transaction
                {
                    Amount = 200
                },
                new Transaction
                {
                    Amount = 300
                },
                new Transaction
                {
                    Amount = 400
                }
            };
            var outgoing = new List<Transaction>
            {
                new Transaction
                {
                    Amount = 100
                },
                new Transaction
                {
                    Amount = 200
                },
                new Transaction
                {
                    Amount = 300
                }
            };
            _mockrepository.SetupSequence(i => i.GetAll(It.IsAny<Func<IQueryable<Transaction>,IEnumerable<Transaction>>>()))
                .Returns(incoming)
                .Returns(outgoing);
            //Act
            var unit = new OwnershipService(_mockrepository.Object);
            var result = unit.GetNetIncome(1);
            //Assert
            Assert.IsTrue(result == (decimal)400.00);
        }
    }
}
