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
    public class DONHANGController : ApiController
    {
        private CafeDbContext db = new CafeDbContext();

        // GET: api/DONHANG
        public IQueryable<DONHANG> GetDONHANG()
        {
            return db.DONHANG;
        }

        // GET: api/DONHANG/5
        [ResponseType(typeof(DONHANG))]
        public async Task<IHttpActionResult> GetDONHANG(int id)
        {
            DONHANG dONHANG = await db.DONHANG.FindAsync(id);
            if (dONHANG == null)
            {
                return NotFound();
            }

            return Ok(dONHANG);
        }

        // PUT: api/DONHANG/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDONHANG(DONHANG dONHANG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (id != dONHANG.ID)
            //{
            //    return BadRequest();
            //}

            db.Entry(dONHANG).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!DONHANGExists(id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/DONHANG
        [ResponseType(typeof(DONHANG))]
        public async Task<IHttpActionResult> PostDONHANG(DONHANG dONHANG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DONHANG.Add(dONHANG);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = dONHANG.ID }, dONHANG);
        }

        // DELETE: api/DONHANG/5
        [ResponseType(typeof(DONHANG))]
        public async Task<IHttpActionResult> DeleteDONHANG(int id)
        {
            DONHANG dONHANG = await db.DONHANG.FindAsync(id);
            if (dONHANG == null)
            {
                return NotFound();
            }

            db.DONHANG.Remove(dONHANG);
            await db.SaveChangesAsync();

            return Ok(dONHANG);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DONHANGExists(int id)
        {
            return db.DONHANG.Count(e => e.ID == id) > 0;
        }
    }
}