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
    public class UsuariosLogicTest
    {

        [TestMethod]
        public void ObtenerUsuariosOkTest()
        {
            //Arrange
            var usuariosEsperados = TestHelper.ObtenerUsuariosFalsos();

            var mockUsuariosRepository = new Mock<IUsuariosRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUsuariosRepository
                .Setup(r => r.ObtenerUsuarios())
                .Returns(usuariosEsperados);

            var businessLogic = new UsuariosService(mockUnitOfWork.Object, mockUsuariosRepository.Object);

            //Act
            IEnumerable<Usuario> obtainedResult = businessLogic.ObtenerUsuarios();

            //Assert
            mockUsuariosRepository.VerifyAll();
            Assert.IsNotNull(obtainedResult);
            Assert.AreEqual(usuariosEsperados, obtainedResult);
        }

        [TestMethod]
        public void AgregarUsuarioOkTest()
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
        public void AgregarUsuarioNombreVacioTest()
        {
            var fakeUsuario = TestHelper.ObtenerUsuarioNombreVacio();
            var mockUsuariosRepository = new Mock<IUsuariosRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUsuariosRepository
                .Setup(r => r.Insertar(fakeUsuario));

            var businessLogic = new UsuariosService(mockUnitOfWork.Object, mockUsuariosRepository.Object);

            //Act
            businessLogic.AgregarUsuario(fakeUsuario);

            //Assert
            mockUsuariosRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(UsuarioDataException))]
        public void AgregarUsuarioApellidoVacioTest()
        {
            var fakeUsuario = TestHelper.ObtenerUsuarioApellidoVacio();
            var mockUsuariosRepository = new Mock<IUsuariosRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUsuariosRepository
                .Setup(r => r.Insertar(fakeUsuario));

            var businessLogic = new UsuariosService(mockUnitOfWork.Object, mockUsuariosRepository.Object);

            //Act
            businessLogic.AgregarUsuario(fakeUsuario);

            //Assert
            mockUsuariosRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(UsuarioDataException))]
        public void AgregarUsuarioContraseñaVaciaTest()
        {
            var fakeUsuario = TestHelper.ObtenerUsuarioContraseñaVacia();
            var mockUsuariosRepository = new Mock<IUsuariosRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUsuariosRepository
                .Setup(r => r.Insertar(fakeUsuario));

            var businessLogic = new UsuariosService(mockUnitOfWork.Object, mockUsuariosRepository.Object);

            //Act
            businessLogic.AgregarUsuario(fakeUsuario);

            //Assert
            mockUsuariosRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(UsuarioDataException))]
        public void AgregarUsuarioNombreUsuarioVacioTest()
        {
            var fakeUsuario = TestHelper.ObtenerUsuarioNombreUsuarioVacio();
            var mockUsuariosRepository = new Mock<IUsuariosRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUsuariosRepository
                .Setup(r => r.Insertar(fakeUsuario));

            var businessLogic = new UsuariosService(mockUnitOfWork.Object, mockUsuariosRepository.Object);

            //Act
            businessLogic.AgregarUsuario(fakeUsuario);

            //Assert
            mockUsuariosRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ExisteUsuarioException))]
        public void AgregarUsuarioExistenteTest()
        {
            var fakeUsuario = TestHelper.ObtenerUsuarioFalso();
            var mockUsuariosRepository = new Mock<IUsuariosRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUsuariosRepository
                .Setup(r => r.Insertar(fakeUsuario))
                .Throws(new UsuarioRepositoryException());

            var businessLogic = new UsuariosService(mockUnitOfWork.Object, mockUsuariosRepository.Object);

            //Act
            businessLogic.AgregarUsuario(fakeUsuario);

            //Assert
            mockUsuariosRepository.VerifyAll();
        }


    }
}
