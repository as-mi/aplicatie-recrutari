using aplicatie_recrutari.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aplicatie_recrutari.Controllers
{
    [Authorize(Roles = "Editor")]
    public class InterviewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index() {
            List<Interview> interviews = db.Interviews.ToList();
            ViewBag.Interviews = interviews;
            return View();
        }
        public ActionResult Show(int? id) {
            if (id.HasValue) {
                Interview interview = db.Interviews.Find(id);
                if (interview != null) {
                    return View(interview);
                }
                return HttpNotFound("Couldn't find the interview with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing interview id parameter!");
        }

        [HttpGet]
        public ActionResult New() {
            Interview interview = new Interview();
            return View(interview);
        }

        [HttpPost]
        public ActionResult New(Interview interviewRequest) {
            try {
                if (ModelState.IsValid) {
                    db.Interviews.Add(interviewRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(interviewRequest);
            }
            catch (Exception) {
                return View(interviewRequest);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id) {
            if (id.HasValue) {
                Interview interview = db.Interviews.Find(id);
                if (interview == null) {
                    return HttpNotFound("Couldn't find the interview with id " + id.ToString());
                }
                return View(interview);
            }
            return HttpNotFound("Missing interview id parameter!");
        }

        [HttpPut]
        public ActionResult Edit(int id, Interview interviewRequest) {
            try {
                if (ModelState.IsValid) {
                    Interview interview = db.Interviews
                    .SingleOrDefault(b => b.InterviewId.Equals(id));
                    if (TryUpdateModel(interview)) {
                        interview.Timestamp = interviewRequest.Timestamp;
                        interview.DepartmentId = interviewRequest.DepartmentId;
                        interview.ProfileId = interviewRequest.ProfileId;
                        interview.SessionId = interviewRequest.SessionId;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(interviewRequest);
            }
            catch (Exception) {
                return View(interviewRequest);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id) {
            Interview interview = db.Interviews.Find(id);
            if (interview != null) {
                db.Interviews.Remove(interview);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Couldn't find the interview with id " + id.ToString());
        }
    }
}