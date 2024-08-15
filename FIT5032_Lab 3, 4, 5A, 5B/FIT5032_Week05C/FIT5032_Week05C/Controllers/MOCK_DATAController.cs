using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_Week05C.Models;

namespace FIT5032_Week05C.Controllers
{
    public class MOCK_DATAController : Controller
    {
        private FIT5032_Week05C_Models db = new FIT5032_Week05C_Models();

        // GET: MOCK_DATA
        public ActionResult Index()
        {
            return View(db.MOCK_DATA.ToList());
        }

        // GET: MOCK_DATA/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MOCK_DATA mOCK_DATA = db.MOCK_DATA.Find(id);
            if (mOCK_DATA == null)
            {
                return HttpNotFound();
            }
            return View(mOCK_DATA);
        }

        // GET: MOCK_DATA/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MOCK_DATA/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,first_name,last_name,email,gender,ip_address")] MOCK_DATA mOCK_DATA)
        {
            if (ModelState.IsValid)
            {
                db.MOCK_DATA.Add(mOCK_DATA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mOCK_DATA);
        }

        // GET: MOCK_DATA/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MOCK_DATA mOCK_DATA = db.MOCK_DATA.Find(id);
            if (mOCK_DATA == null)
            {
                return HttpNotFound();
            }
            return View(mOCK_DATA);
        }

        // POST: MOCK_DATA/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,first_name,last_name,email,gender,ip_address")] MOCK_DATA mOCK_DATA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mOCK_DATA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mOCK_DATA);
        }

        // GET: MOCK_DATA/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MOCK_DATA mOCK_DATA = db.MOCK_DATA.Find(id);
            if (mOCK_DATA == null)
            {
                return HttpNotFound();
            }
            return View(mOCK_DATA);
        }

        // POST: MOCK_DATA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MOCK_DATA mOCK_DATA = db.MOCK_DATA.Find(id);
            db.MOCK_DATA.Remove(mOCK_DATA);
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
