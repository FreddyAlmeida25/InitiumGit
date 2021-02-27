using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Entity
{
    public class ClienteEntity
    {
        public int id { get; set; }
        public string cedula { get; set; }
        public string nombre { get; set; }
        public DateTime fechainicio { get; set; }
        public DateTime fechafin { get; set; }
        public int idcola { get; set; }
        public bool estado { get; set; }
    }
}
