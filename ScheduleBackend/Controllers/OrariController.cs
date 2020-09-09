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
    public class OrariController : ApiController
    {
        private OrariMesimorEntities db = new OrariMesimorEntities();

        
        public IQueryable<Orari> GetOraris()
        {
            return db.Oraris;
        }

        
        [ResponseType(typeof(Orari))]
        public IHttpActionResult GetOrari(int id)
        {
            Orari orari = db.Oraris.Find(id);
            if (orari == null)
            {
                return NotFound();
            }

            return Ok(orari);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrari(int id, Orari orari)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orari.Id)
            {
                return BadRequest();
            }

            db.Entry(orari).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrariExists(id))
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

        [ResponseType(typeof(Orari))]
        public IHttpActionResult PostOrari(Orari orari)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Oraris.Add(orari);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = orari.Id }, orari);
        }

        [ResponseType(typeof(Orari))]
        public IHttpActionResult DeleteOrari(int id)
        {
            Orari orari = db.Oraris.Find(id);
            if (orari == null)
            {
                return NotFound();
            }

            db.Oraris.Remove(orari);
            db.SaveChanges();

            return Ok(orari);
        }
        
        [HttpGet]
        [Route("api/getOrarPedagog")]
        public IHttpActionResult GetOrarPedagog(string pedagog)
        {
            var SelectedPedagog =  db.Oraris.Where(x => x.Pedagog == pedagog ).Select(x => new { x.Dega, x.Lenda, x.Tipi,x.Viti,x.Paraleli,x.Klasa,x.Ora,x.Dita}).ToList();
            if (SelectedPedagog == null)
            {
                return NotFound();
            }

            return Ok(SelectedPedagog);
        }
       
        [HttpGet]
        [Route("api/getOrarStudent")]
        public IHttpActionResult GetOrarStudent(string dega,int viti, string paraleli)
        {
            var SelectedStudent = db.Oraris.Where(x => x.Dega == dega && x.Viti==viti && x.Paraleli==paraleli).Select(x => new {  x.Lenda, x.Tipi, x.Pedagog, x.Klasa, x.Ora, x.Dita }).ToList();
            if (SelectedStudent == null)
            {
                return NotFound();
            }

            return Ok(SelectedStudent);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrariExists(int id)
        {
            return db.Oraris.Count(e => e.Id == id) > 0;
        }
    }
}