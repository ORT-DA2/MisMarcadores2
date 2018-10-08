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
                .Setup(r => r.ObtenerEquipos())
                .Returns(equiposEsperados);

            var businessLogic = new EquiposService(mockUnitOfWork.Object, mockEquiposRepository.Object, null, null);

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
                .Setup(r => r.ObtenerEquipos())
                .Returns(equiposEsperados);

            var businessLogic = new EquiposService(mockUnitOfWork.Object, mockEquiposRepository.Object, null, null);

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

            var businessLogic = new EquiposService(mockUnitOfWork.Object, mockEquiposRepository.Object, mockDeportesRepository.Object, null);

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

            var businessLogic = new EquiposService(mockUnitOfWork.Object, mockEquiposRepository.Object, null, null);

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

            var businessLogic = new EquiposService(mockUnitOfWork.Object, mockEquiposRepository.Object, mockDeportesRepository.Object, null);

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
                 .Setup(r => r.ObtenerEquipoPorDeporte(fakeEquipo.Deporte.Nombre, fakeEquipo.Nombre))
                 .Returns(fakeEquipo);
            mockEquiposRepository.Setup(r => r.Insert(fakeEquipo));

            var businessLogic = new EquiposService(mockUnitOfWork.Object, mockEquiposRepository.Object, mockDeportesRepository.Object, null);

            //Act
            businessLogic.AgregarEquipo(fakeEquipo);

            //Assert
            mockEquiposRepository.VerifyAll();
        }

        [TestMethod]
        public void ObtenerEquipoPorIdOkTest()
        {
            //Arrange
            var fakeEquipo = TestHelper.ObtenerEquipoFalso();
            var fakeId = fakeEquipo.Id;

            var mockEquiposRepository = new Mock<IEquiposRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockEquiposRepository
                .Setup(r => r.ObtenerEquipoPorId(fakeId))
                .Returns(fakeEquipo);

            var businessLogic = new EquiposService(mockUnitOfWork.Object, mockEquiposRepository.Object, null, null);

            //Act
            Equipo obtainedResult = businessLogic.ObtenerEquipoPorId(fakeId);

            //Assert
            mockEquiposRepository.VerifyAll();
            Assert.IsNotNull(obtainedResult);
            Assert.AreEqual(fakeId, obtainedResult.Id);
        }

        [TestMethod]
        public void ObtenerEquipoPorNombreOkTest()
        {
            //Arrange
            var fakeEquipo = TestHelper.ObtenerEquipoFalso();
            var fakeNombre = fakeEquipo.Nombre;

            var mockEquiposRepository = new Mock<IEquiposRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockEquiposRepository
                .Setup(r => r.ObtenerEquipoPorNombre(fakeNombre))
                .Returns(fakeEquipo);

            var businessLogic = new EquiposService(mockUnitOfWork.Object, mockEquiposRepository.Object, null, null);

            //Act
            Equipo obtainedResult = businessLogic.ObtenerEquipoPorNombre(fakeNombre);

            //Assert
            mockEquiposRepository.VerifyAll();
            Assert.IsNotNull(obtainedResult);
            Assert.AreEqual(fakeNombre, obtainedResult.Nombre);
        }

        [TestMethod]
        public void ObtenerEquipoPorIdErrorNotFoundTest()
        {
            //Arrange
            var fakeId = System.Guid.NewGuid();

            var mockEquiposRepository = new Mock<IEquiposRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockEquiposRepository
                .Setup(r => r.ObtenerEquipoPorId(fakeId))
                .Returns((Equipo)null);

            var businessLogic = new EquiposService(mockUnitOfWork.Object, mockEquiposRepository.Object, null, null);

            //Act
            Equipo obtainedResult = businessLogic.ObtenerEquipoPorId(fakeId);

            //Assert
            mockEquiposRepository.VerifyAll();
            Assert.IsNull(obtainedResult);
        }

        [TestMethod]
        [ExpectedException(typeof(NoExisteEquipoException))]
        public void ActualizarEquipoNoExistenteTest()
        {
            //Arrange
            var fakeEquipo = TestHelper.ObtenerEquipoFalso();
            var fakeEquipoId = fakeEquipo.Id;
            var mockEquiposRepository = new Mock<IEquiposRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockEquiposRepository
                .Setup(r => r.ObtenerEquipoPorId(fakeEquipoId))
                .Returns((Equipo)null);

            var businessLogic = new EquiposService(mockUnitOfWork.Object, mockEquiposRepository.Object, null, null);

            //Act
            businessLogic.ModificarEquipo(fakeEquipoId, fakeEquipo);

            //Assert
            mockEquiposRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ExisteEquipoException))]
        public void ActualizarEquipoNuevoNombreYaExistenteTest()
        {
            //Arrange
            var fakeEquipo = TestHelper.ObtenerEquipoFalso();
            var mockEquiposRepository = new Mock<IEquiposRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockEquiposRepository
                .Setup(r => r.ObtenerEquipoPorId(fakeEquipo.Id))
                .Returns(fakeEquipo);
            mockEquiposRepository
                .Setup(r => r.ObtenerEquipoPorDeporte(fakeEquipo.Deporte.Nombre, fakeEquipo.Nombre))
                .Throws(new ExisteEquipoException());

            var businessLogic = new EquiposService(mockUnitOfWork.Object, mockEquiposRepository.Object, null, null);

            //Act
            businessLogic.ModificarEquipo(fakeEquipo.Id, fakeEquipo);

            //Assert
            mockEquiposRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(EquipoDataExceptiom))]
        public void ActualizarEquipoDatosErroneosTest()
        {
            //Arrange
            var fakeEquipo = TestHelper.ObtenerEquipoNombreVacio();
            var mockEquiposRepository = new Mock<IEquiposRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var businessLogic = new EquiposService(mockUnitOfWork.Object, mockEquiposRepository.Object, null, null);

            //Act
            businessLogic.ModificarEquipo(fakeEquipo.Id, fakeEquipo);

            //Assert
            mockEquiposRepository.VerifyAll();
        }

        [TestMethod]
        public void BorrarEquipoOkTest()
        {
            //Arrange
            var fakeEquipo = TestHelper.ObtenerEquipoFalso();
            var fakeEquipoId = fakeEquipo.Id;

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEquiposRepository = new Mock<IEquiposRepository>();
            mockEquiposRepository
                .Setup(r => r.ObtenerEquipoPorId(fakeEquipoId)).Returns(fakeEquipo);
            mockEquiposRepository
                .Setup(r => r.BorrarEquipo(fakeEquipoId));

            var businessLogic = new EquiposService(mockUnitOfWork.Object, mockEquiposRepository.Object, null, null);

            //Act
            businessLogic.BorrarEquipo(fakeEquipoId);

            //Assert
            mockEquiposRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NoExisteEquipoException))]
        public void BorrarEquipoNoExistenteErrorTest()
        {
            var fakeEquipo = TestHelper.ObtenerEquipoFalso();
            var fakeEquipoId = fakeEquipo.Id;

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEquiposRepository = new Mock<IEquiposRepository>();
            mockEquiposRepository
                .Setup(r => r.ObtenerEquipoPorId(fakeEquipoId)).Returns((Equipo)null);

            var businessLogic = new EquiposService(mockUnitOfWork.Object, mockEquiposRepository.Object, null, null);

            //Act
            businessLogic.BorrarEquipo(fakeEquipoId);

            //Assert
            mockEquiposRepository.VerifyAll();
        }
    }
}
