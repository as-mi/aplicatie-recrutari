using aplicatie_recrutari.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aplicatie_recrutari.Controllers
{
    [Authorize(Roles = "Editor, Admin")]
    public class InterviewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Show(int? id) {
            if (id.HasValue) {
                Interview interview = db.Interviews.Find(id);
                if (interview != null) {
                    int idProfil = interview.ProfileId;
                    Profile profile = db.Profiles.Find(idProfil);
                    ViewBag.lastName = profile.LastName;
                    ViewBag.firstName = profile.FirstName;
                    return View(interview);
                }
                return HttpNotFound("Couldn't find the interview with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing interview id parameter!");
        }

        [HttpGet]
        public ActionResult New(int? id) {
            Interview interview = new Interview();
            interview.AllDepartments = GetAllDepartments();
            if (id.HasValue)
            {
                interview.SessionId = id.Value;
            }
            return View(interview);
        }

        [HttpPost]
        public ActionResult New(Interview interviewRequest, int SessionId) {
            try {
                interviewRequest.AllDepartments = GetAllDepartments();
                interviewRequest.SessionId = SessionId;
                if (ModelState.IsValid) {
                    db.Interviews.Add(interviewRequest);
                    db.SaveChanges();
                    /*return RedirectToRoute(new { controller = "Interview", action = "Show", id = interviewRequest.InterviewId });*/
                    return RedirectToRoute(new { controller = "Session", action = "Show", id = interviewRequest.SessionId });
                }
                return View(interviewRequest);
            }
            catch (Exception) {
                return View(interviewRequest);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id, int? SessionId) {
            if (id.HasValue) {
                Interview interview = db.Interviews.Find(id);
                if (interview == null) {
                    return HttpNotFound("Couldn't find the interview with id " + id.ToString());
                }
                interview.AllDepartments = GetAllDepartments();
                if (SessionId.HasValue)
                {
                    interview.SessionId = SessionId.Value;
                }
                return View(interview);
            }
            return HttpNotFound("Missing interview id parameter!");
        }

        [HttpPut]
        public ActionResult Edit(int id, Interview interviewRequest, int SessionId) {
            try {
                interviewRequest.AllDepartments = GetAllDepartments();
                interviewRequest.SessionId = SessionId;
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
                    /*return RedirectToRoute(new { controller = "Interview", action = "Show", id = interview.InterviewId });*/
                    return RedirectToRoute(new { controller = "Session", action = "Show", id = interview.SessionId });
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
            int SessionId = interview.SessionId;
            if (interview != null) {
                db.Interviews.Remove(interview);
                db.SaveChanges();
                return RedirectToRoute(new { controller = "Session", action = "Show", id = SessionId });
            }
            return HttpNotFound("Couldn't find the interview with id " + id.ToString());
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