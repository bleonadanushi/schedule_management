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
using System.Web.Http.Cors;


namespace OrariMesimor.Controllers
{
    
    [EnableCors(origins: "http://localhost:58776", headers: "*", methods: "*")]
    public class ImportLendetsController : ApiController
    {
        private OrariMesimorEntities db = new OrariMesimorEntities();

        
        [HttpGet]
        [Route("api/ImportLendets")]
        public IHttpActionResult GetImportLendets()
        {
            var res = from i in db.ImportLendets
                      select new
                      {
                          i.Id,
                          i.Dega,
                          i.Viti,
                          i.Lenda,
                          i.Paraleli,
                          i.Tipi,
                          i.Pedagog,
                          i.Kapur,
                          i.Zgjedhje
                      };

            return Ok(res);
        }

        
        [ResponseType(typeof(ImportLendet))]
        public IHttpActionResult GetImportLendet(int id)
        {
            ImportLendet importLendet = db.ImportLendets.SingleOrDefault(m => m.Id == id);
          
            if (importLendet == null)
            {
                return NotFound();
            }

            return Ok(importLendet);
        }
       
        [ResponseType(typeof(void))]
        public IHttpActionResult PutImportLendet(int id, ImportLendet importLendet)
        {

            var updateImportLendet = db.ImportLendets.Where(x => x.Id == id).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != importLendet.Id)
            {
                return BadRequest();
            }

            updateImportLendet.Id = importLendet.Id;
            updateImportLendet.Dega = importLendet.Dega;
            updateImportLendet.IdTipi = importLendet.IdTipi;
            updateImportLendet.Lenda = importLendet.Lenda;
            updateImportLendet.Kapur = importLendet.Kapur;
            updateImportLendet.Paraleli = importLendet.Paraleli;
            updateImportLendet.Pedagog = importLendet.Pedagog;
            updateImportLendet.Tipi = importLendet.Tipi;
            updateImportLendet.Viti = importLendet.Viti;
            updateImportLendet.Zgjedhje = importLendet.Zgjedhje;

            db.Entry(updateImportLendet).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImportLendetExists(id))
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

        
        [HttpPost]
        [Route("api/SaveImportLendets")]
        public IHttpActionResult PostImportLendet(ImportLendet importLendet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ImportLendets.Add(importLendet);
            db.SaveChanges();

            return Ok(importLendet);
        }

        
        [ResponseType(typeof(ImportLendet))]
        public IHttpActionResult DeleteImportLendet(int id)
        {
            ImportLendet importLendet = db.ImportLendets.Find(id);
            if (importLendet == null)
            {
                return NotFound();
            }

            db.ImportLendets.Remove(importLendet);
            db.SaveChanges();

            return Ok(importLendet);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ImportLendetExists(int id)
        {
            return db.ImportLendets.Count(e => e.Id == id) > 0;
        }
    }
}