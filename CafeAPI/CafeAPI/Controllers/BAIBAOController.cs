using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CafeAPI.Models;

namespace CafeAPI.Controllers
{
    public class BAIBAOController : ApiController
    {
        private CafeDbContext db = new CafeDbContext();

        // GET: api/BAIBAO
        public IQueryable<BAIBAO> GetBAIBAO()
        {
            return db.BAIBAO;
        }

        // GET: api/BAIBAO/5
        [ResponseType(typeof(BAIBAO))]
        public async Task<IHttpActionResult> GetBAIBAO(int id)
        {
            BAIBAO bAIBAO = await db.BAIBAO.FindAsync(id);
            if (bAIBAO == null)
            {
                return NotFound();
            }

            return Ok(bAIBAO);
        }

        // PUT: api/BAIBAO/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBAIBAO(int id, BAIBAO bAIBAO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bAIBAO.ID)
            {
                return BadRequest();
            }

            db.Entry(bAIBAO).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BAIBAOExists(id))
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

        // POST: api/BAIBAO
        [ResponseType(typeof(BAIBAO))]
        public async Task<IHttpActionResult> PostBAIBAO(BAIBAO bAIBAO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BAIBAO.Add(bAIBAO);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = bAIBAO.ID }, bAIBAO);
        }

        // DELETE: api/BAIBAO/5
        [ResponseType(typeof(BAIBAO))]
        public async Task<IHttpActionResult> DeleteBAIBAO(int id)
        {
            BAIBAO bAIBAO = await db.BAIBAO.FindAsync(id);
            if (bAIBAO == null)
            {
                return NotFound();
            }

            db.BAIBAO.Remove(bAIBAO);
            await db.SaveChangesAsync();

            return Ok(bAIBAO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BAIBAOExists(int id)
        {
            return db.BAIBAO.Count(e => e.ID == id) > 0;
        }
    }
}