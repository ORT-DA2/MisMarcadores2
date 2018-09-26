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
    public class UsuariosLogicTest
    {
        [TestMethod]
        public void CrearUsuarioOkTest()
        {
            //Arrange
            var fakeUsuario = TestHelper.ObtenerUsuarioFalso();

            var mockUsuariosRepository = new Mock<IUsuariosRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUsuariosRepository
                .Setup(r => r.Insert(fakeUsuario));

            var businessLogic = new UsuariosService(mockUnitOfWork.Object,mockUsuariosRepository.Object);

            //Act
            businessLogic.AgregarUsuario(fakeUsuario);

            //Assert
            mockUsuariosRepository.VerifyAll();

        }

        [TestMethod]
        [ExpectedException(typeof(UsuarioDataException))]
        public void AgregarUsuarioMailFormatoErroneoTest()
        {
            var fakeUsuario = TestHelper.ObtenerUsuarioMailFormatoErroneo();
            var mockUsuariosRepository = new Mock<IUsuariosRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUsuariosRepository
                .Setup(r => r.Insert(fakeUsuario));

            var businessLogic = new UsuariosService(mockUnitOfWork.Object, mockUsuariosRepository.Object);

            //Act
            businessLogic.AgregarUsuario(fakeUsuario);

            //Assert
            mockUsuariosRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(UsuarioDataException))]
        public void AgregarUsuarioNombreVacio()
        {
            var fakeUsuario = TestHelper.ObtenerUsuarioNombreVacio();
            var mockUsuariosRepository = new Mock<IUsuariosRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUsuariosRepository
                .Setup(r => r.Insert(fakeUsuario));

            var businessLogic = new UsuariosService(mockUnitOfWork.Object, mockUsuariosRepository.Object);

            //Act
            businessLogic.AgregarUsuario(fakeUsuario);

            //Assert
            mockUsuariosRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(UsuarioDataException))]
        public void AgregarUsuarioApellidoVacio()
        {
            var fakeUsuario = TestHelper.ObtenerUsuarioApellidoVacio();
            var mockUsuariosRepository = new Mock<IUsuariosRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUsuariosRepository
                .Setup(r => r.Insert(fakeUsuario));

            var businessLogic = new UsuariosService(mockUnitOfWork.Object, mockUsuariosRepository.Object);

            //Act
            businessLogic.AgregarUsuario(fakeUsuario);

            //Assert
            mockUsuariosRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(UsuarioDataException))]
        public void AgregarUsuarioContraseñaVacia()
        {
            var fakeUsuario = TestHelper.ObtenerUsuarioContraseñaVacia();
            var mockUsuariosRepository = new Mock<IUsuariosRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUsuariosRepository
                .Setup(r => r.Insert(fakeUsuario));

            var businessLogic = new UsuariosService(mockUnitOfWork.Object, mockUsuariosRepository.Object);

            //Act
            businessLogic.AgregarUsuario(fakeUsuario);

            //Assert
            mockUsuariosRepository.VerifyAll();
        }


    }
}
