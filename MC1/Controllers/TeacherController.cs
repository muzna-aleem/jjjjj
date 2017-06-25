using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MC1.Controllers
{
    public class TeacherController : ApiController
    {
        db9ca28c8b43524a20ac75a79b015fd9b4Entities db = new db9ca28c8b43524a20ac75a79b015fd9b4Entities();
        // GET api/teacher
        public IEnumerable<Object> Get()
        {
            var lst = from a in db.Teachers select new { a.Id, a.Name, a.Email, a.Location, a.Education, a.Password, a.Post, a.Available };
            return lst.ToList().AsEnumerable();
        }

        // GET api/teacher/5
        public IEnumerable<Object> Get(string id)
        {
            var lst = from a in db.Teachers where a.Id.Equals(id) select new { a.Id,  a.Name, a.Email, a.Location, a.Education, a.Password, a.Post, a.Available};
            var returnList =  lst.ToList().AsEnumerable();
            if (returnList == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return returnList;
        }

        // POST api/teacher
        public HttpResponseMessage Post([FromBody] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, teacher);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = teacher.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // PUT api/teacher/5
        public HttpResponseMessage Put( string id, [FromBody] Teacher teacher)
        {
            if (ModelState.IsValid && id == teacher.Id)
            {
                db.Entry(teacher).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/teacher/5
        public HttpResponseMessage Delete(string id)
        {
            
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Teachers.Remove(teacher);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, teacher);
        }
       

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        }
    
}
