using MisMarcadores.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MisMarcadores.Logic
{
    public interface IFixture
    {
        List<Encuentro> GenerarFixture();
    }
}
