using SearchableDesign.Repository.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchableDesign.Repository.Repository.EmployeeRepository
{
    public interface IEmployeeRepository
    {
        ResponseViewModel SaveUploadData(List<EmployeeUploadViewModel>model);
    }
}
