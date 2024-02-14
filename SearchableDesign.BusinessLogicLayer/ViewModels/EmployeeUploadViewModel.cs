using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchableDesign.Repository.ViewModels
{
    public class EmployeeUploadViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public DateTime DOB { get; set; }
        public Decimal Salary { get; set; } 
        public string Gender { get; set; }
        public string Designation { get;set; }
        public string StatusClass { get;set; }  
        public string StatusMsg { get;set; }
        public int sn { get; set; } 
    }
}
