using System;
using System.Collections.Generic;
using System.Text;
using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using MisMarcadores.Repository;

namespace MisMarcadores.Logic
{
    public class DeportesService : IDeportesService
    {
        IUnitOfWork _unitOfWork;
        IDeportesRepository _deportesRepository;

        public DeportesService(IUnitOfWork unitOfWork, IDeportesRepository deportesRepository)
        {
            _unitOfWork = unitOfWork;
            _deportesRepository = deportesRepository;
        }

        public void AgregarDeporte(Deporte deporte)
        {
            throw new NotImplementedException();
        }

        public void BorrarDeporte(string nombre)
        {
            throw new NotImplementedException();
        }

        public void ModificarDeporte(string nombre, Deporte deporte)
        {
            throw new NotImplementedException();
        }

        public Deporte ObtenerDeportePorNombre(string nombre)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Deporte> ObtenerDeportes()
        {
            throw new NotImplementedException();
        }
    }
}
