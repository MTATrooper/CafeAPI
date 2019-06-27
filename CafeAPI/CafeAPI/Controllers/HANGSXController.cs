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
    public class HANGSXController : ApiController
    {
        private CafeDbContext db = new CafeDbContext();

        // GET: api/HANGSX
        public IQueryable<HANGSX> GetHANGSX()
        {
            return db.HANGSX;
        }

        // GET: api/HANGSX/5
        [ResponseType(typeof(HANGSX))]
        public async Task<IHttpActionResult> GetHANGSX(int id)
        {
            HANGSX hANGSX = await db.HANGSX.FindAsync(id);
            if (hANGSX == null)
            {
                return NotFound();
            }

            return Ok(hANGSX);
        }

        // PUT: api/HANGSX/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutHANGSX(int id, HANGSX hANGSX)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hANGSX.ID)
            {
                return BadRequest();
            }

            db.Entry(hANGSX).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HANGSXExists(id))
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

        // POST: api/HANGSX
        [ResponseType(typeof(HANGSX))]
        public async Task<IHttpActionResult> PostHANGSX(HANGSX hANGSX)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HANGSX.Add(hANGSX);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = hANGSX.ID }, hANGSX);
        }

        // DELETE: api/HANGSX/5
        [ResponseType(typeof(HANGSX))]
        public async Task<IHttpActionResult> DeleteHANGSX(int id)
        {
            HANGSX hANGSX = await db.HANGSX.FindAsync(id);
            if (hANGSX == null)
            {
                return NotFound();
            }

            db.HANGSX.Remove(hANGSX);
            await db.SaveChangesAsync();

            return Ok(hANGSX);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HANGSXExists(int id)
        {
            return db.HANGSX.Count(e => e.ID == id) > 0;
        }
    }
}