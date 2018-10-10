using MisMarcadores.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MisMarcadores.Logic
{
    public abstract class Fixture
    {
        public abstract List<Encuentro> GenerarFixture();
    }
}
