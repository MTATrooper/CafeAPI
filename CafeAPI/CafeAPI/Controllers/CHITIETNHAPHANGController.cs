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
    public class CHITIETNHAPHANGController : ApiController
    {
        private CafeDbContext db = new CafeDbContext();

        // GET: api/CHITIETNHAPHANG
        public IQueryable<CHITIETNHAPHANG> GetCHITIETNHAPHANG()
        {
            return db.CHITIETNHAPHANG;
        }

        // GET: api/CHITIETNHAPHANG/5
        [ResponseType(typeof(CHITIETNHAPHANG))]
        public async Task<IHttpActionResult> GetCHITIETNHAPHANG(int id)
        {
            CHITIETNHAPHANG cHITIETNHAPHANG = await db.CHITIETNHAPHANG.FindAsync(id);
            if (cHITIETNHAPHANG == null)
            {
                return NotFound();
            }

            return Ok(cHITIETNHAPHANG);
        }

        // PUT: api/CHITIETNHAPHANG/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCHITIETNHAPHANG(CHITIETNHAPHANG cHITIETNHAPHANG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            

            db.Entry(cHITIETNHAPHANG).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CHITIETNHAPHANG
        [ResponseType(typeof(CHITIETNHAPHANG))]
        public async Task<IHttpActionResult> PostCHITIETNHAPHANG(CHITIETNHAPHANG cHITIETNHAPHANG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CHITIETNHAPHANG.Add(cHITIETNHAPHANG);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cHITIETNHAPHANG.ID }, cHITIETNHAPHANG);
        }

        // DELETE: api/CHITIETNHAPHANG/5
        [ResponseType(typeof(CHITIETNHAPHANG))]
        public async Task<IHttpActionResult> DeleteCHITIETNHAPHANG(int id)
        {
            CHITIETNHAPHANG cHITIETNHAPHANG = await db.CHITIETNHAPHANG.FindAsync(id);
            if (cHITIETNHAPHANG == null)
            {
                return NotFound();
            }

            db.CHITIETNHAPHANG.Remove(cHITIETNHAPHANG);
            await db.SaveChangesAsync();

            return Ok(cHITIETNHAPHANG);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CHITIETNHAPHANGExists(int id)
        {
            return db.CHITIETNHAPHANG.Count(e => e.ID == id) > 0;
        }
    }
}