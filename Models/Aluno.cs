using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marlin_Desafio_Backend_Junior.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Matricula { get; set; }
        public string Nome { get; set; }

        public int idTurma { get; set; }
        public Turma Turma { get; set; }

    }
}
