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
    public class LOAISPController : ApiController
    {
        private CafeDbContext db = new CafeDbContext();

        // GET: api/LOAISP
        public IQueryable<LOAISP> GetLOAISP()
        {
            return db.LOAISP;
        }

        // GET: api/LOAISP/5
        [ResponseType(typeof(LOAISP))]
        public async Task<IHttpActionResult> GetLOAISP(int id)
        {
            LOAISP lOAISP = await db.LOAISP.FindAsync(id);
            if (lOAISP == null)
            {
                return NotFound();
            }

            return Ok(lOAISP);
        }

        // PUT: api/LOAISP/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLOAISP(int id, LOAISP lOAISP)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lOAISP.ID)
            {
                return BadRequest();
            }

            db.Entry(lOAISP).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LOAISPExists(id))
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

        // POST: api/LOAISP
        [ResponseType(typeof(LOAISP))]
        public async Task<IHttpActionResult> PostLOAISP(LOAISP lOAISP)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LOAISP.Add(lOAISP);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = lOAISP.ID }, lOAISP);
        }

        // DELETE: api/LOAISP/5
        [ResponseType(typeof(LOAISP))]
        public async Task<IHttpActionResult> DeleteLOAISP(int id)
        {
            LOAISP lOAISP = await db.LOAISP.FindAsync(id);
            if (lOAISP == null)
            {
                return NotFound();
            }

            db.LOAISP.Remove(lOAISP);
            await db.SaveChangesAsync();

            return Ok(lOAISP);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LOAISPExists(int id)
        {
            return db.LOAISP.Count(e => e.ID == id) > 0;
        }
    }
}