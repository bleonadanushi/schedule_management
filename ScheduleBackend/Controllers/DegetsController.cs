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
    public class DegetsController : ApiController
    {
        private OrariMesimorEntities db = new OrariMesimorEntities();

        
        public IQueryable<Deget> GetDegets()
        {
            return db.Degets;
        }

       
        [ResponseType(typeof(Deget))]
        public IHttpActionResult GetDeget(int id)
        {
            Deget deget = db.Degets.Find(id);
            if (deget == null)
            {
                return NotFound();
            }

            return Ok(deget);
        }

        
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDeget(int id, Deget deget)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deget.Id)
            {
                return BadRequest();
            }

            db.Entry(deget).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DegetExists(id))
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

       
        [ResponseType(typeof(Deget))]
        public IHttpActionResult PostDeget(Deget deget)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Degets.Add(deget);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = deget.Id }, deget);
        }

       
        [ResponseType(typeof(Deget))]
        public IHttpActionResult DeleteDeget(int id)
        {
            Deget deget = db.Degets.Find(id);
            if (deget == null)
            {
                return NotFound();
            }

            db.Degets.Remove(deget);
            db.SaveChanges();

            return Ok(deget);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DegetExists(int id)
        {
            return db.Degets.Count(e => e.Id == id) > 0;
        }
    }
}