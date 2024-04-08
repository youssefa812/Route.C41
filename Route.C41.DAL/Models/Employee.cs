using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.DAL.Models
{
    public enum Gender
    {
        [EnumMember(Value = "Male")]
        Male = 1,
        [EnumMember(Value = "female")]
        Female = 2
    }
    public enum EmpType
    {
        [EnumMember(Value = "Full Time")]
        FullTime = 1,
        [EnumMember(Value = "Part Time")]
        PartTime = 2
    }
    public class Employee : ModelBase
    {
        public string Name { get; set; }

        public int Age { get; set; }
        public string Address { get; set; }

        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }

        public EmpType EmployeeType { get; set; }

        public Gender Gender { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

		public string ImageName { get; set; }

		// ForeginKey
		public int? DepartmentId { get; set; }

        // Navigational Property
        // [InverseProperty(nameof(Models.Department.Employees))]
        public virtual Department Department { get; set; }
    }
}
