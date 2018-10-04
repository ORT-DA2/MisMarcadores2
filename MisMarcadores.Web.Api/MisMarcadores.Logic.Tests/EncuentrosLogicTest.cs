using Microsoft.VisualStudio.TestTools.UnitTesting;
using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using MisMarcadores.Repository;
using MisMarcadores.Repository.Exceptions;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace MisMarcadores.Logic.Tests
{
    [TestClass]
    public class EncuentrosLogicTest
    {
        [TestMethod]
        public void ObtenerEncuentrosOkTest()
        {
            //Arrange
            var encuentrosEsperados = TestHelper.ObtenerEncuentrosFalsos();

            var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockEncuentrosRepository
                .Setup(r => r.ObtenerEncuentros())
                .Returns(encuentrosEsperados);

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object, null, null);

            //Act
            IEnumerable<Encuentro> obtainedResult = businessLogic.ObtenerEncuentros();

            //Assert
            mockEncuentrosRepository.VerifyAll();
            Assert.IsNotNull(obtainedResult);
            Assert.AreEqual(encuentrosEsperados, obtainedResult);
        }

        [TestMethod]
        public void ObtenerEncuentrosNullTest()
        {
            //Arrange
            List<Encuentro> encuentrosEsperados = null;

            var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockEncuentrosRepository
                .Setup(r => r.ObtenerEncuentros())
                .Returns(encuentrosEsperados);

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object, null, null);

            //Act
            IEnumerable<Encuentro> obtainedResult = businessLogic.ObtenerEncuentros();

            //Assert
            mockEncuentrosRepository.VerifyAll();
            Assert.IsNull(obtainedResult);
        }

        [TestMethod]
        public void ObtenerEncuentroPorIdOkTest()
        {
            //Arrange
            var fakeEncuentro = TestHelper.ObtenerEncuentroFalso();
            var fakeId = fakeEncuentro.Id;

            var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockEncuentrosRepository
                .Setup(r => r.ObtenerEncuentroPorId(fakeId))
                .Returns(fakeEncuentro);

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object, null, null);

            //Act
            Encuentro obtainedResult = businessLogic.ObtenerEncuentroPorId(fakeId);

            //Assert
            mockEncuentrosRepository.VerifyAll();
            Assert.IsNotNull(obtainedResult);
            Assert.AreEqual(fakeId, obtainedResult.Id);
        }

        [TestMethod]
        public void ObtenerEncuentroPorIdErrorNotFoundTest()
        {
            //Arrange
            var fakeId = System.Guid.NewGuid();

            var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockEncuentrosRepository
                .Setup(r => r.ObtenerEncuentroPorId(fakeId))
                .Returns((Encuentro)null);

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object, null, null);

            //Act
            Encuentro obtainedResult = businessLogic.ObtenerEncuentroPorId(fakeId);

            //Assert
            mockEncuentrosRepository.VerifyAll();
            Assert.IsNull(obtainedResult);
        }

        [TestMethod]
        public void AgregarEncuentroOkTest()
        {
            //Arrange
            var fakeEncuentro = TestHelper.ObtenerEncuentroFalso();

            var mockEquiposRepository = new Mock<IEquiposRepository>();
            var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            var mockDeportesRepository = new Mock<IDeportesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockDeportesRepository
                  .Setup(r => r.ObtenerDeportePorNombre(fakeEncuentro.Deporte.Nombre))
                  .Returns(fakeEncuentro.Deporte);
            mockEquiposRepository
                 .Setup(r => r.ObtenerEquipoPorDeporte(fakeEncuentro.Deporte.Nombre, fakeEncuentro.EquipoLocal.Nombre))
                 .Returns(fakeEncuentro.EquipoLocal);
            mockEquiposRepository
                 .Setup(r => r.ObtenerEquipoPorDeporte(fakeEncuentro.Deporte.Nombre, fakeEncuentro.EquipoVisitante.Nombre))
                 .Returns(fakeEncuentro.EquipoVisitante);
            mockEncuentrosRepository
                .Setup(r => r.ExisteEncuentroEnFecha(fakeEncuentro.FechaHora, fakeEncuentro.EquipoLocal.Id))
                .Returns(false);
            mockEncuentrosRepository
                .Setup(r => r.ExisteEncuentroEnFecha(fakeEncuentro.FechaHora, fakeEncuentro.EquipoVisitante.Id))
                .Returns(false);
            mockEncuentrosRepository.Setup(r => r.Insert(fakeEncuentro));

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object,
                mockDeportesRepository.Object, mockEquiposRepository.Object);

            //Act
            businessLogic.AgregarEncuentro(fakeEncuentro);

            //Assert
            mockEncuentrosRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(EncuentroDataException))]
        public void AgregarEncuentroEquipoNombreVacioTest()
        {
            var fakeEncuentro = TestHelper.ObtenerEncuentroEquipoNombreVacio();
            var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockEncuentrosRepository
                .Setup(r => r.Insert(fakeEncuentro));

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object, null, null);

            //Act
            businessLogic.AgregarEncuentro(fakeEncuentro);

            //Assert
            mockEncuentrosRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NoExisteDeporteException))]
        public void AgregarEncuentroDeporteNoExistenteErrorTest()
        {
            var fakeEncuentro = TestHelper.ObtenerEncuentroFalso();

            var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            var mockDeportesRepository = new Mock<IDeportesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockDeportesRepository
                 .Setup(r => r.ObtenerDeportePorNombre(fakeEncuentro.Deporte.Nombre)).Returns((Deporte)null);
            mockEncuentrosRepository.Setup(r => r.Insert(fakeEncuentro));

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object, mockDeportesRepository.Object, null);

            //Act
            businessLogic.AgregarEncuentro(fakeEncuentro);

            //Assert
            mockEncuentrosRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NoExisteEquipoException))]
        public void AgregarEncuentroEquipoLocalNoExistenteErrorTest()
        {
            var fakeEncuentro = TestHelper.ObtenerEncuentroFalso();

            var mockEquiposRepository = new Mock<IEquiposRepository>();
            var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            var mockDeportesRepository = new Mock<IDeportesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockDeportesRepository
                 .Setup(r => r.ObtenerDeportePorNombre(fakeEncuentro.Deporte.Nombre))
                 .Returns(fakeEncuentro.Deporte);
            mockEquiposRepository
                 .Setup(r => r.ObtenerEquipoPorDeporte(fakeEncuentro.Deporte.Nombre, fakeEncuentro.EquipoLocal.Nombre))
                 .Returns((Equipo)null);
            mockEncuentrosRepository.Setup(r => r.Insert(fakeEncuentro));

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object, 
                mockDeportesRepository.Object, mockEquiposRepository.Object);

            //Act
            businessLogic.AgregarEncuentro(fakeEncuentro);

            //Assert
            mockEquiposRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ExisteEncuentroEnFecha))]
        public void AgregarEncuentroEquipoLocalExisteEncuentroEnFechaErrorTest()
        {
            var fakeEncuentro = TestHelper.ObtenerEncuentroFalso();

            var mockEquiposRepository = new Mock<IEquiposRepository>();
            var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            var mockDeportesRepository = new Mock<IDeportesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockDeportesRepository
                 .Setup(r => r.ObtenerDeportePorNombre(fakeEncuentro.Deporte.Nombre))
                 .Returns(fakeEncuentro.Deporte);
            mockEquiposRepository
                 .Setup(r => r.ObtenerEquipoPorDeporte(fakeEncuentro.Deporte.Nombre, fakeEncuentro.EquipoLocal.Nombre))
                 .Returns(fakeEncuentro.EquipoLocal);
            mockEquiposRepository
                 .Setup(r => r.ObtenerEquipoPorDeporte(fakeEncuentro.Deporte.Nombre, fakeEncuentro.EquipoVisitante.Nombre))
                 .Returns(fakeEncuentro.EquipoVisitante);
            mockEncuentrosRepository
                .Setup(r => r.ExisteEncuentroEnFecha(fakeEncuentro.FechaHora, fakeEncuentro.EquipoLocal.Id))
                .Returns(true);
            mockEncuentrosRepository.Setup(r => r.Insert(fakeEncuentro));

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object,
                mockDeportesRepository.Object, mockEquiposRepository.Object);

            //Act
            businessLogic.AgregarEncuentro(fakeEncuentro);

            //Assert
            mockEquiposRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ExisteEncuentroEnFecha))]
        public void AgregarEncuentroEquipoVisitanteExisteEncuentroEnFechaErrorTest()
        {
            var fakeEncuentro = TestHelper.ObtenerEncuentroFalso();

            var mockEquiposRepository = new Mock<IEquiposRepository>();
            var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            var mockDeportesRepository = new Mock<IDeportesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockDeportesRepository
                 .Setup(r => r.ObtenerDeportePorNombre(fakeEncuentro.Deporte.Nombre))
                 .Returns(fakeEncuentro.Deporte);
            mockEquiposRepository
                 .Setup(r => r.ObtenerEquipoPorDeporte(fakeEncuentro.Deporte.Nombre, fakeEncuentro.EquipoLocal.Nombre))
                 .Returns(fakeEncuentro.EquipoLocal);
            mockEquiposRepository
                 .Setup(r => r.ObtenerEquipoPorDeporte(fakeEncuentro.Deporte.Nombre, fakeEncuentro.EquipoVisitante.Nombre))
                 .Returns(fakeEncuentro.EquipoVisitante);
            mockEncuentrosRepository
                .Setup(r => r.ExisteEncuentroEnFecha(fakeEncuentro.FechaHora, fakeEncuentro.EquipoVisitante.Id))
                .Returns(true);
            mockEncuentrosRepository.Setup(r => r.Insert(fakeEncuentro));

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object,
                mockDeportesRepository.Object, mockEquiposRepository.Object);

            //Act
            businessLogic.AgregarEncuentro(fakeEncuentro);

            //Assert
            mockEquiposRepository.VerifyAll();
        }

        [TestMethod]
        public void BorrarEncuentroOkTest()
        {
            //Arrange
            var fakeEncuentro = TestHelper.ObtenerEncuentroFalso();
            var fakeEncuentroId = fakeEncuentro.Id;

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            mockEncuentrosRepository
                .Setup(r => r.ObtenerEncuentroPorId(fakeEncuentroId)).Returns(fakeEncuentro);
            mockEncuentrosRepository
                .Setup(r => r.BorrarEncuentro(fakeEncuentroId));

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object, null, null);

            //Act
            businessLogic.BorrarEncuentro(fakeEncuentroId);

            //Assert
            mockEncuentrosRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NoExisteEncuentroException))]
        public void BorrarEncuentroNoExistenteErrorTest()
        {
            var fakeEncuentro = TestHelper.ObtenerEncuentroFalso();
            var fakeEncuentroId = fakeEncuentro.Id;

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            mockEncuentrosRepository
                .Setup(r => r.ObtenerEncuentroPorId(fakeEncuentroId)).Returns((Encuentro) null);

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object, null, null);

            //Act
            businessLogic.BorrarEncuentro(fakeEncuentroId);

            //Assert
            mockEncuentrosRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(EncuentroDataException))]
        public void ActualizarEncuentroDatosInvalidosTest()
        {
            var fakeEncuentro = TestHelper.ObtenerEncuentroEquipoNombreVacio();
            var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockEncuentrosRepository
                .Setup(r => r.ModificarEncuentro(fakeEncuentro));

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object, null, null);

            //Act
            businessLogic.AgregarEncuentro(fakeEncuentro);

            //Assert
            mockEncuentrosRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NoExisteDeporteException))]
        public void ActualizarEncuentroDeporteNoExistenteErrorTest()
        {
            var fakeEncuentro = TestHelper.ObtenerEncuentroFalso();

            var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            var mockDeportesRepository = new Mock<IDeportesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockDeportesRepository
                 .Setup(r => r.ObtenerDeportePorNombre(fakeEncuentro.Deporte.Nombre)).Returns((Deporte)null);
            mockEncuentrosRepository.Setup(r => r.Insert(fakeEncuentro));

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object, mockDeportesRepository.Object, null);

            //Act
            businessLogic.ModificarEncuentro(fakeEncuentro.Id, fakeEncuentro);

            //Assert
            mockEncuentrosRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NoExisteEquipoException))]
        public void ActualizarEncuentroEquipoNoExistenteTest()
        {
            //Arrange
            var fakeEncuentro = TestHelper.ObtenerEncuentroFalso();
            var fakeEquipoId = fakeEncuentro.EquipoLocal.Id;
            var mockEquiposRepository = new Mock<IEquiposRepository>();
            var mockDeportesRepository = new Mock<IDeportesRepository>();
            var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockDeportesRepository
                 .Setup(r => r.ObtenerDeportePorNombre(fakeEncuentro.Deporte.Nombre))
                 .Returns(fakeEncuentro.Deporte);
            mockEquiposRepository
                .Setup(r => r.ObtenerEquipoPorDeporte(fakeEncuentro.Deporte.Nombre, fakeEncuentro.EquipoLocal.Nombre))
                .Returns((Equipo)null);

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object, mockDeportesRepository.Object, mockEquiposRepository.Object);

            //Act
            businessLogic.ModificarEncuentro(fakeEncuentro.Id, fakeEncuentro);

            //Assert
            mockEquiposRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ExisteEncuentroEnFecha))]
        public void ActualizarEncuentroEquipoExisteEncuentroEnFechaErrorTest()
        {
            var fakeEncuentro = TestHelper.ObtenerEncuentroFalso();

            var mockEquiposRepository = new Mock<IEquiposRepository>();
            var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            var mockDeportesRepository = new Mock<IDeportesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockDeportesRepository
                 .Setup(r => r.ObtenerDeportePorNombre(fakeEncuentro.Deporte.Nombre))
                 .Returns(fakeEncuentro.Deporte);
            mockEquiposRepository
                 .Setup(r => r.ObtenerEquipoPorDeporte(fakeEncuentro.Deporte.Nombre, fakeEncuentro.EquipoLocal.Nombre))
                 .Returns(fakeEncuentro.EquipoLocal);
            mockEquiposRepository
                 .Setup(r => r.ObtenerEquipoPorDeporte(fakeEncuentro.Deporte.Nombre, fakeEncuentro.EquipoVisitante.Nombre))
                 .Returns(fakeEncuentro.EquipoVisitante);
            mockEncuentrosRepository
                .Setup(r => r.ExisteEncuentroEnFecha(fakeEncuentro.FechaHora, fakeEncuentro.EquipoLocal.Id))
                .Returns(true);
            mockEncuentrosRepository.Setup(r => r.ModificarEncuentro(fakeEncuentro));

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object,
                mockDeportesRepository.Object, mockEquiposRepository.Object);

            //Act
            businessLogic.ModificarEncuentro(fakeEncuentro.Id, fakeEncuentro);

            //Assert
            mockEquiposRepository.VerifyAll();
        }
    }
}
