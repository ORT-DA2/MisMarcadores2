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

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object);

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

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object);

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

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object);

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

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object);

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

            var mockEncuentrosRepository = new Mock<IEncuentrosRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockEncuentrosRepository
                .Setup(r => r.Insert(fakeEncuentro));

            var businessLogic = new EncuentrosService(mockUnitOfWork.Object, mockEncuentrosRepository.Object);

            //Act
            businessLogic.AgregarEncuentro(fakeEncuentro);

            //Assert
            mockEncuentrosRepository.VerifyAll();
        }

    }
}
