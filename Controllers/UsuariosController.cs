using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Marlin_Desafio_Backend_Junior.Db;
using Marlin_Desafio_Backend_Junior.Models;
using Marlin_Desafio_Backend_Junior.Services;
using Microsoft.AspNetCore.Authorization;

namespace Marlin_Desafio_Backend_Junior.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public UsuariosController(ApiDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Metodo de login.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST /login
        ///     {
        ///        
        ///        "login": "marlin",
        ///        "senha": "marlin"
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public  ActionResult<dynamic> Login(Usuario usuario)
        {
            bool result = _context.Usuarios.AnyAsync(u => u.Login == usuario.Login).Result;

            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Usuario inexistente!");
                return NotFound(ModelState);
                
            }

            var usuarioLogin = _context.Usuarios.Where(u => u.Login == usuario.Login).FirstOrDefault();

            if (usuario.Senha != usuarioLogin.Senha)
            {
                ModelState.AddModelError(string.Empty, "Senha incorreta!");
                return BadRequest(ModelState);
                
            }

            var token = TokenServices.GerarToken(usuario);

            usuarioLogin.Senha = "";
            usuario.Senha = "";

            return new
            {
                Usuario = new Usuario
                {
                    Id = usuarioLogin.Id,
                    Login = usuarioLogin.Login,
                    Senha = usuarioLogin.Senha,
                    Role = usuarioLogin.Role
                },

                Token = token
            };            

            
        }
        
    }
}
