using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AvalieMe.API.Models;

namespace AvalieMe.API.Controllers
{
    [Authorize]
    public class TesteController : BaseController
    {
        private Contexto db = new Contexto();

        // GET: api/Teste
        public IQueryable<teste> Getteste()
        {
            return db.teste;
        }

        // GET: api/Teste/5
        [ResponseType(typeof(teste))]
        public async Task<IHttpActionResult> Getteste(long id)
        {
            teste teste = await db.teste.FindAsync(id);
            if (teste == null)
            {
                return NotFound();
            }

            return Ok(teste);
        }

        // PUT: api/Teste/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putteste(long id, teste teste)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teste.id)
            {
                return BadRequest();
            }

            db.Entry(teste).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!testeExists(id))
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

        // POST: api/Teste
        [ResponseType(typeof(teste))]
        public async Task<IHttpActionResult> Postteste(NovoTesteModel novoTesteModel)
        {
            if (string.IsNullOrWhiteSpace(novoTesteModel.Categoria))
                return BadRequest("Selecione a categoria.");

            if (novoTesteModel.UserId < 0)
                return BadRequest("Usuário não encontrado.");

            var teste = new teste()
            {
                ativo = true,
                categoria = novoTesteModel.Categoria,
                inclusao = DateTime.Now,
                multiplasPessoas = novoTesteModel.MultiplasPessoas,
                nomeImagem = $"img-{novoTesteModel.UserId}-{DateTime.Now:ddMMyyyyHHmmss}.png",
                posicaoPessoa = novoTesteModel.PosicaoPessoa,
                usuarioId = novoTesteModel.UserId
            };

            var caminho = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\cdn", teste.nomeImagem);

            try
            {
                File.WriteAllBytes(caminho, novoTesteModel.Imagem);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao fazer upload da foto, tente novamente mais tarde.");
            }

            db.teste.Add(teste);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = teste.id }, string.Empty);
        }

        // DELETE: api/Teste/5
        [ResponseType(typeof(teste))]
        public async Task<IHttpActionResult> Deleteteste(long id)
        {
            teste teste = await db.teste.FindAsync(id);
            if (teste == null)
            {
                return NotFound();
            }

            db.teste.Remove(teste);
            await db.SaveChangesAsync();

            return Ok(teste);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool testeExists(long id)
        {
            return db.teste.Count(e => e.id == id) > 0;
        }
    }
}