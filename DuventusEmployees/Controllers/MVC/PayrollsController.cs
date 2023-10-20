using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.models;
using DuventusEmployees.Data;
using Newtonsoft.Json.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;
using System.Configuration;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Web.Routing;

namespace DuventusEmployees.Controllers.MVC
{
    public class PayrollsController : Controller
    {
        private DuventusEmployeesContext db = new DuventusEmployeesContext();

        // GET: Payrolls
        public ActionResult Index()
        {
            var payrolls = db.Payrolls.Include(p => p.Employee);
            return View(payrolls.ToList());
        }

        // GET: Payrolls/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payroll payroll = db.Payrolls.Find(id);
            if (payroll == null)
            {
                return HttpNotFound();
            }
            return View(payroll);
        }

        // GET: Payrolls/Create
        public ActionResult Create()
        {
           
          
                ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name");
          
            

            return View();
        }

        // POST: Payrolls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,EmployeeId,Week,HoursWorked, total")] Payroll payroll)
        {
            if (ModelState.IsValid)
            {
                db.Payrolls.Add(payroll);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", payroll.EmployeeId);
            return View(payroll);
        }

        // GET: Payrolls/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payroll payroll = db.Payrolls.Find(id);



            if (payroll == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", payroll.EmployeeId);
            return View(payroll);
        }

      

        // POST: Payrolls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeId,Week,HoursWorked, total")] Payroll payroll)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payroll).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", payroll.EmployeeId);
            return View(payroll);
        }

        // GET: Payrolls/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payroll payroll = db.Payrolls.Find(id);
            if (payroll == null)
            {
                return HttpNotFound();
            }
            return View(payroll);
        }

        // POST: Payrolls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Payroll payroll = db.Payrolls.Find(id);
            db.Payrolls.Remove(payroll);
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
