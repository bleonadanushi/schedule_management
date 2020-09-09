using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using OrariMesimor;

namespace OrariMesimor.Controllers
{
    [EnableCors(origins: "http://localhost:58776", headers: "*", methods: "*")]
    public class LendetsController : ApiController
    {
        private OrariMesimorEntities db = new OrariMesimorEntities();

        
        public IQueryable<Lendet> GetLendets()
        {
            return db.Lendets;
        }

        [ResponseType(typeof(Lendet))]
        public IHttpActionResult GetLendet(int id)
        {
            Lendet lendet = db.Lendets.Find(id);
            if (lendet == null)
            {
                return NotFound();
            }

            return Ok(lendet);
        }

        
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLendet(int id, Lendet lendet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lendet.Id)
            {
                return BadRequest();
            }

            db.Entry(lendet).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LendetExists(id))
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

        
        [ResponseType(typeof(Lendet))]
        public IHttpActionResult PostLendet(Lendet lendet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lendets.Add(lendet);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = lendet.Id }, lendet);
        }

        [ResponseType(typeof(Lendet))]
        public IHttpActionResult DeleteLendet(int id)
        {
            Lendet lendet = db.Lendets.Find(id);
            if (lendet == null)
            {
                return NotFound();
            }

            db.Lendets.Remove(lendet);
            db.SaveChanges();

            return Ok(lendet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LendetExists(int id)
        {
            return db.Lendets.Count(e => e.Id == id) > 0;
        }
    }
}