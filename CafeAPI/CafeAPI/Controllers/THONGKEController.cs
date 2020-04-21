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
    [RoutePrefix("api/THONGKE")]
    public class THONGKEController : ApiController
    {
        private CafeDbContext db = new CafeDbContext();

        // GET: api/THONGKE
        public IQueryable<THONGKE> GetTHONGKE()
        {
            return db.THONGKE;
        }

        // GET: api/THONGKE/5
        [ResponseType(typeof(THONGKE))]
        public async Task<IHttpActionResult> GetTHONGKE(int id)
        {
            THONGKE tHONGKE = await db.THONGKE.FindAsync(id);
            if (tHONGKE == null)
            {
                return NotFound();
            }

            return Ok(tHONGKE);
        }

        // PUT: api/THONGKE/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTHONGKE(THONGKE tHONGKE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           

            db.Entry(tHONGKE).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/THONGKE
        [ResponseType(typeof(THONGKE))]
        public async Task<IHttpActionResult> PostTHONGKE(THONGKE tHONGKE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.THONGKE.Add(tHONGKE);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tHONGKE.ID }, tHONGKE);
        }

        // POST: api/THONGKE/GetThongKe
        [HttpPost]
        [Route("GetThongKe")]
        //[ResponseType(typeof(List<THONGKE>))]
        public async Task<IHttpActionResult> GetThongKe(ThongKeDate tkDate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ThongKe_DAO tk_DAO = new ThongKe_DAO();
            List<THONGKE> results = tk_DAO.getThongKe(tkDate);
            //return CreatedAtRoute("DefaultApi", new { id = 1 }, results);
            return Ok(results);
        }
        // POST: api/THONGKE/GetSanPhamBanChay
        [HttpPost]
        [Route("GetSanPhamBanChay")]
        //[ResponseType(typeof(List<THONGKE>))]
        public async Task<IHttpActionResult> GetSanPhamBanChay(ThongKeDate tkDate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ThongKe_DAO tk_DAO = new ThongKe_DAO();
            List<SanPhamBanChay> results = tk_DAO.getSanPhamBanChay(tkDate);
            //return CreatedAtRoute("DefaultApi", new { id = 1 }, results);
            return Ok(results);
        }

        // DELETE: api/THONGKE/5
        [ResponseType(typeof(THONGKE))]
        public async Task<IHttpActionResult> DeleteTHONGKE(int id)
        {
            THONGKE tHONGKE = await db.THONGKE.FindAsync(id);
            if (tHONGKE == null)
            {
                return NotFound();
            }

            db.THONGKE.Remove(tHONGKE);
            await db.SaveChangesAsync();

            return Ok(tHONGKE);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool THONGKEExists(int id)
        {
            return db.THONGKE.Count(e => e.ID == id) > 0;
        }
    }
}