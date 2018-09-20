using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            mockUsuariosRepository
                .Setup(r => r.Agregar(fakeUsuario));

            var businessLogic = new UsuariosLogic(mockUsuariosRepository.Object);

            //Act
            businessLogic.AgregarUsuario(fakeUsuario);

        }
    }
}
