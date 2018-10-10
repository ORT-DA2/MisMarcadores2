using System;
using System.Collections.Generic;
using System.Text;
using MisMarcadores.Data.Entities;

namespace MisMarcadores.Logic
{
    public class FixtureLiga : Fixture
    {
        private readonly DateTime fechaInicio;
        private readonly List<Equipo> equipos;
        
        public FixtureLiga(DateTime fechaInicio, List<Equipo> equipos)
        {
            this.fechaInicio = fechaInicio;
            this.equipos = equipos;
        }

        public override List<Encuentro> GenerarFixture()
        {
            List<Encuentro> encuentros = new List<Encuentro>();
            int fechaEncuentro = 0;
            for (int i = 0; i < equipos.Count; i++)
            {
                for (int j = 0; j < equipos.Count; j++)
                {
                    if (i != j)
                    {
                        Encuentro encuentro = new Encuentro
                        {
                            EquipoLocal = equipos[i],
                            EquipoVisitante = equipos[j],
                            FechaHora = fechaInicio.AddDays(fechaEncuentro)
                        };
                        encuentros.Add(encuentro);
                        fechaEncuentro += 3;
                    }
                }
            }
            return encuentros;
        }
    }
}
