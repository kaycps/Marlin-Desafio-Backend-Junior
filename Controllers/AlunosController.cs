using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Marlin_Desafio_Backend_Junior.Db;
using Marlin_Desafio_Backend_Junior.Models;
using Microsoft.AspNetCore.Authorization;
using Marlin_Desafio_Backend_Junior.Services;
using Marlin_Desafio_Backend_Junior.Interfaces;

namespace Marlin_Desafio_Backend_Junior.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AlunosController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IRegistroAlunoTurmas _registroAluno;
        public AlunosController(ApiDbContext context)
        {
            _context = context;
            _registroAluno = new RegistroAlunoTurmas(context);

        }

        /// <summary>
        /// Metodo de listagem de todos alunos.
        /// </summary>        
        // GET: api/Alunos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunos()
        {
            return await _context.Alunos.ToListAsync();
        }

        /// <summary>
        /// Metodo de listagem/aluno, com base no id.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     Get api/alunos{id}
        ///     
        ///
        /// </remarks>
        // GET: api/Alunos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Aluno>> GetAluno(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);

            if (aluno == null)
            {
                return NotFound();
            }

            return aluno;
        }

        /// <summary>
        /// Metodo de edição/aluno.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     PUT api/alunos/id
        ///     {
        ///        
        ///        "id": 1,
        ///        "matricula": "51362",
        ///        "nome": "kayc prado",
        ///        "idturma":1
        ///        
        ///     }
        ///
        /// </remarks>
        /// <param id="id"></param>
        // PUT: api/Alunos/5       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluno(int id, Aluno aluno)
        {
            if (id != aluno.Id)
            {
                return BadRequest();
            }

            if (!AlunoExists(id))
            {               
                return NotFound();
            }

            if (GetAlunoId(id).idTurma != aluno.idTurma)
            {
                _registroAluno.CriarRegistro(aluno);
            }            

            _context.Entry(aluno).State = EntityState.Modified;

            try
            {                
                await _context.SaveChangesAsync();          
                
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlunoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }          

            return NoContent();
        }

        /// <summary>
        /// Metodo de criação/aluno.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     Post api/alunos
        ///     {
        ///                
        ///        "matricula": "51362",
        ///        "nome": "kayc prado",
        ///        "idturma":1
        ///        
        ///     }
        ///
        /// </remarks>
        // POST: api/Alunos        
        [HttpPost]        
        public async Task<ActionResult<Aluno>> PostAluno([Bind("Id")]int Id,Aluno aluno)
        {
            if (AlunoExists(aluno.Matricula))
            {
                ModelState.AddModelError(string.Empty, "Numero de matricula ja cadastrada!");
                return BadRequest(ModelState);
                
            }

            if (!_context.Turmas.Any(e => e.Id == Id))
            {
                ModelState.AddModelError(string.Empty, "Impossivel cadastrar aluno, Turma indexistente!");
                return BadRequest(ModelState);
                
            }

            var turma = _context.Turmas.Include(e=>e.Alunos)
                                        .Where(e=>e.Id==Id)                                        
                                        .First();

            if (turma.Alunos!=null)
            {
                if (turma.Alunos.Count() >= 5)
                {
                    ModelState.AddModelError(string.Empty, "Numero de alunos na turma atigiu o limite maximo!");
                    return BadRequest(ModelState);
                    
                }
            }            

            
            aluno.Turma = turma;

            RegistroAlunoTurma registro = new RegistroAlunoTurma()
            {
                Data = DateTime.Now,
                Matricula = aluno.Matricula,
                Staus = EnumRegistro.Ingressou.ToString(),
                TurmaId = turma.Id
            };

            _context.Registros.Add(registro);
            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAluno", new { id = aluno.Id }, aluno);
        }

        /// <summary>
        /// Metodo de exclusão/aluno.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     DELETE api/alunos/id
        ///    
        ///
        /// </remarks>
        // DELETE: api/Alunos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Aluno>> DeleteAluno(int id)
        {
           
            if (!AlunoExists(id))
            {
                ModelState.AddModelError(string.Empty, "Aluno inexistente!");
                return NotFound(ModelState);
                
            }

            var aluno = await _context.Alunos.Include(e => e.Turma)
                                               .Where(e => e.Id == id)
                                               .FirstAsync();

            if (aluno.Turma != null)
            {
                ModelState.AddModelError(string.Empty, "Não é possivel excluir aluno matriculado!");
                return BadRequest(ModelState);
                
            }
            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();

            return aluno;
        }

        private bool AlunoExists(int id)
        {
            return _context.Alunos.Any(e => e.Id == id);
        }

        private bool AlunoExists(string matricula)
        {
            return _context.Alunos.Any(e => e.Matricula == matricula);
        }

        private Aluno GetAlunoId(int id)
        {
            return _context.Alunos.AsNoTracking()
                                  .FirstOrDefault(e => e.Id == id);
        }
    }
}
