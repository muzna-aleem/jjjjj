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
    public class RequestController : ApiController
    {
        // GET api/request
        db9ca28c8b43524a20ac75a79b015fd9b4Entities db = new db9ca28c8b43524a20ac75a79b015fd9b4Entities();
        // GET api/teacher
        public IEnumerable<Object> Get()
        {
            var lst = from a in db.Requests select new { a.Id, a.SenderId , a.ReceiverId , a.DateTime , a.Type , a.SeenStatus };
            return lst.ToList().AsEnumerable();
        }

        // GET api/teacher/5
        public IEnumerable<Object> Get(string id)
        {
            var lst = from a in db.Requests where a.ReceiverId.Equals(id) select new { a.Id, a.SenderId, a.ReceiverId, a.DateTime, a.Type  , a.SeenStatus};
            var returnList = lst.ToList().AsEnumerable();
            if (returnList == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return returnList;
        }

        // POST api/teacher
        public HttpResponseMessage Post([FromBody] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Requests.Add(request);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, request);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = request.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // PUT api/teacher/5
        public HttpResponseMessage Put(int id, [FromBody] Request request)
        {
            if (ModelState.IsValid && id == request.Id)
            {
                db.Entry(request).State = EntityState.Modified;

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
        public HttpResponseMessage Delete(int id)
        {

            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Requests.Remove(request);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, request);
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
