using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DataAccessLayer.models;
using DuventusEmployees.Data;

namespace DuventusEmployees.Controllers.API
{
    public class PayrollsController : ApiController
    {
        private DuventusEmployeesContext db = new DuventusEmployeesContext();

        // GET: api/Payrolls
        public IQueryable<Payroll> GetPayrolls()
        {
            return db.Payrolls;
        }
        [HttpGet]
        [Route("api/Payrolls/CalculateTotal/{EmployeeId}/{HoursWorked}")]
        public float CalculateTotal(Guid EmployeeId, float HoursWorked)
        {
            var employee = db.Employees.Where(x => x.Id == EmployeeId).Include(x => x.EmployeeType).FirstOrDefault();

            if (HoursWorked <= employee.HoursWorked)
            {
                float total = 0;
                total = (float)(employee.SalaryPerHour * HoursWorked);

                return total;
            }
            else
            {
                float total = 0;
                var ExcessHour = HoursWorked - employee.HoursWorked;
                float FirstBonusPercent = employee.EmployeeType.FirstBonus;
                total = (float)(employee.SalaryPerHour * employee.HoursWorked);
                if (employee.EmployeeType.HasSecondBonus == false || ExcessHour <= 12)
                {
                    total = (float)(total + (ExcessHour * employee.SalaryPerHour) + (((ExcessHour * employee.SalaryPerHour) * FirstBonusPercent) / 100));
                }
                else
                {
                        var ExcessBonusHours = ExcessHour - 12;

                        FirstBonusPercent = (float)((employee.EmployeeType.FirstBonus * (12 * employee.SalaryPerHour)) / 100);
                        float SecondBonusPercent = (float)((employee.EmployeeType.SecondBonus * (ExcessBonusHours * employee.SalaryPerHour)) / 100);

                        total = total + (12 * employee.SalaryPerHour) + FirstBonusPercent;

                        total = total + (float)(ExcessBonusHours * employee.SalaryPerHour) + SecondBonusPercent;
                    
                }

                return total;
            }
        }
        // GET: api/Payrolls/5
        [ResponseType(typeof(Payroll))]
        public async Task<IHttpActionResult> GetPayroll(Guid id)
        {
            Payroll payroll = await db.Payrolls.FindAsync(id);
            if (payroll == null)
            {
                return NotFound();
            }

            return Ok(payroll);
        }

        // PUT: api/Payrolls/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPayroll(Guid id, Payroll payroll)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != payroll.Id)
            {
                return BadRequest();
            }

            db.Entry(payroll).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayrollExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Payrolls
        [ResponseType(typeof(Payroll))]
        public async Task<IHttpActionResult> PostPayroll(Payroll payroll)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Payrolls.Add(payroll);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = payroll.Id }, payroll);
        }

        // DELETE: api/Payrolls/5
        [ResponseType(typeof(Payroll))]
        public async Task<IHttpActionResult> DeletePayroll(Guid id)
        {
            Payroll payroll = await db.Payrolls.FindAsync(id);
            if (payroll == null)
            {
                return NotFound();
            }

            db.Payrolls.Remove(payroll);
            await db.SaveChangesAsync();

            return Ok(payroll);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PayrollExists(Guid id)
        {
            return db.Payrolls.Count(e => e.Id == id) > 0;
        }
    }
}