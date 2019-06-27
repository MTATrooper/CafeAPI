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
    public class TRANGTHAIDHController : ApiController
    {
        private CafeDbContext db = new CafeDbContext();

        // GET: api/TRANGTHAIDH
        public IQueryable<TRANGTHAIDH> GetTRANGTHAIDH()
        {
            return db.TRANGTHAIDH;
        }

        // GET: api/TRANGTHAIDH/5
        [ResponseType(typeof(TRANGTHAIDH))]
        public async Task<IHttpActionResult> GetTRANGTHAIDH(int id)
        {
            TRANGTHAIDH tRANGTHAIDH = await db.TRANGTHAIDH.FindAsync(id);
            if (tRANGTHAIDH == null)
            {
                return NotFound();
            }

            return Ok(tRANGTHAIDH);
        }

        // PUT: api/TRANGTHAIDH/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTRANGTHAIDH(int id, TRANGTHAIDH tRANGTHAIDH)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tRANGTHAIDH.ID)
            {
                return BadRequest();
            }

            db.Entry(tRANGTHAIDH).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TRANGTHAIDHExists(id))
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

        // POST: api/TRANGTHAIDH
        [ResponseType(typeof(TRANGTHAIDH))]
        public async Task<IHttpActionResult> PostTRANGTHAIDH(TRANGTHAIDH tRANGTHAIDH)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TRANGTHAIDH.Add(tRANGTHAIDH);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tRANGTHAIDH.ID }, tRANGTHAIDH);
        }

        // DELETE: api/TRANGTHAIDH/5
        [ResponseType(typeof(TRANGTHAIDH))]
        public async Task<IHttpActionResult> DeleteTRANGTHAIDH(int id)
        {
            TRANGTHAIDH tRANGTHAIDH = await db.TRANGTHAIDH.FindAsync(id);
            if (tRANGTHAIDH == null)
            {
                return NotFound();
            }

            db.TRANGTHAIDH.Remove(tRANGTHAIDH);
            await db.SaveChangesAsync();

            return Ok(tRANGTHAIDH);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TRANGTHAIDHExists(int id)
        {
            return db.TRANGTHAIDH.Count(e => e.ID == id) > 0;
        }
    }
}