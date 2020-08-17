using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marlin_Desafio_Backend_Junior.Models
{
    public class RegistroAlunoTurma
    {
        public int id { get; set; }
        public DateTime Data { get; set; }
        public string Staus { get; set; }
        public string Matricula { get; set; }
        public int TurmaId { get; set; }

    }
}
