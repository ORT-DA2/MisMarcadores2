using Microsoft.VisualStudio.TestTools.UnitTesting;
using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using MisMarcadores.Logic;
using MisMarcadores.Repository;
using Moq;
using System.Collections.Generic;
using System.Linq;
namespace MisMarcadores.Logic.Tests
{
    [TestClass]
    public class ParticipantesLogicTest
    {
        [TestMethod]
        public void ObtenerParticipantesOkTest()
        {
            //Arrange
            var participantesEsperados = TestHelper.ObtenerParticipantesFalsos();

            var mockParticipantesRepository = new Mock<IParticipantesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockParticipantesRepository
                .Setup(r => r.ObtenerParticipantes())
                .Returns(participantesEsperados);

            var businessLogic = new ParticipantesService(mockUnitOfWork.Object, mockParticipantesRepository.Object, null, null);

            //Act
            IEnumerable<Participante> obtainedResult = businessLogic.ObtenerParticipantes();

            //Assert
            mockParticipantesRepository.VerifyAll();
            Assert.IsNotNull(obtainedResult);
            Assert.AreEqual(participantesEsperados, obtainedResult);
        }

        [TestMethod]
        public void ObtenerParticipantesNullTest()
        {
            //Arrange
            List<Participante> participantesEsperados = null;

            var mockParticipantesRepository = new Mock<IParticipantesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockParticipantesRepository
                .Setup(r => r.ObtenerParticipantes())
                .Returns(participantesEsperados);

            var businessLogic = new ParticipantesService(mockUnitOfWork.Object, mockParticipantesRepository.Object, null, null);

            //Act
            IEnumerable<Participante> obtainedResult = businessLogic.ObtenerParticipantes();

            //Assert
            mockParticipantesRepository.VerifyAll();
            Assert.IsNull(obtainedResult);
        }

        [TestMethod]
        public void AgregarParticipanteOkTest()
        {
            //Arrange
            var fakeParticipante = TestHelper.ObtenerParticipanteFalso();

            var mockParticipantesRepository = new Mock<IParticipantesRepository>();
            var mockDeportesRepository = new Mock<IDeportesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockDeportesRepository
                .Setup(r => r.ObtenerDeportePorNombre(fakeParticipante.Deporte.Nombre)).Returns(fakeParticipante.Deporte);
            mockParticipantesRepository
                .Setup(r => r.Insert(fakeParticipante));

            var businessLogic = new ParticipantesService(mockUnitOfWork.Object, mockParticipantesRepository.Object, mockDeportesRepository.Object, null);

            //Act
            businessLogic.AgregarParticipante(fakeParticipante);

            //Assert
            mockParticipantesRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ParticipanteDataExceptiom))]
        public void AgregarParticipanteNombreVacioTest()
        {
            var fakeParticipante = TestHelper.ObtenerParticipanteNombreVacio();
            var mockParticipantesRepository = new Mock<IParticipantesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockParticipantesRepository
                .Setup(r => r.Insert(fakeParticipante));

            var businessLogic = new ParticipantesService(mockUnitOfWork.Object, mockParticipantesRepository.Object, null, null);

            //Act
            businessLogic.AgregarParticipante(fakeParticipante);

            //Assert
            mockParticipantesRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NoExisteDeporteException))]
        public void AgregarParticipanteDeporteNoExistenteErrorTest()
        {
            var fakeParticipante = TestHelper.ObtenerParticipanteFalso();

            var mockParticipantesRepository = new Mock<IParticipantesRepository>();
            var mockDeportesRepository = new Mock<IDeportesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockDeportesRepository
                 .Setup(r => r.ObtenerDeportePorNombre(fakeParticipante.Deporte.Nombre)).Returns((Deporte)null);
            mockParticipantesRepository.Setup(r => r.Insert(fakeParticipante));

            var businessLogic = new ParticipantesService(mockUnitOfWork.Object, mockParticipantesRepository.Object, mockDeportesRepository.Object, null);

            //Act
            businessLogic.AgregarParticipante(fakeParticipante);

            //Assert
            mockParticipantesRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ExisteParticipanteException))]
        public void AgregarParticipanteExistenteErrorTest()
        {
            var fakeParticipante = TestHelper.ObtenerParticipanteFalso();

            var mockParticipantesRepository = new Mock<IParticipantesRepository>();
            var mockDeportesRepository = new Mock<IDeportesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockDeportesRepository
                 .Setup(r => r.ObtenerDeportePorNombre(fakeParticipante.Deporte.Nombre))
                 .Returns(fakeParticipante.Deporte);
            mockParticipantesRepository
                 .Setup(r => r.ObtenerParticipantePorDeporte(fakeParticipante.Deporte.Nombre, fakeParticipante.Nombre))
                 .Returns(fakeParticipante);
            mockParticipantesRepository.Setup(r => r.Insert(fakeParticipante));

            var businessLogic = new ParticipantesService(mockUnitOfWork.Object, mockParticipantesRepository.Object, mockDeportesRepository.Object, null);

            //Act
            businessLogic.AgregarParticipante(fakeParticipante);

            //Assert
            mockParticipantesRepository.VerifyAll();
        }

        [TestMethod]
        public void ObtenerParticipantePorIdOkTest()
        {
            //Arrange
            var fakeParticipante = TestHelper.ObtenerParticipanteFalso();
            var fakeId = fakeParticipante.Id;

            var mockParticipantesRepository = new Mock<IParticipantesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockParticipantesRepository
                .Setup(r => r.ObtenerParticipantePorId(fakeId))
                .Returns(fakeParticipante);

            var businessLogic = new ParticipantesService(mockUnitOfWork.Object, mockParticipantesRepository.Object, null, null);

            //Act
            Participante obtainedResult = businessLogic.ObtenerParticipantePorId(fakeId);

            //Assert
            mockParticipantesRepository.VerifyAll();
            Assert.IsNotNull(obtainedResult);
            Assert.AreEqual(fakeId, obtainedResult.Id);
        }

        [TestMethod]
        public void ObtenerParticipantePorNombreOkTest()
        {
            //Arrange
            var fakeParticipante = TestHelper.ObtenerParticipanteFalso();
            var fakeNombre = fakeParticipante.Nombre;

            var mockParticipantesRepository = new Mock<IParticipantesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockParticipantesRepository
                .Setup(r => r.ObtenerParticipantePorNombre(fakeNombre))
                .Returns(fakeParticipante);

            var businessLogic = new ParticipantesService(mockUnitOfWork.Object, mockParticipantesRepository.Object, null, null);

            //Act
            Participante obtainedResult = businessLogic.ObtenerParticipantePorNombre(fakeNombre);

            //Assert
            mockParticipantesRepository.VerifyAll();
            Assert.IsNotNull(obtainedResult);
            Assert.AreEqual(fakeNombre, obtainedResult.Nombre);
        }

        [TestMethod]
        public void ObtenerParticipantePorIdErrorNotFoundTest()
        {
            //Arrange
            var fakeId = System.Guid.NewGuid();

            var mockParticipantesRepository = new Mock<IParticipantesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockParticipantesRepository
                .Setup(r => r.ObtenerParticipantePorId(fakeId))
                .Returns((Participante)null);

            var businessLogic = new ParticipantesService(mockUnitOfWork.Object, mockParticipantesRepository.Object, null, null);

            //Act
            Participante obtainedResult = businessLogic.ObtenerParticipantePorId(fakeId);

            //Assert
            mockParticipantesRepository.VerifyAll();
            Assert.IsNull(obtainedResult);
        }

        [TestMethod]
        [ExpectedException(typeof(NoExisteParticipanteException))]
        public void ActualizarParticipanteNoExistenteTest()
        {
            //Arrange
            var fakeParticipante = TestHelper.ObtenerParticipanteFalso();
            var fakeParticipanteId = fakeParticipante.Id;
            var mockParticipantesRepository = new Mock<IParticipantesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockParticipantesRepository
                .Setup(r => r.ObtenerParticipantePorId(fakeParticipanteId))
                .Returns((Participante)null);

            var businessLogic = new ParticipantesService(mockUnitOfWork.Object, mockParticipantesRepository.Object, null, null);

            //Act
            businessLogic.ModificarParticipante(fakeParticipanteId, fakeParticipante);

            //Assert
            mockParticipantesRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ExisteParticipanteException))]
        public void ActualizarParticipanteNuevoNombreYaExistenteTest()
        {
            //Arrange
            var fakeParticipante = TestHelper.ObtenerParticipanteFalso();
            var mockParticipantesRepository = new Mock<IParticipantesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockParticipantesRepository
                .Setup(r => r.ObtenerParticipantePorId(fakeParticipante.Id))
                .Returns(fakeParticipante);
            mockParticipantesRepository
                .Setup(r => r.ObtenerParticipantePorDeporte(fakeParticipante.Deporte.Nombre, fakeParticipante.Nombre))
                .Throws(new ExisteParticipanteException());

            var businessLogic = new ParticipantesService(mockUnitOfWork.Object, mockParticipantesRepository.Object, null, null);

            //Act
            businessLogic.ModificarParticipante(fakeParticipante.Id, fakeParticipante);

            //Assert
            mockParticipantesRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ParticipanteDataExceptiom))]
        public void ActualizarParticipanteDatosErroneosTest()
        {
            //Arrange
            var fakeParticipante = TestHelper.ObtenerParticipanteNombreVacio();
            var mockParticipantesRepository = new Mock<IParticipantesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var businessLogic = new ParticipantesService(mockUnitOfWork.Object, mockParticipantesRepository.Object, null, null);

            //Act
            businessLogic.ModificarParticipante(fakeParticipante.Id, fakeParticipante);

            //Assert
            mockParticipantesRepository.VerifyAll();
        }

        [TestMethod]
        public void BorrarParticipanteOkTest()
        {
            //Arrange
            var fakeParticipante = TestHelper.ObtenerParticipanteFalso();
            var fakeParticipanteId = fakeParticipante.Id;

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockParticipantesRepository = new Mock<IParticipantesRepository>();
            mockParticipantesRepository
                .Setup(r => r.ObtenerParticipantePorId(fakeParticipanteId)).Returns(fakeParticipante);
            mockParticipantesRepository
                .Setup(r => r.BorrarParticipante(fakeParticipanteId));

            var businessLogic = new ParticipantesService(mockUnitOfWork.Object, mockParticipantesRepository.Object, null, null);

            //Act
            businessLogic.BorrarParticipante(fakeParticipanteId);

            //Assert
            mockParticipantesRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NoExisteParticipanteException))]
        public void BorrarParticipanteNoExistenteErrorTest()
        {
            var fakeParticipante = TestHelper.ObtenerParticipanteFalso();
            var fakeParticipanteId = fakeParticipante.Id;

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockParticipantesRepository = new Mock<IParticipantesRepository>();
            mockParticipantesRepository
                .Setup(r => r.ObtenerParticipantePorId(fakeParticipanteId)).Returns((Participante)null);

            var businessLogic = new ParticipantesService(mockUnitOfWork.Object, mockParticipantesRepository.Object, null, null);

            //Act
            businessLogic.BorrarParticipante(fakeParticipanteId);

            //Assert
            mockParticipantesRepository.VerifyAll();
        }
    }
}
