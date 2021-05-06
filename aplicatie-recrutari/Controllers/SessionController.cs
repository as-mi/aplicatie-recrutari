using aplicatie_recrutari.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aplicatie_recrutari.Controllers
{
    public class SessionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize(Roles = "Admin, Editor")]
        public ActionResult Index() {
            List<Recruitment_Session> recruitment_sessions = db.Recruitment_Sessions.ToList();
            ViewBag.Recruitment_Sessions = recruitment_sessions;
            return View();
        }
        [Authorize(Roles = "Admin, Editor")]
        public ActionResult Show(int? id) {
            if (id.HasValue) {
                Recruitment_Session recruitment_session = db.Recruitment_Sessions.Find(id);
                if (recruitment_session != null) {
                    return View(recruitment_session);
                }
                return HttpNotFound("Couldn't find the session with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing session id parameter!");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult New() {
            Recruitment_Session recruitment_session = new Recruitment_Session();
            return View(recruitment_session);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult New(Recruitment_Session sessionRequest) {
            try {
                if (ModelState.IsValid) {
                    db.Recruitment_Sessions.Add(sessionRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(sessionRequest);
            }
            catch (Exception) {
                return View(sessionRequest);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Edit(int? id) {
            if (id.HasValue) {
                Recruitment_Session recruitment_session = db.Recruitment_Sessions.Find(id);
                if (recruitment_session == null) {
                    return HttpNotFound("Couldn't find the session with id " + id.ToString());
                }
                return View(recruitment_session);
            }
            return HttpNotFound("Missing session id parameter!");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public ActionResult Edit(int id, Recruitment_Session sessionRequest) {
            try {
                if (ModelState.IsValid) {
                    Recruitment_Session recruitment_session = db.Recruitment_Sessions
                    .SingleOrDefault(b => b.SessionId.Equals(id));
                    if (TryUpdateModel(recruitment_session)) {
                        recruitment_session.Period = sessionRequest.Period;
                        recruitment_session.Year = sessionRequest.Year;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(sessionRequest);
            }
            catch (Exception) {
                return View(sessionRequest);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public ActionResult Delete(int id) {
            Recruitment_Session recruitment_session = db.Recruitment_Sessions.Find(id);
            if (recruitment_session != null) {
                db.Recruitment_Sessions.Remove(recruitment_session);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Couldn't find the session with id " + id.ToString());
        }
    }
}