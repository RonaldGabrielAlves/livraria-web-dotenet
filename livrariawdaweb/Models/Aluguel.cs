using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace livrariawdaweb.Models
{
    public class Aluguel
    {
        public int idalug { get; set; }
        public int livroalu { get; set; }
        public int clientealu { get; set; }
        public string dataalu { get; set; }
        public string dataprevdev { get; set; }
        public string datadev { get; set; }
    }
}
