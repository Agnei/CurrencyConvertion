using System;
using System.Collections.Generic;
using System.Text;

namespace ConversorMoeda.Application.Entities
{
    public class ListaMoedaModelView
    {
            public bool success { get; set; }
            public string terms { get; set; }
            public string privacy { get; set; }
            public object Currencies { get; set; }
    }
}
