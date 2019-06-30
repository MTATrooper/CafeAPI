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
    public class LOAISPController : ApiController
    {
        private LOAISP_DAO lspDAO = new LOAISP_DAO();
        private CafeDbContext db = new CafeDbContext();

        // GET: api/LOAISP
        public List<LOAISP> GetLOAISP()
        {
            List<LOAISP> lst = lspDAO.GetLOAISP();
            return lst;
            //return db.LOAISP.ToList();
        }

        // GET: api/LOAISP/5
        [ResponseType(typeof(LOAISP))]
        public IHttpActionResult GetLOAISP(int id)
        {
            LOAISP lOAISP = lspDAO.GetLSPbyId(id);
            if (lOAISP == null)
            {
                return NotFound();
            }

            return Ok(lOAISP);
        }

        // PUT: api/LOAISP/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLOAISP([FromBody]LOAISP lOAISP)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                lspDAO.UpdateLOAISP(lOAISP);
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/LOAISP
        [ResponseType(typeof(LOAISP))]
        public IHttpActionResult PostLOAISP(LOAISP lOAISP)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            lspDAO.InsertLOAISP(lOAISP);
            
            return CreatedAtRoute("DefaultApi", new { id = lOAISP.ID }, lOAISP);
        }

        // DELETE: api/LOAISP/5
        [ResponseType(typeof(LOAISP))]
        public IHttpActionResult DeleteLOAISP(int id)
        {
            LOAISP lOAISP = lspDAO.GetLSPbyId(id);
            if (lOAISP == null)
            {
                return NotFound();
            }

            lspDAO.DeleteLOAISP(lOAISP);

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