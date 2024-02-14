using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchableDesign.Repository.ViewModels
{
    public class Employee
    {
        public int id { get; set; }
        public string employee_name { get; set; }
        public double employee_salary { get; set; }
        public int employee_age { get; set; }
        public string profile_image { get; set; }

    }
    public class APIRespondViewModel
    {
            public string status { get; set; }
            public List<Employee> data { get; set; }
            public string message { get; set; }
    }
}
