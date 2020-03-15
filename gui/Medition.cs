using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gui
{
    // Clase contenedora de los datos recibidos del dataset
    class Medition
    {

        public string fecha { get; set; }
        public string autoridad_ambiental { get; set; }
        public string nombre_de_la_estaci_n { get; set; }
        public string tecnolog_a { get; set; }
        public Double latitud { get; set; }
        public Double longitud { get; set; }
        public int c_digo_del_departamento { get; set; }
        public string departamento { get; set; }
        public int c_digo_del_municipio { get; set; }
        public string nombre_del_municipio { get; set; }
        public string tipo_de_estaci_n { get; set; }
        public int tiempo_de_exposici_n { get; set; }
        public string variable { get; set; }
        public string unidades { get; set; }
        public Double concentraci_n { get; set; }

    }
}
