using MLGDataAccessLayer.models.common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.models.enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.models
{
    public class Employee : BaseEntity
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre de empleado es requerido.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debería tener entre 3 y 50 caracteres.")]
        public string Name { get; set; }
        [Display(Name = "Apellido Paterno")]
        [StringLength(50, ErrorMessage = "El apellido paterno debería menos de 50 caracteres.")]
        public string Lastname { get; set; }
        [Display(Name = "Apellido Materno")]
        [StringLength(50, ErrorMessage = "El apellido materno debería menos de 50 caracteres.")]
        public string Secondlastname { get; set; }

        [Display(Name = "Tipo")]
        public Guid EmployeeTypeId { get; set; }
        public EmployeeType EmployeeType { get; set; }

        [Display(Name = "RFC")]
        [Required(ErrorMessage = "El RFC de empleado es requerido.")]
        [RegularExpression("^([A-z]{4}[0-9]{6}[A-z0-9]{3})", ErrorMessage = "RFC Invalido")]
        [StringLength(14, MinimumLength = 1)]
        public string RFC { get; set; }
        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime Birthdate { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Salario por Hora")]
        public float SalaryPerHour { get; set; }
        [Display(Name = "Horas por Semana")]
        public double HoursWorked { get; set; }


        public ICollection<Payroll> Payrolls { get; set; }

    }
}
