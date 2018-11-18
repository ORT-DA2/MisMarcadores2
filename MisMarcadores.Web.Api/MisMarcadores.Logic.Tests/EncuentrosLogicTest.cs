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
            var mockParticipantesRepository = new Mock<IParticipantesRepository>();

            for (int i = 0; i < 2; i++)            
                for (int j = 0; j < 2; j++)               
                  mockParticipantesRepository
                 .Setup(r => r.ObtenerParticipantePorId(encuentrosEsperados.ToList()[i].ParticipanteEncuentro.ToList()[j].ParticipanteId))
                 .Returns(encuentrosEsperados.ToList()[i].ParticipanteEncuentro.ToList()[j].Participante);
                        
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockEncuentrosRepository
                .Setup(r => r.ObtenerEncuentros())
                .Returns(encuentrosEsperados);
        
            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object, null, mockParticipantesRepository.Object);

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
            var mockParticipantesRepository = new Mock<IParticipantesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockEncuentrosRepository
                .Setup(r => r.ObtenerEncuentroPorId(fakeId))
                .Returns(fakeEncuentro);
            mockParticipantesRepository
                 .Setup(r => r.ObtenerParticipantePorId(fakeEncuentro.ParticipanteEncuentro.ToList()[0].ParticipanteId))
                 .Returns(fakeEncuentro.ParticipanteEncuentro.ToList()[0].Participante);

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object, null, mockParticipantesRepository.Object);

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

            mockParticipantesRepository
                 .Setup(r => r.ObtenerParticipantePorId(fakeEncuentro.ParticipanteEncuentro.ToList()[0].ParticipanteId))
                 .Returns(fakeEncuentro.ParticipanteEncuentro.ToList()[0].Participante);

            mockParticipantesRepository
                .Setup(r => r.ObtenerParticipantePorId(fakeEncuentro.ParticipanteEncuentro.ToList()[1].ParticipanteId))
                .Returns(fakeEncuentro.ParticipanteEncuentro.ToList()[1].Participante);

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object,
                mockDeportesRepository.Object, mockParticipantesRepository.Object);

            //Act
            businessLogic.AgregarEncuentro(fakeEncuentro);

            //Assert
            mockEncuentrosRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(EncuentroDataException))]
        public void AgregarEncuentroConDatosInvalidosTest()
        {
            var fakeEncuentro = TestHelper.ObtenerEncuentroSinDeporte();

            var mockParticipantesRepository = new Mock<IParticipantesRepository>();
            var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            var mockDeportesRepository = new Mock<IDeportesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockDeportesRepository
                   .Setup(r => r.ObtenerDeportePorNombre(fakeEncuentro.Deporte.Nombre))
                   .Returns(fakeEncuentro.Deporte);

            mockParticipantesRepository
                 .Setup(r => r.ObtenerParticipantePorId(fakeEncuentro.ParticipanteEncuentro.ToList()[0].ParticipanteId))
                 .Returns(fakeEncuentro.ParticipanteEncuentro.ToList()[0].Participante);

            mockParticipantesRepository
                .Setup(r => r.ObtenerParticipantePorId(fakeEncuentro.ParticipanteEncuentro.ToList()[1].ParticipanteId))
                .Returns(fakeEncuentro.ParticipanteEncuentro.ToList()[1].Participante);

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object,
                mockDeportesRepository.Object, mockParticipantesRepository.Object);

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
        public void AgregarEncuentroSinParticipanteErrorTest()
        {
            var fakeEncuentro = TestHelper.ObtenerEncuentroSinParticipantes();

            var mockParticipantesRepository = new Mock<IParticipantesRepository>();
            var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            var mockDeportesRepository = new Mock<IDeportesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockDeportesRepository
                 .Setup(r => r.ObtenerDeportePorNombre(fakeEncuentro.Deporte.Nombre))
                 .Returns(fakeEncuentro.Deporte);                   
            mockEncuentrosRepository.Setup(r => r.Insert(fakeEncuentro));

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object,
                mockDeportesRepository.Object, mockParticipantesRepository.Object);

            //Act
            businessLogic.AgregarEncuentro(fakeEncuentro);

            //Assert
            mockParticipantesRepository.VerifyAll();
        }
        
        [TestMethod]
        [ExpectedException(typeof(CantidadIncorrectaDePartcipantesException))]
        public void AgregarEncuentroConCantidadIncorrectaParticipantesError()
        {
            var fakeEncuentro = TestHelper.ObtenerEncuentroNumeroIncorrectoParticipantes();

            var mockParticipantesRepository = new Mock<IParticipantesRepository>();
            var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            var mockDeportesRepository = new Mock<IDeportesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockDeportesRepository
                 .Setup(r => r.ObtenerDeportePorNombre(fakeEncuentro.Deporte.Nombre))
                 .Returns(fakeEncuentro.Deporte);
            mockParticipantesRepository
                .Setup(r => r.ObtenerParticipantePorId(fakeEncuentro.ParticipanteEncuentro.ToList()[0].ParticipanteId))
                .Returns(fakeEncuentro.ParticipanteEncuentro.ToList()[0].Participante);
            mockEncuentrosRepository.Setup(r => r.Insert(fakeEncuentro));

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object,
                mockDeportesRepository.Object, mockParticipantesRepository.Object);

            //Act
            businessLogic.AgregarEncuentro(fakeEncuentro);

            //Assert
            mockParticipantesRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ParticipantesRepetidoException))]
        public void AgregarEncuentroParticipanteRepetido()
        {
            //Arrange
            var fakeEncuentro = TestHelper.ObtenerEncuentroParticipanteRepetido();

            var mockParticipantesRepository = new Mock<IParticipantesRepository>();
            var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            var mockDeportesRepository = new Mock<IDeportesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockDeportesRepository
                   .Setup(r => r.ObtenerDeportePorNombre(fakeEncuentro.Deporte.Nombre))
                   .Returns(fakeEncuentro.Deporte);

            mockParticipantesRepository
                 .Setup(r => r.ObtenerParticipantePorId(fakeEncuentro.ParticipanteEncuentro.ToList()[0].ParticipanteId))
                 .Returns(fakeEncuentro.ParticipanteEncuentro.ToList()[0].Participante);

            mockParticipantesRepository
                .Setup(r => r.ObtenerParticipantePorId(fakeEncuentro.ParticipanteEncuentro.ToList()[1].ParticipanteId))
                .Returns(fakeEncuentro.ParticipanteEncuentro.ToList()[1].Participante);

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object,
                mockDeportesRepository.Object, mockParticipantesRepository.Object);

            //Act
            businessLogic.AgregarEncuentro(fakeEncuentro);

            //Assert
            mockEncuentrosRepository.VerifyAll();
        }
             
        [TestMethod]
        public void BorrarEncuentroOkTest()
        {
            //Arrange
            var fakeEncuentro = TestHelper.ObtenerEncuentroFalso();
            var fakeEncuentroId = fakeEncuentro.Id;
            var mockParticipantesRepository = new Mock<IParticipantesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();

            mockEncuentrosRepository
                .Setup(r => r.ObtenerEncuentroPorId(fakeEncuentroId)).Returns(fakeEncuentro);

            mockParticipantesRepository
                 .Setup(r => r.ObtenerParticipantePorId(fakeEncuentro.ParticipanteEncuentro.ToList()[0].ParticipanteId))
                 .Returns(fakeEncuentro.ParticipanteEncuentro.ToList()[0].Participante);

            mockParticipantesRepository
                .Setup(r => r.ObtenerParticipantePorId(fakeEncuentro.ParticipanteEncuentro.ToList()[1].ParticipanteId))
                .Returns(fakeEncuentro.ParticipanteEncuentro.ToList()[1].Participante);
            mockEncuentrosRepository
                .Setup(r => r.BorrarEncuentro(fakeEncuentroId));

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object, null, mockParticipantesRepository.Object);

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
                .Setup(r => r.ObtenerEncuentroPorId(fakeEncuentroId)).Returns((Encuentro)null);

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object, null, null);

            //Act
            businessLogic.BorrarEncuentro(fakeEncuentroId);

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

        [TestMethod]
        [ExpectedException(typeof(NoCoincideDeporteException))]
        public void AgregarEncuentroNoCoincideDeporteErrorTest()
        {
            //Arrange
            var fakeEncuentro = TestHelper.ObtenerEncuentroFalso();

            var mockParticipantesRepository = new Mock<IParticipantesRepository>();
            var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            var mockDeportesRepository = new Mock<IDeportesRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockDeportesRepository
                   .Setup(r => r.ObtenerDeportePorNombre(fakeEncuentro.Deporte.Nombre))
                   .Returns(new Deporte { Nombre = "Tenis"});

            mockParticipantesRepository
                 .Setup(r => r.ObtenerParticipantePorId(fakeEncuentro.ParticipanteEncuentro.ToList()[0].ParticipanteId))
                 .Returns(fakeEncuentro.ParticipanteEncuentro.ToList()[0].Participante);

            mockParticipantesRepository
                .Setup(r => r.ObtenerParticipantePorId(fakeEncuentro.ParticipanteEncuentro.ToList()[1].ParticipanteId))
                .Returns(fakeEncuentro.ParticipanteEncuentro.ToList()[1].Participante);

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object,
                mockDeportesRepository.Object, mockParticipantesRepository.Object);

            //Act
            businessLogic.AgregarEncuentro(fakeEncuentro);

            //Assert
            mockEncuentrosRepository.VerifyAll();
        }
    }
}
