using Microsoft.VisualStudio.TestTools.UnitTesting;
using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using MisMarcadores.Repository;
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

            var mockParticipantesRepository = new Mock<IParticipantesRepository>();
            var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            var mockDeportesRepository = new Mock<IDeportesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockDeportesRepository
                  .Setup(r => r.ObtenerDeportePorNombre(fakeEncuentro.Deporte.Nombre))
                  .Returns(fakeEncuentro.Deporte);
            //mockParticipantesRepository
            //     .Setup(r => r.ObtenerParticipantePorDeporte(fakeEncuentro.Deporte.Nombre, fakeEncuentro.ParticipanteLocal.Nombre))
            //     .Returns(fakeEncuentro.ParticipanteLocal);
            //mockParticipantesRepository
            //     .Setup(r => r.ObtenerParticipantePorDeporte(fakeEncuentro.Deporte.Nombre, fakeEncuentro.ParticipanteVisitante.Nombre))
            //     .Returns(fakeEncuentro.ParticipanteVisitante);
            //mockEncuentrosRepository
            //    .Setup(r => r.ExisteEncuentroEnFecha(fakeEncuentro.FechaHora, fakeEncuentro.ParticipanteLocal.Id))
            //    .Returns(false);
            //mockEncuentrosRepository
            //    .Setup(r => r.ExisteEncuentroEnFecha(fakeEncuentro.FechaHora, fakeEncuentro.ParticipanteVisitante.Id))
            //    .Returns(false);
            //mockEncuentrosRepository.Setup(r => r.Insert(fakeEncuentro));

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object,
                mockDeportesRepository.Object, mockParticipantesRepository.Object);

            //Act
            businessLogic.AgregarEncuentro(fakeEncuentro);

            //Assert
            mockEncuentrosRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(EncuentroDataException))]
        public void AgregarEncuentroParticipanteNombreVacioTest()
        {
            var fakeEncuentro = TestHelper.ObtenerEncuentroParticipanteNombreVacio();
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
        [ExpectedException(typeof(NoExisteParticipanteException))]
        public void AgregarEncuentroParticipanteLocalNoExistenteErrorTest()
        {
            //var fakeEncuentro = TestHelper.ObtenerEncuentroFalso();

            //var mockParticipantesRepository = new Mock<IParticipantesRepository>();
            //var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            //var mockDeportesRepository = new Mock<IDeportesRepository>();
            //var mockUnitOfWork = new Mock<IUnitOfWork>();

            //mockDeportesRepository
            //     .Setup(r => r.ObtenerDeportePorNombre(fakeEncuentro.Deporte.Nombre))
            //     .Returns(fakeEncuentro.Deporte);
            //mockParticipantesRepository
            //     .Setup(r => r.ObtenerParticipantePorDeporte(fakeEncuentro.Deporte.Nombre, fakeEncuentro.ParticipanteLocal.Nombre))
            //     .Returns((Participante)null);
            //mockEncuentrosRepository.Setup(r => r.Insert(fakeEncuentro));

            //var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object, 
            //    mockDeportesRepository.Object, mockParticipantesRepository.Object);

            ////Act
            //businessLogic.AgregarEncuentro(fakeEncuentro);

            ////Assert
            //mockParticipantesRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ExisteEncuentroEnFecha))]
        public void AgregarEncuentroParticipanteLocalExisteEncuentroEnFechaErrorTest()
        {
            //var fakeEncuentro = TestHelper.ObtenerEncuentroFalso();

            //var mockParticipantesRepository = new Mock<IParticipantesRepository>();
            //var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            //var mockDeportesRepository = new Mock<IDeportesRepository>();
            //var mockUnitOfWork = new Mock<IUnitOfWork>();

            //mockDeportesRepository
            //     .Setup(r => r.ObtenerDeportePorNombre(fakeEncuentro.Deporte.Nombre))
            //     .Returns(fakeEncuentro.Deporte);
            //mockParticipantesRepository
            //     .Setup(r => r.ObtenerParticipantePorDeporte(fakeEncuentro.Deporte.Nombre, fakeEncuentro.ParticipanteLocal.Nombre))
            //     .Returns(fakeEncuentro.ParticipanteLocal);
            //mockParticipantesRepository
            //     .Setup(r => r.ObtenerParticipantePorDeporte(fakeEncuentro.Deporte.Nombre, fakeEncuentro.ParticipanteVisitante.Nombre))
            //     .Returns(fakeEncuentro.ParticipanteVisitante);
            //mockEncuentrosRepository
            //    .Setup(r => r.ExisteEncuentroEnFecha(fakeEncuentro.FechaHora, fakeEncuentro.ParticipanteLocal.Id))
            //    .Returns(true);
            //mockEncuentrosRepository.Setup(r => r.Insert(fakeEncuentro));

            //var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object,
            //    mockDeportesRepository.Object, mockParticipantesRepository.Object);

            ////Act
            //businessLogic.AgregarEncuentro(fakeEncuentro);

            ////Assert
            //mockParticipantesRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ExisteEncuentroEnFecha))]
        public void AgregarEncuentroParticipanteVisitanteExisteEncuentroEnFechaErrorTest()
        {
            //var fakeEncuentro = TestHelper.ObtenerEncuentroFalso();

            //var mockParticipantesRepository = new Mock<IParticipantesRepository>();
            //var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            //var mockDeportesRepository = new Mock<IDeportesRepository>();
            //var mockUnitOfWork = new Mock<IUnitOfWork>();

            //mockDeportesRepository
            //     .Setup(r => r.ObtenerDeportePorNombre(fakeEncuentro.Deporte.Nombre))
            //     .Returns(fakeEncuentro.Deporte);
            //mockParticipantesRepository
            //     .Setup(r => r.ObtenerParticipantePorDeporte(fakeEncuentro.Deporte.Nombre, fakeEncuentro.ParticipanteLocal.Nombre))
            //     .Returns(fakeEncuentro.ParticipanteLocal);
            //mockParticipantesRepository
            //     .Setup(r => r.ObtenerParticipantePorDeporte(fakeEncuentro.Deporte.Nombre, fakeEncuentro.ParticipanteVisitante.Nombre))
            //     .Returns(fakeEncuentro.ParticipanteVisitante);
            //mockEncuentrosRepository
            //    .Setup(r => r.ExisteEncuentroEnFecha(fakeEncuentro.FechaHora, fakeEncuentro.ParticipanteVisitante.Id))
            //    .Returns(true);
            //mockEncuentrosRepository.Setup(r => r.Insert(fakeEncuentro));

            //var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object,
            //    mockDeportesRepository.Object, mockParticipantesRepository.Object);

            ////Act
            //businessLogic.AgregarEncuentro(fakeEncuentro);

            ////Assert
            //mockParticipantesRepository.VerifyAll();
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
            var fakeEncuentro = TestHelper.ObtenerEncuentroParticipanteNombreVacio();
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
        [ExpectedException(typeof(NoExisteParticipanteException))]
        public void ActualizarEncuentroParticipanteNoExistenteTest()
        {
            ////Arrange
            //var fakeEncuentro = TestHelper.ObtenerEncuentroFalso();
            //var fakeParticipanteId = fakeEncuentro.ParticipanteLocal.Id;
            //var mockParticipantesRepository = new Mock<IParticipantesRepository>();
            //var mockDeportesRepository = new Mock<IDeportesRepository>();
            //var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            //var mockUnitOfWork = new Mock<IUnitOfWork>();

            //mockDeportesRepository
            //     .Setup(r => r.ObtenerDeportePorNombre(fakeEncuentro.Deporte.Nombre))
            //     .Returns(fakeEncuentro.Deporte);
            //mockParticipantesRepository
            //    .Setup(r => r.ObtenerParticipantePorDeporte(fakeEncuentro.Deporte.Nombre, fakeEncuentro.ParticipanteLocal.Nombre))
            //    .Returns((Participante)null);

            //var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object, mockDeportesRepository.Object, mockParticipantesRepository.Object);

            ////Act
            //businessLogic.ModificarEncuentro(fakeEncuentro.Id, fakeEncuentro);

            ////Assert
            //mockParticipantesRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ExisteEncuentroEnFecha))]
        public void ActualizarEncuentroParticipanteExisteEncuentroEnFechaErrorTest()
        {
            //var fakeEncuentro = TestHelper.ObtenerEncuentroFalso();
            //var fakeNuevoEncuentro = TestHelper.ObtenerNuevoEncuentroFalso();

            //var mockParticipantesRepository = new Mock<IParticipantesRepository>();
            //var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            //var mockDeportesRepository = new Mock<IDeportesRepository>();
            //var mockUnitOfWork = new Mock<IUnitOfWork>();

            //mockDeportesRepository
            //     .Setup(r => r.ObtenerDeportePorNombre(fakeNuevoEncuentro.Deporte.Nombre))
            //     .Returns(fakeEncuentro.Deporte);
            //mockParticipantesRepository
            //     .Setup(r => r.ObtenerParticipantePorDeporte(fakeNuevoEncuentro.Deporte.Nombre, fakeNuevoEncuentro.ParticipanteLocal.Nombre))
            //     .Returns(fakeNuevoEncuentro.ParticipanteLocal);
            //mockParticipantesRepository
            //     .Setup(r => r.ObtenerParticipantePorDeporte(fakeNuevoEncuentro.Deporte.Nombre, fakeNuevoEncuentro.ParticipanteVisitante.Nombre))
            //     .Returns(fakeNuevoEncuentro.ParticipanteVisitante);
            //mockEncuentrosRepository
            //     .Setup(r => r.ObtenerEncuentroPorId(fakeEncuentro.Id))
            //     .Returns(fakeEncuentro);
            //mockEncuentrosRepository
            //    .Setup(r => r.ExisteEncuentroEnFecha(fakeNuevoEncuentro.FechaHora, fakeNuevoEncuentro.ParticipanteLocal.Id))
            //    .Returns(true);
            //mockEncuentrosRepository.Setup(r => r.ModificarEncuentro(fakeEncuentro));

            //var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object,
            //    mockDeportesRepository.Object, mockParticipantesRepository.Object);

            ////Act
            //businessLogic.ModificarEncuentro(fakeEncuentro.Id, fakeNuevoEncuentro);

            ////Assert
            //mockParticipantesRepository.VerifyAll();
        }

        [TestMethod]
        public void ObtenerEncuentrosPorDeporteOkTest()
        {
            //Arrange
            var encuentrosEsperados = TestHelper.ObtenerEncuentrosFalsos();
            var deporteFalso = TestHelper.ObtenerDeporteFalso();

            var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockEncuentrosRepository
                .Setup(r => r.ObtenerEncuentrosPorDeporte(deporteFalso.Nombre))
                .Returns(encuentrosEsperados);

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object, null, null);

            //Act
            IEnumerable<Encuentro> obtainedResult = businessLogic.ObtenerEncuentrosPorDeporte(deporteFalso.Nombre);

            //Assert
            mockEncuentrosRepository.VerifyAll();
            Assert.IsNotNull(obtainedResult);
            Assert.AreEqual(encuentrosEsperados, obtainedResult);
        }

        [TestMethod]
        public void ObtenerEncuentrosPorParticipanteOkTest()
        {
            //Arrange
            var encuentrosEsperados = TestHelper.ObtenerEncuentrosFalsos();
            var participanteFalso = TestHelper.ObtenerParticipanteFalso();

            var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockEncuentrosRepository
                .Setup(r => r.ObtenerEncuentrosPorParticipante(participanteFalso.Id))
                .Returns(encuentrosEsperados);

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object, null, null);

            //Act
            IEnumerable<Encuentro> obtainedResult = businessLogic.ObtenerEncuentrosPorParticipante(participanteFalso.Id);

            //Assert
            mockEncuentrosRepository.VerifyAll();
            Assert.IsNotNull(obtainedResult);
            Assert.AreEqual(encuentrosEsperados, obtainedResult);
        }
    }
}
