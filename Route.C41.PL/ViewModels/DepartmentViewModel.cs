using System;

namespace Route.C41.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime DateOfCreation { get; set; }

        public string Description { get; set; }
    }
}