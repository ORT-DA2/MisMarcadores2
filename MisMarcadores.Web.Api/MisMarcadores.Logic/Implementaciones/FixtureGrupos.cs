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
        private readonly List<Participante> participantes;

        public FixtureGrupos(DateTime fechaInicio, List<Participante> participantes)
        {
            this.fechaInicio = fechaInicio;
            this.participantes = participantes;
        }

        public override List<Encuentro> GenerarFixture() {
            int cantIteraciones = participantes.Count / 4;
            List<Participante> participantesGrupos = participantes.Take(4).ToList();
            List<Encuentro> encuentros = new List<Encuentro>();
            while (cantIteraciones > 0)
            {
                List<Encuentro> encuentrosGrupo = GenerarEncuentros(participantesGrupos);
                foreach (Encuentro encuentro in encuentrosGrupo)
                {
                    encuentros.Add(encuentro);
                }
                participantesGrupos = participantes.Skip(4).ToList();
                cantIteraciones--;
            }
            return encuentros;
        }

        private List<Encuentro> GenerarEncuentros(List<Participante> participantesGrupos)
        {
            List<Encuentro> encuentros = new List<Encuentro>();
            int fechaEncuentro = 0;
            for (int i = 0; i < participantesGrupos.Count; i++)
            {
                for (int j = 0; j < participantesGrupos.Count; j++)
                {
                    if (i != j)
                    {
                        Encuentro encuentro = new Encuentro
                        {
                            //ParticipanteLocal = participantesGrupos[i],
                            //ParticipanteVisitante = participantesGrupos[j],
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
