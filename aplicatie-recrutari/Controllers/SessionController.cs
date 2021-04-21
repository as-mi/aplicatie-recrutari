using aplicatie_recrutari.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aplicatie_recrutari.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SessionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index() {
            List<Recruitment_Session> recruitment_sessions = db.Recruitment_Sessions.ToList();
            ViewBag.Recruitment_Sessions = recruitment_sessions;
            return View();
        }

        [HttpGet]
        public ActionResult New() {
            Recruitment_Session recruitment_session = new Recruitment_Session();
            /*recruitment_session.Genres = new List<Genre>();*/
            return View(recruitment_session);
        }

        [HttpPost]
        public ActionResult New(Recruitment_Session sessionRequest) {
            try {
                if (ModelState.IsValid) {
                    /*sessionRequest.Publisher = db.Publishers
                   .FirstOrDefault(p => p.PublisherId.Equals(1));*/
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

        [HttpPut]
        public ActionResult Edit(int id, Recruitment_Session sessionRequest) {
            try {
                if (ModelState.IsValid) {
                    /*Recruitment_Session book = db.Recruitment_Sessions
                   .Include("Publisher")
                    .SingleOrDefault(b => b.BookId.Equals(id));*/
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