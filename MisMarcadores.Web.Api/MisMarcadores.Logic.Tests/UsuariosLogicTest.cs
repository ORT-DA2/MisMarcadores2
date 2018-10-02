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
                .Setup(r => r.GetAll())
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
        public void ObtenerUsuariosNullTest()
        {
            //Arrange
            List<Usuario> usuariosEsperados = null;

            var mockUsuariosRepository = new Mock<IUsuariosRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUsuariosRepository
                .Setup(r => r.GetAll())
                .Returns(usuariosEsperados);

            var businessLogic = new UsuariosService(mockUnitOfWork.Object, mockUsuariosRepository.Object);

            //Act
            IEnumerable<Usuario> obtainedResult = businessLogic.ObtenerUsuarios();

            //Assert
            mockUsuariosRepository.VerifyAll();
            Assert.IsNull(obtainedResult);
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
        public void ObtenerUsuarioPorNombreUsuarioOkTest()
        {
            //Arrange
            var fakeUsuario = TestHelper.ObtenerUsuarioFalso();
            var fakeNombreUsuario = "acorrea";

            var mockUsuariosRepository = new Mock<IUsuariosRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUsuariosRepository
                .Setup(r => r.ObtenerPorNombreUsuario(fakeNombreUsuario))
                .Returns(fakeUsuario);

            var businessLogic = new UsuariosService(mockUnitOfWork.Object, mockUsuariosRepository.Object);

            //Act
            Usuario obtainedResult = businessLogic.ObtenerPorNombreUsuario(fakeNombreUsuario);

            //Assert
            mockUsuariosRepository.VerifyAll();
            Assert.IsNotNull(obtainedResult);
            Assert.AreEqual(fakeNombreUsuario, obtainedResult.NombreUsuario);
        }

        [TestMethod]
        public void ObtenerUsuarioPorNombreUsuarioErrorNotFoundTest()
        {
            //Arrange
            var fakeNombreUsuario = "pepe";

            var mockUsuariosRepository = new Mock<IUsuariosRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUsuariosRepository
                .Setup(r => r.ObtenerPorNombreUsuario(fakeNombreUsuario))
                .Returns((Usuario)null);

            var businessLogic = new UsuariosService(mockUnitOfWork.Object, mockUsuariosRepository.Object);

            //Act
            Usuario obtainedResult = businessLogic.ObtenerPorNombreUsuario(fakeNombreUsuario);

            //Assert
            mockUsuariosRepository.VerifyAll();
            Assert.IsNull(obtainedResult);
        }



        [TestMethod]
        [ExpectedException(typeof(UsuarioDataException))]
        public void AgregarUsuarioMailFormatoErroneoTest()
        {
            var fakeUsuario = TestHelper.ObtenerUsuarioMailFormatoErroneo();
            var mockUsuariosRepository = new Mock<IUsuariosRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

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

            var businessLogic = new UsuariosService(mockUnitOfWork.Object, mockUsuariosRepository.Object);

            //Act
            businessLogic.AgregarUsuario(fakeUsuario);

            //Assert
            mockUsuariosRepository.VerifyAll();
        }

        [TestMethod]
        public void ActualizarUsuarioExistenteOkTest()
        {
            //Arrange
            var fakeUsuario = TestHelper.ObtenerUsuarioFalso();
            var fakeNombreUsuario = fakeUsuario.NombreUsuario;
            var mockUsuariosRepository = new Mock<IUsuariosRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUsuariosRepository.Setup(r => r.ObtenerPorNombreUsuario(fakeNombreUsuario)).Returns(fakeUsuario);
            mockUsuariosRepository.Setup(r => r.ModificarUsuario(fakeUsuario));

            var businessLogic = new UsuariosService(mockUnitOfWork.Object, mockUsuariosRepository.Object);

            //Act
            businessLogic.ModificarUsuario(fakeNombreUsuario, fakeUsuario);

            //Assert
            mockUsuariosRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(UsuarioDataException))]
        public void ActualizarUsuarioDatosErroneosTest()
        {
            //Arrange
            var fakeUsuario = TestHelper.ObtenerUsuarioNombreVacio();
            var fakeNombreUsuario = fakeUsuario.NombreUsuario;
            var mockUsuariosRepository = new Mock<IUsuariosRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var businessLogic = new UsuariosService(mockUnitOfWork.Object, mockUsuariosRepository.Object);

            //Act
            businessLogic.ModificarUsuario(fakeNombreUsuario, fakeUsuario);

            //Assert
            mockUsuariosRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NoExisteUsuarioException))]
        public void ActualizarUsuarioNoExistenteTest()
        {
            //Arrange
            var fakeUsuario = TestHelper.ObtenerUsuarioFalso();
            var fakeNombreUsuario = fakeUsuario.NombreUsuario;
            var mockUsuariosRepository = new Mock<IUsuariosRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUsuariosRepository
                .Setup(r => r.ObtenerPorNombreUsuario(fakeNombreUsuario))
                .Returns((Usuario)null);

            var businessLogic = new UsuariosService(mockUnitOfWork.Object, mockUsuariosRepository.Object);

            //Act
            businessLogic.ModificarUsuario(fakeNombreUsuario, fakeUsuario);

            //Assert
            mockUsuariosRepository.VerifyAll();
        }

        [TestMethod]
        public void BorrarUsuarioOkTest()
        {
            //Arrange
            var fakeUsuario = TestHelper.ObtenerUsuarioFalso();
            var fakeNombreUsuario = fakeUsuario.NombreUsuario;

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockUsuariosRepository = new Mock<IUsuariosRepository>();
            mockUsuariosRepository
                .Setup(r => r.ObtenerPorNombreUsuario(fakeNombreUsuario)).Returns(fakeUsuario);
            mockUsuariosRepository
                .Setup(r => r.BorrarUsuario(fakeUsuario.Id));

            var businessLogic = new UsuariosService(mockUnitOfWork.Object, mockUsuariosRepository.Object);

            //Act
            businessLogic.BorrarUsuario(fakeNombreUsuario);

            //Assert
            mockUsuariosRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NoExisteUsuarioException))]
        public void BorrarUsuarioNoExistenteErrorTest()
        {
            //Arrange
            var fakeUsuario = TestHelper.ObtenerUsuarioFalso();
            var fakeNombreUsuario = fakeUsuario.NombreUsuario;

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockUsuariosRepository = new Mock<IUsuariosRepository>();
            mockUsuariosRepository
               .Setup(r => r.ObtenerPorNombreUsuario(fakeNombreUsuario))
               .Returns((Usuario)null);

            var businessLogic = new UsuariosService(mockUnitOfWork.Object, mockUsuariosRepository.Object);

            //Act
            businessLogic.BorrarUsuario(fakeNombreUsuario);

            //Assert
            mockUsuariosRepository.VerifyAll();
        }

    }
}
