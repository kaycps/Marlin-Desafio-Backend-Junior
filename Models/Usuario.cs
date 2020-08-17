using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marlin_Desafio_Backend_Junior.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }
    }
}
