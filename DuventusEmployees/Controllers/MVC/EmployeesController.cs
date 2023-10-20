using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.models;
using DuventusEmployees.Data;

namespace DuventusEmployees.Controllers.MVC
{
    public class EmployeesController : Controller
    {
        private DuventusEmployeesContext db = new DuventusEmployeesContext();

        // GET: Employees
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.EmployeeType);
            return View(employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeTypeId = new SelectList(db.EmployeeTypes, "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Lastname,Secondlastname,EmployeeTypeId,RFC,Birthdate,SalaryPerHour,HoursWorked")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                var _employee = await db.Employees.Where(a => a.RFC == employee.RFC).FirstOrDefaultAsync();
                if (_employee != null)
                {
                    ModelState.AddModelError("RFC", "Empleado registrado con este RFC.");

                    ViewBag.EmployeeTypeId = new SelectList(db.EmployeeTypes, "ID", "Name"); // add this

                    return View(employee);
                }

                db.Employees.Add(employee);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            ViewBag.EmployeeTypeId = new SelectList(db.EmployeeTypes, "Id", "Name", employee.EmployeeTypeId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeTypeId = new SelectList(db.EmployeeTypes, "Id", "Name", employee.EmployeeTypeId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Lastname,Secondlastname,EmployeeTypeId,RFC,Birthdate,SalaryPerHour,HoursWorked")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeTypeId = new SelectList(db.EmployeeTypes, "Id", "Name", employee.EmployeeTypeId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
