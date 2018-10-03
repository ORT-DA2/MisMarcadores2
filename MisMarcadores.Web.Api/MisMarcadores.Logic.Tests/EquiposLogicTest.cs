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
    public class EquiposLogicTest
    {
        [TestMethod]
        public void ObtenerEquiposOkTest()
        {
            //Arrange
            var equiposEsperados = TestHelper.ObtenerEquiposFalsos();

            var mockEquiposRepository = new Mock<IEquiposRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockEquiposRepository
                .Setup(r => r.GetAll())
                .Returns(equiposEsperados);

            var businessLogic = new EquiposService(mockUnitOfWork.Object, mockEquiposRepository.Object);

            //Act
            IEnumerable<Equipo> obtainedResult = businessLogic.ObtenerEquipos();

            //Assert
            mockEquiposRepository.VerifyAll();
            Assert.IsNotNull(obtainedResult);
            Assert.AreEqual(equiposEsperados, obtainedResult);
        }

        [TestMethod]
        public void ObtenerEquiposNullTest()
        {
            //Arrange
            List<Equipo> equiposEsperados = null;

            var mockEquiposRepository = new Mock<IEquiposRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockEquiposRepository
                .Setup(r => r.GetAll())
                .Returns(equiposEsperados);

            var businessLogic = new EquiposService(mockUnitOfWork.Object, mockEquiposRepository.Object);

            //Act
            IEnumerable<Equipo> obtainedResult = businessLogic.ObtenerEquipos();

            //Assert
            mockEquiposRepository.VerifyAll();
            Assert.IsNull(obtainedResult);
        }
    }
}
