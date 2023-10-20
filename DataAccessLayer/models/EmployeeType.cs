using MLGDataAccessLayer.models.common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.models.enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.models
{
    public class EmployeeType : BaseEntity
    {
        
        [Display(Name = "Nombre")]
        [StringLength(30, ErrorMessage = "El nombre debería menos de 50 caracteres.")]
        [Required(ErrorMessage = "El nombre es requerido.")]
        public string Name { get; set; }
        [Display(Name = "Bonificación")]
        [Required(ErrorMessage = "La bonificación por tiempo extra es requerida.")]
        public int FirstBonus { get; set; }
        [Display(Name = "Tiene Segundo Bunus")]
        public bool HasSecondBonus { get; set; }

        [Display(Name = "Bonificación + 12hrs")]
        public int SecondBonus { get; set; }

        public double HoursWorked { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
