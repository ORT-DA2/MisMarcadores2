using System;

namespace MisMarcadores.Repository
{
    public interface IRepository<T>
    {
        void Agregar(T elemento);
        T Obtener(Guid id);
        void Borrar(Guid id);
        void Modificar(T elemento);
    }
}
