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

namespace Marlin_Desafio_Backend_Junior.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TurmasController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public TurmasController(ApiDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Metodo de listagem de todas turmas.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     GET api/alunos
        ///     
        ///
        /// </remarks>
        // GET: api/Turmas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Turma>>> GetTurmas()
        {
            return await _context.Turmas.Include(e=>e.Alunos).ToListAsync();
        }

        /// <summary>
        /// Metodo de listagem/turma, com base no id.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     GET api/turmas/{id}
        ///    
        ///
        /// </remarks>
        // GET: api/Turmas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Turma>> GetTurma(int id)
        {
            var turma = await _context.Turmas.FindAsync(id);

            if (turma == null)
            {
                return NotFound();
            }

            return turma;
        }

        /// <summary>
        /// Metodo de edição/turma.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     PUT api/turmas/{id}
        ///     {
        ///        
        ///        "id": 3,
        ///        "nome":"frances"
        ///        
        ///     }
        ///
        /// </remarks>
        // PUT: api/Turmas/5        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTurma(int id, Turma turma)
        {
            if (id != turma.Id)
            {
                return BadRequest();
            }

            _context.Entry(turma).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TurmaExists(id))
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
        /// Metodo de criação/turma.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST api/turmas
        ///     {
        ///        
        ///        "nome":"ingles"
        ///        
        ///     }
        ///
        /// </remarks>
        // POST: api/Turmas       
        [HttpPost]
        public async Task<ActionResult<Turma>> PostTurma(Turma turma)
        {
            _context.Turmas.Add(turma);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTurma", new { id = turma.Id }, turma);
        }

        /// <summary>
        /// Metodo de exclusão/turma.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     DELETE api/turmas/{id}
        ///             
        /// </remarks>
        // DELETE: api/Turmas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Turma>> DeleteTurma(int id)
        {
            if (!TurmaExists(id))
            {
                ModelState.AddModelError(string.Empty, "Turma não encontrada!");
                return NotFound(ModelState);
            }
            var turma = await _context.Turmas.Where(e=>e.Id==id)
                                              .Include(e => e.Alunos)
                                              .FirstAsync();
            

            if (turma.Alunos.Count()>=1)
            {
                ModelState.AddModelError(string.Empty, "Não foi possivel excluir a turma pois existe(m) aluno(s) cadastrado(s)!");
                return BadRequest(ModelState);
                
            }
            _context.Turmas.Remove(turma);
            await _context.SaveChangesAsync();

            return turma;
        }

        private bool TurmaExists(int id)
        {
            return _context.Turmas.Any(e => e.Id == id);
        }
    }
}
