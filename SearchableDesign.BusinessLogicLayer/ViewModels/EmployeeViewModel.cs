using Microsoft.AspNetCore.Http;
using SearchableDesign.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchableDesign.Repository.ViewModels
{
    public class EmployeeViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public DateTime DOB { get; set; }
        public Decimal Salary { get; set; }
        public string Gender { get; set; }
        public string Designation { get; set; }
        public string StatusClass { get; set; }
        public string StatusMsg { get; set; }
        public string ProfileImagePath { get; set; } = string.Empty;
    }
    public class EmployeeSearchVM : PaginationHelper
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public DateTime? DOB { get; set; }
        public Decimal Salary { get; set; }
        public string Gender { get; set; }
        public string Designation { get; set; }
        public DateTime? UploadedDate { get; set; }

        
    }
}
