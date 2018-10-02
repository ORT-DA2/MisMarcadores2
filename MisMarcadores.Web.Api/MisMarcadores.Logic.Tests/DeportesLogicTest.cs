using Microsoft.VisualStudio.TestTools.UnitTesting;
using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using MisMarcadores.Logic;
using MisMarcadores.Repository;
using MisMarcadores.Repository.Exceptions;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace MisMarcadores.Logic.Tests
{
    [TestClass]
    public class DeportesLogicTest
    {
        [TestMethod]
        public void ObtenerDeportesOkTest()
        {
            //Arrange
            var deportesEsperados = TestHelper.ObtenerDeportesFalsos();

            var mockDeportesRepository = new Mock<IDeportesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockDeportesRepository
                .Setup(r => r.GetAll())
                .Returns(deportesEsperados);

            var businessLogic = new DeportesService(mockUnitOfWork.Object, mockDeportesRepository.Object);

            //Act
            IEnumerable<Deporte> obtainedResult = businessLogic.ObtenerDeportes();

            //Assert
            mockDeportesRepository.VerifyAll();
            Assert.IsNotNull(obtainedResult);
            Assert.AreEqual(deportesEsperados, obtainedResult);
        }
    }
}
