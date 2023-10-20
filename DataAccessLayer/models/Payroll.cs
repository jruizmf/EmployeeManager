using MLGDataAccessLayer.models.common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.models.enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.models
{
    public class Payroll : BaseEntity
    {
        [Display(Name = "Empleado")]
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }


        [Display(Name = "Semana Numero")]
        [Required(ErrorMessage = "El numero de semana es requerido.")]
        public int Week { get; set; }

        [Display(Name = "Horas trabajadas")]
        [Required(ErrorMessage = "El numero de semana es requerido.")]
        public float HoursWorked { get; set; }

        [Display(Name = "Total")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "El calculo total no ha sido generado.")]
        public float total { get; set; }
    }
}
