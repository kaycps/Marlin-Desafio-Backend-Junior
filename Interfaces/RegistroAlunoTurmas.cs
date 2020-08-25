using Marlin_Desafio_Backend_Junior.Db;
using Marlin_Desafio_Backend_Junior.Models;
using Marlin_Desafio_Backend_Junior.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Marlin_Desafio_Backend_Junior.Interfaces
{
    public class RegistroAlunoTurmas : IRegistroAlunoTurmas
    {
        private readonly ApiDbContext _context;

        public RegistroAlunoTurmas(ApiDbContext context)
        {
            _context = context;
        }
        public void CriarRegistro(Aluno aluno)
        {
            RegistroAlunoTurma registroEntrada = new RegistroAlunoTurma()
            {
                Data = DateTime.Today,
                Matricula = aluno.Matricula,
                Staus = EnumRegistro.Ingressou.ToString(),
                TurmaId = aluno.idTurma
            };

            _context.Registros.Add(registroEntrada);
            _context.SaveChanges();
        }
    }
}
