using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using MisMarcadores.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MisMarcadores.Repository
{
    public class UsuariosRepository : GenericRepository<Usuario>, IUsuariosRepository
    {
        public UsuariosRepository(MisMarcadoresContext context) : base(context) {}
    }
}