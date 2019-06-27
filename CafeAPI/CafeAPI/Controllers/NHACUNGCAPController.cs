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
    public class NHACUNGCAPController : ApiController
    {
        private CafeDbContext db = new CafeDbContext();

        // GET: api/NHACUNGCAP
        public IQueryable<NHACUNGCAP> GetNHACUNGCAP()
        {
            return db.NHACUNGCAP;
        }

        // GET: api/NHACUNGCAP/5
        [ResponseType(typeof(NHACUNGCAP))]
        public async Task<IHttpActionResult> GetNHACUNGCAP(int id)
        {
            NHACUNGCAP nHACUNGCAP = await db.NHACUNGCAP.FindAsync(id);
            if (nHACUNGCAP == null)
            {
                return NotFound();
            }

            return Ok(nHACUNGCAP);
        }

        // PUT: api/NHACUNGCAP/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutNHACUNGCAP(int id, NHACUNGCAP nHACUNGCAP)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nHACUNGCAP.ID)
            {
                return BadRequest();
            }

            db.Entry(nHACUNGCAP).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NHACUNGCAPExists(id))
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

        // POST: api/NHACUNGCAP
        [ResponseType(typeof(NHACUNGCAP))]
        public async Task<IHttpActionResult> PostNHACUNGCAP(NHACUNGCAP nHACUNGCAP)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NHACUNGCAP.Add(nHACUNGCAP);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = nHACUNGCAP.ID }, nHACUNGCAP);
        }

        // DELETE: api/NHACUNGCAP/5
        [ResponseType(typeof(NHACUNGCAP))]
        public async Task<IHttpActionResult> DeleteNHACUNGCAP(int id)
        {
            NHACUNGCAP nHACUNGCAP = await db.NHACUNGCAP.FindAsync(id);
            if (nHACUNGCAP == null)
            {
                return NotFound();
            }

            db.NHACUNGCAP.Remove(nHACUNGCAP);
            await db.SaveChangesAsync();

            return Ok(nHACUNGCAP);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NHACUNGCAPExists(int id)
        {
            return db.NHACUNGCAP.Count(e => e.ID == id) > 0;
        }
    }
}