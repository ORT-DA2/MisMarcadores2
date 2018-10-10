using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MisMarcadores.Data.Entities;

namespace MisMarcadores.Logic
{
    public class FixtureGrupos : Fixture
    {
        private readonly DateTime fechaInicio;
        private readonly List<Equipo> equipos;

        public FixtureGrupos(DateTime fechaInicio, List<Equipo> equipos)
        {
            this.fechaInicio = fechaInicio;
            this.equipos = equipos;
        }

        public override List<Encuentro> GenerarFixture() {
            int cantIteraciones = equipos.Count / 4;
            List<Equipo> equiposGrupos = equipos.Take(4).ToList();
            List<Encuentro> encuentros = new List<Encuentro>();
            while (cantIteraciones > 0)
            {
                List<Encuentro> encuentrosGrupo = GenerarEncuentros(equiposGrupos);
                foreach (Encuentro encuentro in encuentrosGrupo)
                {
                    encuentros.Add(encuentro);
                }
                equiposGrupos = equipos.Skip(4).ToList();
                cantIteraciones--;
            }
            return encuentros;
        }

        private List<Encuentro> GenerarEncuentros(List<Equipo> equiposGrupos)
        {
            List<Encuentro> encuentros = new List<Encuentro>();
            int fechaEncuentro = 0;
            for (int i = 0; i < equiposGrupos.Count; i++)
            {
                for (int j = 0; j < equiposGrupos.Count; j++)
                {
                    if (i != j)
                    {
                        Encuentro encuentro = new Encuentro
                        {
                            EquipoLocal = equiposGrupos[i],
                            EquipoVisitante = equiposGrupos[j],
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
