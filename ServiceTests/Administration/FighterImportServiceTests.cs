using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DAL;
using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service.Administration;

namespace ServiceTests.Administration
{
    [TestClass]
    public class FighterImportServiceTests
    {
        [TestMethod]
        public void Driver1()
        {
            var mockRepo = new Mock<IRepository>();
            mockRepo.Setup(i => i.GetAll(It.IsAny<Func<IQueryable<Fighter>, IEnumerable<Fighter>>>())).Returns(CreateFighterList);
            var service = new FighterImportService(mockRepo.Object);
            var fighters = service.GetFightersFromImport();
            service.HydrateWithIds(new HashSet<Fighter>(fighters));
        }

        private List<Fighter> CreateFighterList()
        {
            return new List<Fighter>
            {
                new Fighter
                {
                    FirstMidName = "Ronda",
                    LastName = "Rousey",
                    FighterId = 3
                }
            };
        }
    }
}
