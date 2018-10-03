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

            var businessLogic = new EquiposService(mockUnitOfWork.Object, mockEquiposRepository.Object, null);

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

            var businessLogic = new EquiposService(mockUnitOfWork.Object, mockEquiposRepository.Object, null);

            //Act
            IEnumerable<Equipo> obtainedResult = businessLogic.ObtenerEquipos();

            //Assert
            mockEquiposRepository.VerifyAll();
            Assert.IsNull(obtainedResult);
        }

        [TestMethod]
        public void AgregarEquipoOkTest()
        {
            //Arrange
            var fakeEquipo = TestHelper.ObtenerEquipoFalso();

            var mockEquiposRepository = new Mock<IEquiposRepository>();
            var mockDeportesRepository = new Mock<IDeportesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockDeportesRepository
                .Setup(r => r.ObtenerDeportePorNombre(fakeEquipo.Deporte.Nombre)).Returns(fakeEquipo.Deporte);
            mockEquiposRepository
                .Setup(r => r.Insert(fakeEquipo));

            var businessLogic = new EquiposService(mockUnitOfWork.Object, mockEquiposRepository.Object, mockDeportesRepository.Object);

            //Act
            businessLogic.AgregarEquipo(fakeEquipo);

            //Assert
            mockEquiposRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(EquipoDataExceptiom))]
        public void AgregarEquipoNombreVacioTest()
        {
            var fakeEquipo = TestHelper.ObtenerEquipoNombreVacio();
            var mockEquiposRepository = new Mock<IEquiposRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockEquiposRepository
                .Setup(r => r.Insert(fakeEquipo));

            var businessLogic = new EquiposService(mockUnitOfWork.Object, mockEquiposRepository.Object, null);

            //Act
            businessLogic.AgregarEquipo(fakeEquipo);

            //Assert
            mockEquiposRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NoExisteDeporteException))]
        public void AgregarEquipoDeporteNoExistenteErrorTest()
        {
            var fakeEquipo = TestHelper.ObtenerEquipoFalso();

            var mockEquiposRepository = new Mock<IEquiposRepository>();
            var mockDeportesRepository = new Mock<IDeportesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockDeportesRepository
                 .Setup(r => r.ObtenerDeportePorNombre(fakeEquipo.Deporte.Nombre)).Returns((Deporte)null);
            mockEquiposRepository.Setup(r => r.Insert(fakeEquipo));

            var businessLogic = new EquiposService(mockUnitOfWork.Object, mockEquiposRepository.Object, mockDeportesRepository.Object);

            //Act
            businessLogic.AgregarEquipo(fakeEquipo);

            //Assert
            mockEquiposRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ExisteEquipoException))]
        public void AgregarEquipoExistenteErrorTest()
        {
            var fakeEquipo = TestHelper.ObtenerEquipoFalso();

            var mockEquiposRepository = new Mock<IEquiposRepository>();
            var mockDeportesRepository = new Mock<IDeportesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockDeportesRepository
                 .Setup(r => r.ObtenerDeportePorNombre(fakeEquipo.Deporte.Nombre))
                 .Returns(fakeEquipo.Deporte);
            mockEquiposRepository
                 .Setup(r => r.ExisteEquipo(fakeEquipo.Deporte.Nombre, fakeEquipo.Nombre))
                 .Returns(true);
            mockEquiposRepository.Setup(r => r.Insert(fakeEquipo));

            var businessLogic = new EquiposService(mockUnitOfWork.Object, mockEquiposRepository.Object, mockDeportesRepository.Object);

            //Act
            businessLogic.AgregarEquipo(fakeEquipo);

            //Assert
            mockEquiposRepository.VerifyAll();
        }

        [TestMethod]
        public void ObtenerEquipoPorNombreOkTest()
        {
            //Arrange
            var fakeEquipo = TestHelper.ObtenerEquipoFalso();
            var fakeNombreEquipo = "Defensor";

            var mockEquiposRepository = new Mock<IEquiposRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockEquiposRepository
                .Setup(r => r.ObtenerEquipoPorNombre(fakeNombreEquipo))
                .Returns(fakeEquipo);

            var businessLogic = new EquiposService(mockUnitOfWork.Object, mockEquiposRepository.Object, null);

            //Act
            Equipo obtainedResult = businessLogic.ObtenerEquipoPorNombre(fakeNombreEquipo);

            //Assert
            mockEquiposRepository.VerifyAll();
            Assert.IsNotNull(obtainedResult);
            Assert.AreEqual(fakeNombreEquipo, obtainedResult.Nombre);
        }

        [TestMethod]
        public void ObtenerEquipoPorNombreErrorNotFoundTest()
        {
            //Arrange
            var fakeNombreEquipo = "Nacional";

            var mockEquiposRepository = new Mock<IEquiposRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockEquiposRepository
                .Setup(r => r.ObtenerEquipoPorNombre(fakeNombreEquipo))
                .Returns((Equipo)null);

            var businessLogic = new EquiposService(mockUnitOfWork.Object, mockEquiposRepository.Object, null);

            //Act
            Equipo obtainedResult = businessLogic.ObtenerEquipoPorNombre(fakeNombreEquipo);

            //Assert
            mockEquiposRepository.VerifyAll();
            Assert.IsNull(obtainedResult);
        }

    }
}
