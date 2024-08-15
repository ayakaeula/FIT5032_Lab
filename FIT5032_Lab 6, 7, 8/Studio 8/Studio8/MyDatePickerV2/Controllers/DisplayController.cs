using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyDatePickerV2.Models;

namespace MyDatePickerV2.Controllers
{
    public class DisplayController : Controller
    {
        private MyAppointmentDBEntities db = new MyAppointmentDBEntities();

        // GET: Display
        public ActionResult Index()
        {
            return View(db.Appointments.ToList());
        }

        // GET: Display/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Display/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ContentResult GetImage()
        {
            var Height = Int32.Parse(Request.Form["Height"]);
            var Width = Int32.Parse(Request.Form["Width"]);

            var FilePath = "";
            if (Height >= 900 && Width >= 900)
            {
                FilePath = Server.MapPath("~/Uploads/") + "MacBook-Big.png";
            }
            else
            {
                FilePath = Server.MapPath("~/Uploads/") + "MacBook-Small.png";
            }

            byte[] FileBytes = System.IO.File.ReadAllBytes(FilePath);
            string FileBase64 = Convert.ToBase64String(FileBytes,0,FileBytes.Length);
            return Content(FileBase64);
        }

        // GET: Display/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Display/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StudentID,EngineerID,Date")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appointment);
        }

        // GET: Display/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Display/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
