using MisMarcadores.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisMarcadores.Logic.Tests
{
    public class TestHelper
    {
        public static Usuario ObtenerUsuarioFalso()
        {
            List<Usuario> usuarios = ObtenerUsuariosFalsos().ToList();
            return usuarios.FirstOrDefault();
        }

        public static IEnumerable<Usuario> ObtenerUsuariosFalsos()
        {
            return new List<Usuario>
            {
                new Usuario
                {
                    Nombre = "Andres",
                    Apellido = "Correa",
                    NombreUsuario = "acorrea",
                    Contraseña = "acorrea123",
                    Mail = "acorrea@gmail.com",
                    Administrador = true,
                },
                new Usuario
                {
                    Nombre = "Jorge",
                    Apellido = "Perez",
                    NombreUsuario = "jperez",
                    Contraseña = "jperez123",
                    Mail = "jperez@gmail.com",
                    Administrador = true,
                },
                new Usuario
                {
                    Nombre = "Carlos",
                    Apellido = "Gutierrez",
                    NombreUsuario = "cguti",
                    Contraseña = "cguti1234",
                    Mail = "cgutierrez@gmail.com",
                    Administrador = false,
                }
            };
        }

        public static Usuario ObtenerUsuarioMailFormatoErroneo()
        {
            return new Usuario
            {
                Nombre = "Andres",
                Apellido = "Correa",
                NombreUsuario = "acorrea",
                Contraseña = "acorrea123",
                Mail = "abcd",
                Administrador = true,
            };
        }
    }
}
