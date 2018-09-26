using MisMarcadores.Data.Entities;
using MisMarcadores.Web.Api.Models;
using System;

namespace MisMarcadores.Data.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private MisMarcadoresContext context;
        private GenericRepository<Deporte> deporteRepository;
        private GenericRepository<Encuentro> encuentroRepository;
        private GenericRepository<Equipo> equipoRepository;
        private GenericRepository<Sesion> sesionRepository;
        private GenericRepository<Usuario> usuarioRepository;
        private GenericRepository<Comentario> comentarioRepository;

        public UnitOfWork(MisMarcadoresContext context) {
            this.context = context;
        }

        public GenericRepository<Deporte> DeporteRepository
        {
            get
            {

                if (this.deporteRepository == null)
                {
                    this.deporteRepository = new GenericRepository<Deporte>(context);
                }
                return deporteRepository;
            }
        }

        public GenericRepository<Encuentro> EncuentroRepository
        {
            get
            {

                if (this.encuentroRepository == null)
                {
                    this.encuentroRepository = new GenericRepository<Encuentro>(context);
                }
                return encuentroRepository;
            }
        }

        public GenericRepository<Sesion> SesionRepository
        {
            get
            {

                if (this.sesionRepository == null)
                {
                    this.sesionRepository = new GenericRepository<Sesion>(context);
                }
                return sesionRepository;
            }
        }

        public GenericRepository<Equipo> EquipoRepository
        {
            get
            {

                if (this.equipoRepository == null)
                {
                    this.equipoRepository = new GenericRepository<Equipo>(context);
                }
                return equipoRepository;
            }
        }

        public GenericRepository<Usuario> UsuarioRepository
        {
            get
            {

                if (this.usuarioRepository == null)
                {
                    this.usuarioRepository = new GenericRepository<Usuario>(context);
                }
                return usuarioRepository;
            }
        }

        public GenericRepository<Comentario> ComentarioRepository
        {
            get
            {

                if (this.comentarioRepository == null)
                {
                    this.comentarioRepository = new GenericRepository<Comentario>(context);
                }
                return comentarioRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}