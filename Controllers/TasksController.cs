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
    public class TasksController : ApiController
    {
        private TaskManagerEntities1 db = new TaskManagerEntities1();

        // GET: api/Tasks
        public List<TaskView> GetTasks()
        {
            return getTaskView();
        }

        // GET: api/Tasks/5
        [ResponseType(typeof(Task))]
        public IHttpActionResult GetTask(int id)
        {
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // PUT: api/Tasks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTask(int id, Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != task.Task_ID)
            {
                return BadRequest();
            }

            db.Entry(task).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
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

        // POST: api/Tasks
        [ResponseType(typeof(Task))]
        public IHttpActionResult PostTask(Task task)
        {
            if (!ModelState.IsValid && task.Task_ID != 0)
            {
                return BadRequest(ModelState);
            }

            db.Tasks.Add(task);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = task.Task_ID }, task);
        }

        // DELETE: api/Tasks/5
        [ResponseType(typeof(Task))]
        public IHttpActionResult DeleteTask(int id)
        {
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            db.Tasks.Remove(task);
            db.SaveChanges();

            return Ok(task);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaskExists(int id)
        {
            return db.Tasks.Count(e => e.Task_ID == id) > 0;
        }

        private List<TaskView> getTaskView()
        {
            List<TaskView> taskview = new List<TaskView>();
            List<Parent_Task> parenttask = db.Parent_Tasks.ToList();
            foreach(Task task in db.Tasks)
            {
                taskview.Add(new TaskView()
                {
                    Task_ID=task.Task_ID,
                    Parent_ID=task.Parent_ID,
                   Start_Date=task.Start_Date,
                   End_Date=task.End_Date,
                    Priority=task.Priority,
                    TaskDec=task.TaskDec,
                    ParentTaskDesc=parenttask.SingleOrDefault(sa=>sa.Parent_ID==task.Parent_ID).TaskDescription
                });
            }
            return taskview;
        }
    }
}