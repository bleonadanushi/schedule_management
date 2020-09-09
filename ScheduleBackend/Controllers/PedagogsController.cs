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
    public class PedagogsController : ApiController
    {
        private OrariMesimorEntities db = new OrariMesimorEntities();

        
        public IQueryable<Pedagog> GetPedagogs()
        {
            return db.Pedagogs;
        }

        
        [ResponseType(typeof(Pedagog))]
        public IHttpActionResult GetPedagog(int id)
        {
            Pedagog pedagog = db.Pedagogs.Find(id);
            if (pedagog == null)
            {
                return NotFound();
            }

            return Ok(pedagog);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutPedagog(int id, Pedagog pedagog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pedagog.Id)
            {
                return BadRequest();
            }

            db.Entry(pedagog).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedagogExists(id))
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

        
        [ResponseType(typeof(Pedagog))]
        public IHttpActionResult PostPedagog(Pedagog pedagog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pedagogs.Add(pedagog);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pedagog.Id }, pedagog);
        }

        
        [ResponseType(typeof(Pedagog))]
        public IHttpActionResult DeletePedagog(int id)
        {
            Pedagog pedagog = db.Pedagogs.Find(id);
            if (pedagog == null)
            {
                return NotFound();
            }

            db.Pedagogs.Remove(pedagog);
            db.SaveChanges();

            return Ok(pedagog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PedagogExists(int id)
        {
            return db.Pedagogs.Count(e => e.Id == id) > 0;
        }
    }
}