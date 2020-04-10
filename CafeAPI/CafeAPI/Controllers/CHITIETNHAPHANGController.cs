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
using CafeAPI.DAO;
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

        // POST: api/CHITIETNHAPHANG
        [ResponseType(typeof(CHITIETNHAPHANG))]
        public async Task<IHttpActionResult> PostCHITIETNHAPHANG(CHITIETNHAPHANG cHITIETNHAPHANG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            new NhapHangDAO().InsertCTNhapHang(cHITIETNHAPHANG);

            return CreatedAtRoute("DefaultApi", new { id = cHITIETNHAPHANG.ID }, cHITIETNHAPHANG);
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