using Microsoft.VisualStudio.TestTools.UnitTesting;
using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using MisMarcadores.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace MisMarcadores.Logic.Tests
{
    [TestClass]
    public class SesionesLogicTest
    {
        [TestMethod]
        public void ObtenerSesionPorTokenTestOk()
        {
            //Arrange
            var fakeSesion = TestHelper.ObtenerSesionFalsa();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockSesionesRepository = new Mock<ISesionesRepository>();
            mockSesionesRepository
                .Setup(r => r.ObtenerPorToken(fakeSesion.Token))
                .Returns(fakeSesion);

            var businessLogic = new SesionesService(mockUnitOfWork.Object, mockSesionesRepository.Object);

            //Act
            Sesion obtainedResult = businessLogic.ObtenerPorToken(fakeSesion.Token);

            //Assert
            mockSesionesRepository.VerifyAll();
            Assert.IsNotNull(obtainedResult);
            Assert.AreEqual(fakeSesion, obtainedResult);
        }

        [TestMethod]
        public void ObtenerSesionPorTokenErrorNotFoundTest()
        {
            //Arrange
            var fakeSesion = TestHelper.ObtenerSesionFalsa();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockSesionesRepository = new Mock<ISesionesRepository>();
            mockSesionesRepository
                .Setup(r => r.ObtenerPorToken(fakeSesion.Token))
                .Returns((Sesion)null);

            var businessLogic = new SesionesService(mockUnitOfWork.Object, mockSesionesRepository.Object);

            //Act
            Sesion obtainedResult = businessLogic.ObtenerPorToken(fakeSesion.Token);

            //Assert
            mockSesionesRepository.VerifyAll();
            Assert.IsNull(obtainedResult);
        }

        [TestMethod]
        public void IniciarSesionTestOk()
        {
            //Arrange
            var fakeSesion = TestHelper.ObtenerSesionFalsa();
            var fakeUsuario = fakeSesion.NombreUsuario;
            var fakeContraseña = "acorrea123";

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockSesionesRepository = new Mock<ISesionesRepository>();
            mockSesionesRepository
                .Setup(r => r.CredencialesValidas(fakeUsuario, fakeContraseña))
                .Returns(true);
            mockSesionesRepository
                .Setup(r => r.AgregarSesion(It.IsAny<Sesion>()));

            var businessLogic = new SesionesService(mockUnitOfWork.Object, mockSesionesRepository.Object);

            //Act
            Sesion obtainedResult = businessLogic.Login(fakeUsuario, fakeContraseña);

            //Assert
            mockSesionesRepository.VerifyAll();
            Assert.IsNotNull(obtainedResult);
            Assert.AreEqual(fakeSesion.NombreUsuario, obtainedResult.NombreUsuario);
        }

        [TestMethod]
        public void IniciarSesionInvalidaErrorTest()
        {
            //Arrange
            var fakeSesion = TestHelper.ObtenerSesionFalsa();
            var fakeUsuario = fakeSesion.NombreUsuario;
            var fakeContraseña = "acorrea123";

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockSesionesRepository = new Mock<ISesionesRepository>();
            mockSesionesRepository
                .Setup(r => r.CredencialesValidas(fakeUsuario, fakeContraseña))
                .Returns(false);

            var businessLogic = new SesionesService(mockUnitOfWork.Object, mockSesionesRepository.Object);

            //Act
            Sesion obtainedResult = businessLogic.Login(fakeUsuario, fakeContraseña);

            //Assert
            mockSesionesRepository.VerifyAll();
            Assert.IsNull(obtainedResult);
        }

        [TestMethod]
        public void CerrarSesionTestOK()
        {
            //Arrange
            var fakeSesion = TestHelper.ObtenerSesionFalsa();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockSesionesRepository = new Mock<ISesionesRepository>();
            mockSesionesRepository
                .Setup(r => r.BorrarSesion(fakeSesion.Token));

            var businessLogic = new SesionesService(mockUnitOfWork.Object, mockSesionesRepository.Object);

            //Act
            businessLogic.Logout(fakeSesion.Token);

            //Assert
            mockSesionesRepository.VerifyAll();
        }
    }
}
