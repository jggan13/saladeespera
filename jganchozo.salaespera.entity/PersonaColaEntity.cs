using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jganchozo.salaespera.entity
{
    public class PersonaColaEntity
    {
        public int PersonaColaID { get; set; }
        public int ColaID { get; set; }
        public string ColaNombre { get; set; }
        public int PersonaID { get; set; }
        public string PersonaNombre { get; set; }
        public string IdPersona { get; set; }
        public bool EstadoAtencion { get; set; }
        public DateTime HoraIn { get; set; }
        public DateTime HoraOut { get; set; }
        public string NumeroAtencion { get; set; }
        public int NumeroCola { get; set; }
    }
}
