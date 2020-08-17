using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Marlin_Desafio_Backend_Junior.Db;
using Marlin_Desafio_Backend_Junior.Models;

namespace Marlin_Desafio_Backend_Junior.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroAlunoTurmasController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public RegistroAlunoTurmasController(ApiDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Metodo de listagem dos registros de matricula dos alunos.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     GET api/registroalunoturmas
        ///     
        ///
        /// </remarks>
        // GET: api/RegistroAlunoTurmas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegistroAlunoTurma>>> GetRegistros()
        {
            return await _context.Registros.ToListAsync();
        }

       
    }
}
