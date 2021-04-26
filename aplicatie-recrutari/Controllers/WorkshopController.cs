using aplicatie_recrutari.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aplicatie_recrutari.Controllers
{
    [Authorize(Roles = "Admin")]
    public class WorkshopController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        public ActionResult Show(int? id) {
            if (id.HasValue) {
                Workshop workshop = db.Workshops.Find(id);
                if (workshop != null) {
                    return View(workshop);
                }
                return HttpNotFound("Couldn't find the workshop with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing workshop id parameter!");
        }

        [HttpGet]
        public ActionResult New(int? id) {
            Workshop workshop = new Workshop();
            workshop.AllDepartments = GetAllDepartments();
            if (id.HasValue)
            {
                workshop.SessionId = id.Value;
            }
            return View(workshop);
        }

        [HttpPost]
        public ActionResult New(Workshop workshopRequest, int SessionId) {
            try {
                workshopRequest.AllDepartments = GetAllDepartments();
                workshopRequest.SessionId = SessionId;
                if (ModelState.IsValid) {
                    db.Workshops.Add(workshopRequest);
                    db.SaveChanges();
                    return RedirectToRoute(new { controller = "Workshop", action = "Show", id = workshopRequest.WorkshopId });
                }
                return View(workshopRequest);
            }
            catch (Exception) {
                return View(workshopRequest);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id, int? SessionId) {

            if (id.HasValue) {
                Workshop workshop = db.Workshops.Find(id);
                if (workshop == null) {
                    return HttpNotFound("Couldn't find the workshop with id " + id.ToString());
                }
                workshop.AllDepartments = GetAllDepartments();
                if (SessionId.HasValue)
                {
                    workshop.SessionId = SessionId.Value;
                }
                return View(workshop);
            }
            return HttpNotFound("Missing workshop id parameter!");
        }

        [HttpPut]
        public ActionResult Edit(int id, Workshop workshopRequest, int SessionId) {
            try {
                workshopRequest.AllDepartments = GetAllDepartments();
                workshopRequest.SessionId = SessionId;
                if (ModelState.IsValid) {
                    Workshop workshop = db.Workshops
                    .SingleOrDefault(b => b.WorkshopId.Equals(id));
                    if (TryUpdateModel(workshop)) {
                        workshop.Timestamp = workshopRequest.Timestamp;
                        workshop.DepartmentId1 = workshopRequest.DepartmentId1;
                        workshop.DepartmentId2 = workshopRequest.DepartmentId2;
                        workshop.SessionId = workshopRequest.SessionId;
                        db.SaveChanges();
                    }
                    return RedirectToRoute(new { controller = "Workshop", action = "Show", id = workshop.WorkshopId });
                }
                return View(workshopRequest);
            }
            catch (Exception) {
                return View(workshopRequest);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id) {
            Workshop workshop = db.Workshops.Find(id);
            int SessionId = workshop.SessionId;
            if (workshop != null) {
                db.Workshops.Remove(workshop);
                db.SaveChanges();
                return RedirectToRoute(new { controller = "Session", action = "Show", id = SessionId });
            }
            return HttpNotFound("Couldn't find the workshop with id " + id.ToString());
        }

        [NonAction]
        public IEnumerable<SelectListItem> GetAllDepartments()
        {
            var selectList = new List<SelectListItem>();

            var departments = from department in db.Departments select department;
            foreach (var dep in departments)
            {
                selectList.Add(new SelectListItem
                {
                    Value = dep.DepartmentId.ToString(),
                    Text = dep.Name.ToString()
                });
            }
            return selectList;
        }
    }
}