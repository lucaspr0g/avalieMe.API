using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AvalieMe.API.Models;
using AvalieMe.API.Services;

namespace AvalieMe.API.Controllers
{
    [Authorize]
    public class UsuarioController : BaseController
    {
        private Contexto db = new Contexto();

        // GET: api/Usuario
        public IQueryable<usuario> Getusuario()
        {
            return db.usuario;
        }

        // GET: api/Usuario/5
        [ResponseType(typeof(usuario))]
        [HttpGet]
        public async Task<IHttpActionResult> Getusuario(long id)
        {
            usuario usuario = await db.usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // GET: api/Usuario/Getusuario
        [ResponseType(typeof(usuario))]
        [HttpGet]
        public async Task<IHttpActionResult> Getusuario(string login, string senha)
        {
            if (string.IsNullOrWhiteSpace(login))
                return BadRequest("Preencha o login.");

            if (string.IsNullOrWhiteSpace(senha))
                return BadRequest("Preencha a senha.");

            login = login?.ToLower();

            var senhaEncrypt = Util.EncryptString(senha);

            var usuario = await db.usuario.FirstOrDefaultAsync(p => p.email == login && p.senha == senhaEncrypt);

            if (usuario == null)
                return BadRequest("Usuário ou senha inválidos.");

            usuario.senha = null;

            return Ok(usuario);
        }

        // PUT: api/Usuario/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public async Task<IHttpActionResult> Putusuario(long id, usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuario.id)
            {
                return BadRequest();
            }

            db.Entry(usuario).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!usuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Usuario
        [ResponseType(typeof(usuario))]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Postusuario(usuario usuario)
        {
            if (!ModelState.IsValid)
                return CustomBadRequest(ModelState);

            usuario.email = usuario.email?.ToString();

            if (usuarioExists(usuario.email))
                return BadRequest("Usuário já cadastrado.");

            if (usuario.senha.Length < 6)
                return BadRequest("A senha deve conter no mínimo 6 dígitos.");

            usuario.senha = Util.EncryptString(usuario.senha);
            usuario.inclusao = DateTime.Now;
            usuario.ativo = true;

            db.usuario.Add(usuario);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (usuarioExists(usuario.id))
                    return Conflict();

                return BadRequest("Erro ao cadastrar usuário.");
            }

            usuario.senha = null;

            return CreatedAtRoute("DefaultApi", new { id = usuario.id }, usuario);
        }

        // DELETE: api/Usuario/5
        [ResponseType(typeof(usuario))]
        public async Task<IHttpActionResult> Deleteusuario(long id)
        {
            usuario usuario = await db.usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            db.usuario.Remove(usuario);
            await db.SaveChangesAsync();

            return Ok(usuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool usuarioExists(long id)
        {
            return db.usuario.Count(e => e.id == id) > 0;
        }

        private bool usuarioExists(string email)
        {
            return db.usuario.Count(e => e.email == email) > 0;
        }
    }
}