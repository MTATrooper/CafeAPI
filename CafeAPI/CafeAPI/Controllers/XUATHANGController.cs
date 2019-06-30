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
    public class XUATHANGController : ApiController
    {
        private CafeDbContext db = new CafeDbContext();

        // GET: api/XUATHANG
        public IQueryable<XUATHANG> GetXUATHANG()
        {
            return db.XUATHANG;
        }

        // GET: api/XUATHANG/5
        [ResponseType(typeof(XUATHANG))]
        public async Task<IHttpActionResult> GetXUATHANG(int id)
        {
            XUATHANG xUATHANG = await db.XUATHANG.FindAsync(id);
            if (xUATHANG == null)
            {
                return NotFound();
            }

            return Ok(xUATHANG);
        }

        // PUT: api/XUATHANG/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutXUATHANG(XUATHANG xUATHANG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            

            db.Entry(xUATHANG).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/XUATHANG
        [ResponseType(typeof(XUATHANG))]
        public async Task<IHttpActionResult> PostXUATHANG(XUATHANG xUATHANG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.XUATHANG.Add(xUATHANG);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = xUATHANG.ID }, xUATHANG);
        }

        // DELETE: api/XUATHANG/5
        [ResponseType(typeof(XUATHANG))]
        public async Task<IHttpActionResult> DeleteXUATHANG(int id)
        {
            XUATHANG xUATHANG = await db.XUATHANG.FindAsync(id);
            if (xUATHANG == null)
            {
                return NotFound();
            }

            db.XUATHANG.Remove(xUATHANG);
            await db.SaveChangesAsync();

            return Ok(xUATHANG);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool XUATHANGExists(int id)
        {
            return db.XUATHANG.Count(e => e.ID == id) > 0;
        }
    }
}