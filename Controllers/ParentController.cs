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
using CapsuleTaskManage.DBContext;
using System.Web.Http.Cors;
namespace CapsuleTaskManage.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
    public class ParentController : ApiController
    {
        private TaskManagerEntities1 db = new TaskManagerEntities1();

        // GET: api/Parent
        public IQueryable<Parent_Task> GetParent_Tasks()
        {
            return db.Parent_Tasks;
        }

        // GET: api/Parent/5
        [ResponseType(typeof(Parent_Task))]
        public IHttpActionResult GetParent_Task(int id)
        {
            Parent_Task parent_Task = db.Parent_Tasks.Find(id);
            if (parent_Task == null)
            {
                return NotFound();
            }

            return Ok(parent_Task);
        }

        // PUT: api/Parent/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParent_Task(int id, Parent_Task parent_Task)
        {
            if (!ModelState.IsValid )
            {
                return BadRequest(ModelState);
            }

            if (id != parent_Task.Parent_ID)
            {
                return BadRequest();
            }

            db.Entry(parent_Task).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Parent_TaskExists(id))
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

        // POST: api/Parent
        [ResponseType(typeof(Parent_Task))]
        public IHttpActionResult PostParent_Task(Parent_Task parent_Task)
        {
            if (!ModelState.IsValid && parent_Task.Parent_ID != 0)
            {
                return BadRequest(ModelState);
            }

            db.Parent_Tasks.Add(parent_Task);
            db.SaveChanges();                              

            return CreatedAtRoute("DefaultApi", new { id = parent_Task.Parent_ID }, parent_Task);
        }

        // DELETE: api/Parent/5
        [ResponseType(typeof(Parent_Task))]
        public IHttpActionResult DeleteParent_Task(int id)
        {
            Parent_Task parent_Task = db.Parent_Tasks.Find(id);
            if (parent_Task == null)
            {
                return NotFound();
            }

            db.Parent_Tasks.Remove(parent_Task);
            db.SaveChanges();

            return Ok(parent_Task);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Parent_TaskExists(int id)
        {
            return db.Parent_Tasks.Count(e => e.Parent_ID == id) > 0;
        }
    }
}