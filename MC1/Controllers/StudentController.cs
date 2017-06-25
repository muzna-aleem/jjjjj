
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
    public class StudentController : ApiController
    {
        private db9ca28c8b43524a20ac75a79b015fd9b4Entities db = new db9ca28c8b43524a20ac75a79b015fd9b4Entities();
        
      
        // GET api/Student
        public IEnumerable<object> GetStudents()
        {
            var lst = from a in db.Students select new { a.RollNumber, a.Name, a.Password, a.Email};
            return lst.ToList().AsEnumerable();
        }

        // GET api/Student/5
        public IEnumerable<object>  GetStudent(string id)
        {
            var lst = from a in db.Students where a.RollNumber.Equals(id) select new { a.RollNumber, a.Name, a.Password, a.Email };
            var returnList = lst.ToList().AsEnumerable();
           
            if (returnList == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return returnList;
        }

        // PUT api/Student/5
        public HttpResponseMessage PutStudent(string id, Student student)
        {
            if (ModelState.IsValid && id == student.RollNumber)
            {
                db.Entry(student).State = EntityState.Modified;

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

        // POST api/Student
        public HttpResponseMessage PostStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, student);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = student.RollNumber }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Student/5
        public HttpResponseMessage DeleteStudent(string id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Students.Remove(student);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, student);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}