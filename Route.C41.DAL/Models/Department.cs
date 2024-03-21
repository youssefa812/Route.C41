using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.DAL.Models
{
    // Model
    public class Department
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        [Display(Name = "Date Od Creation")]
        public DateTime DateOfCreation { get; set; }
    }
}
