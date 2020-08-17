using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marlin_Desafio_Backend_Junior.Models
{
    public class Turma
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<Aluno> Alunos { get; set; }

    }
}
