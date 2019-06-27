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
    public class CHITIETDONHANGController : ApiController
    {
        private CafeDbContext db = new CafeDbContext();

        // GET: api/CHITIETDONHANG
        public IQueryable<CHITIETDONHANG> GetCHITIETDONHANG()
        {
            return db.CHITIETDONHANG;
        }

        // GET: api/CHITIETDONHANG/5
        [ResponseType(typeof(CHITIETDONHANG))]
        public async Task<IHttpActionResult> GetCHITIETDONHANG(int id)
        {
            CHITIETDONHANG cHITIETDONHANG = await db.CHITIETDONHANG.FindAsync(id);
            if (cHITIETDONHANG == null)
            {
                return NotFound();
            }

            return Ok(cHITIETDONHANG);
        }

        // PUT: api/CHITIETDONHANG/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCHITIETDONHANG(int id, CHITIETDONHANG cHITIETDONHANG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cHITIETDONHANG.ID)
            {
                return BadRequest();
            }

            db.Entry(cHITIETDONHANG).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CHITIETDONHANGExists(id))
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

        // POST: api/CHITIETDONHANG
        [ResponseType(typeof(CHITIETDONHANG))]
        public async Task<IHttpActionResult> PostCHITIETDONHANG(CHITIETDONHANG cHITIETDONHANG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CHITIETDONHANG.Add(cHITIETDONHANG);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cHITIETDONHANG.ID }, cHITIETDONHANG);
        }

        // DELETE: api/CHITIETDONHANG/5
        [ResponseType(typeof(CHITIETDONHANG))]
        public async Task<IHttpActionResult> DeleteCHITIETDONHANG(int id)
        {
            CHITIETDONHANG cHITIETDONHANG = await db.CHITIETDONHANG.FindAsync(id);
            if (cHITIETDONHANG == null)
            {
                return NotFound();
            }

            db.CHITIETDONHANG.Remove(cHITIETDONHANG);
            await db.SaveChangesAsync();

            return Ok(cHITIETDONHANG);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CHITIETDONHANGExists(int id)
        {
            return db.CHITIETDONHANG.Count(e => e.ID == id) > 0;
        }
    }
}