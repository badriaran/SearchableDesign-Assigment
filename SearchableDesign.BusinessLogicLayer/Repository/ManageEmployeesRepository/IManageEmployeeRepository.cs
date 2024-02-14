using SearchableDesign.Repository.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchableDesign.Repository.Repository.ManageEmployees
{
    public interface IManageEmployeeRepository
    {
        IEnumerable<EmployeeViewModel> GetEmployees(EmployeeSearchVM model);
        ResponseViewModel DeleteEmployee(EmployeeViewModel model);
        IEnumerable<EmployeeViewModel> GetEmployeeToExcel(EmployeeSearchVM model);
        ResponseViewModel AddEmployee(EmployeeViewModel model);
        ResponseViewModel UpdateEmployee(EmployeeViewModel model);
    }
}
