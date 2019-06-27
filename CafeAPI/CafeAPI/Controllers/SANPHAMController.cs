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
    public class SANPHAMController : ApiController
    {
        private CafeDbContext db = new CafeDbContext();

        // GET: api/SANPHAM
        public IQueryable<SANPHAM> GetSANPHAM()
        {
            return db.SANPHAM;
        }

        // GET: api/SANPHAM/5
        [ResponseType(typeof(SANPHAM))]
        public async Task<IHttpActionResult> GetSANPHAM(int id)
        {
            SANPHAM sANPHAM = await db.SANPHAM.FindAsync(id);
            if (sANPHAM == null)
            {
                return NotFound();
            }

            return Ok(sANPHAM);
        }

        // PUT: api/SANPHAM/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSANPHAM(SANPHAM sANPHAM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (id != sANPHAM.ID)
            //{
            //    return BadRequest();
            //}

            db.Entry(sANPHAM).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!SANPHAMExists(id))
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

        // POST: api/SANPHAM
        [ResponseType(typeof(SANPHAM))]
        public async Task<IHttpActionResult> PostSANPHAM(SANPHAM sANPHAM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SANPHAM.Add(sANPHAM);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = sANPHAM.ID }, sANPHAM);
        }

        // DELETE: api/SANPHAM/5
        [ResponseType(typeof(SANPHAM))]
        public async Task<IHttpActionResult> DeleteSANPHAM(int id)
        {
            SANPHAM sANPHAM = await db.SANPHAM.FindAsync(id);
            if (sANPHAM == null)
            {
                return NotFound();
            }

            db.SANPHAM.Remove(sANPHAM);
            await db.SaveChangesAsync();

            return Ok(sANPHAM);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SANPHAMExists(int id)
        {
            return db.SANPHAM.Count(e => e.ID == id) > 0;
        }
    }
}