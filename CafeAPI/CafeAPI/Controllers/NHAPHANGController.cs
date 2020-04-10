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
using CafeAPI.DAO;

namespace CafeAPI.Controllers
{
    public class NHAPHANGController : ApiController
    {
        private CafeDbContext db = new CafeDbContext();

        // GET: api/NHAPHANG
        public IQueryable<NHAPHANG> GetNHAPHANG()
        {
            return db.NHAPHANG;
        }

        // GET: api/NHAPHANG/5
        [ResponseType(typeof(NHAPHANG))]
        public async Task<IHttpActionResult> GetNHAPHANG(int id)
        {
            NHAPHANG nHAPHANG = await db.NHAPHANG.FindAsync(id);
            if (nHAPHANG == null)
            {
                return NotFound();
            }

            return Ok(nHAPHANG);
        }


        // POST: api/NHAPHANG
        [ResponseType(typeof(NHAPHANG))]
        public async Task<IHttpActionResult> PostNHAPHANG(NHAPHANG nHAPHANG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int idNH = new NhapHangDAO().InsertNhapHang(nHAPHANG);
            nHAPHANG.ID = idNH;

            return CreatedAtRoute("DefaultApi", new { id = nHAPHANG.ID }, nHAPHANG);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NHAPHANGExists(int id)
        {
            return db.NHAPHANG.Count(e => e.ID == id) > 0;
        }
    }
}