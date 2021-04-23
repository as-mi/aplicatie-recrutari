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
        public ActionResult Index() {
            List<Workshop> workshops = db.Workshops.ToList();
            ViewBag.Workshops = workshops;
            return View();
        }
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
        public ActionResult New() {
            Workshop workshop = new Workshop();
            return View(workshop);
        }

        [HttpPost]
        public ActionResult New(Workshop workshopRequest) {
            try {
                if (ModelState.IsValid) {
                    db.Workshops.Add(workshopRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(workshopRequest);
            }
            catch (Exception) {
                return View(workshopRequest);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id) {
            if (id.HasValue) {
                Workshop workshop = db.Workshops.Find(id);
                if (workshop == null) {
                    return HttpNotFound("Couldn't find the workshop with id " + id.ToString());
                }
                return View(workshop);
            }
            return HttpNotFound("Missing workshop id parameter!");
        }

        [HttpPut]
        public ActionResult Edit(int id, Workshop workshopRequest) {
            try {
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
                    return RedirectToAction("Index");
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
            if (workshop != null) {
                db.Workshops.Remove(workshop);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Couldn't find the workshop with id " + id.ToString());
        }
    }
}