using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using OrariMesimor;

namespace OrariMesimor.Controllers
{
    public class TipetsController : ApiController
    {
        private OrariMesimorEntities db = new OrariMesimorEntities();

        
        public IQueryable<Tipet> GetTipets()
        {
            return db.Tipets;
        }

        
        [ResponseType(typeof(Tipet))]
        public IHttpActionResult GetTipet(int id)
        {
            Tipet tipet = db.Tipets.Find(id);
            if (tipet == null)
            {
                return NotFound();
            }

            return Ok(tipet);
        }

        
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTipet(int id, Tipet tipet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipet.Id)
            {
                return BadRequest();
            }

            db.Entry(tipet).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipetExists(id))
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

        
        [ResponseType(typeof(Tipet))]
        public IHttpActionResult PostTipet(Tipet tipet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tipets.Add(tipet);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TipetExists(tipet.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tipet.Id }, tipet);
        }

        
        [ResponseType(typeof(Tipet))]
        public IHttpActionResult DeleteTipet(int id)
        {
            Tipet tipet = db.Tipets.Find(id);
            if (tipet == null)
            {
                return NotFound();
            }

            db.Tipets.Remove(tipet);
            db.SaveChanges();

            return Ok(tipet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipetExists(int id)
        {
            return db.Tipets.Count(e => e.Id == id) > 0;
        }
    }
}