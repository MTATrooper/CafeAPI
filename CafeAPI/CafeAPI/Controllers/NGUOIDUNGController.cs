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
    public class NGUOIDUNGController : ApiController
    {
        private CafeDbContext db = new CafeDbContext();

        // GET: api/NGUOIDUNG
        public IQueryable<NGUOIDUNG> GetNGUOIDUNG()
        {
            return db.NGUOIDUNG;
        }

        // GET: api/NGUOIDUNG/5
        [ResponseType(typeof(NGUOIDUNG))]
        public async Task<IHttpActionResult> GetNGUOIDUNG(int id)
        {
            NGUOIDUNG nGUOIDUNG = await db.NGUOIDUNG.FindAsync(id);
            if (nGUOIDUNG == null)
            {
                return NotFound();
            }

            return Ok(nGUOIDUNG);
        }

        // PUT: api/NGUOIDUNG/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutNGUOIDUNG(int id, NGUOIDUNG nGUOIDUNG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nGUOIDUNG.ID)
            {
                return BadRequest();
            }

            db.Entry(nGUOIDUNG).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NGUOIDUNGExists(id))
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

        // POST: api/NGUOIDUNG
        [ResponseType(typeof(NGUOIDUNG))]
        public async Task<IHttpActionResult> PostNGUOIDUNG(NGUOIDUNG nGUOIDUNG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NGUOIDUNG.Add(nGUOIDUNG);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = nGUOIDUNG.ID }, nGUOIDUNG);
        }

        // DELETE: api/NGUOIDUNG/5
        [ResponseType(typeof(NGUOIDUNG))]
        public async Task<IHttpActionResult> DeleteNGUOIDUNG(int id)
        {
            NGUOIDUNG nGUOIDUNG = await db.NGUOIDUNG.FindAsync(id);
            if (nGUOIDUNG == null)
            {
                return NotFound();
            }

            db.NGUOIDUNG.Remove(nGUOIDUNG);
            await db.SaveChangesAsync();

            return Ok(nGUOIDUNG);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NGUOIDUNGExists(int id)
        {
            return db.NGUOIDUNG.Count(e => e.ID == id) > 0;
        }
    }
}