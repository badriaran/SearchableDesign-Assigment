using SearchableDesign.Infrastructure;
using SearchableDesign.Repository.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchableDesign.Repository.Repository.EmployeeRepository
{
    public class EmployeeRepository: IEmployeeRepository
    {
        public ResponseViewModel SaveUploadData(List<EmployeeUploadViewModel> model)
        {
            try
            {
                foreach (var employee in model)
                {
                    string query = "INSERT INTO Employees (SN, FullName, DOB, Salary, Designation, Gender, UploadedBy, UploadedDate) " +
                                   "VALUES (@SN, @FullName, @DOB, @Salary, @Designation, @Gender, @UploadedBy, @UploadedDate)";

                    SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@SN", employee.sn),
                        new SqlParameter("@FullName", employee.FullName),
                        new SqlParameter("@DOB", employee.DOB),
                        new SqlParameter("@Salary", employee.Salary),
                        new SqlParameter("@Designation", employee.Designation),
                        new SqlParameter("@Gender", employee.Gender),
                        new SqlParameter("@UploadedBy", ""), 
                        new SqlParameter("@UploadedDate", DateTime.Now)
                    };

                    int result =DAO.IDU(query, CommandType.Text, param);
                }

                return new ResponseViewModel() { Message = MsgBox.save_msg, Status = MsgBox.success_status };
            }
            catch (Exception ex)
            {

                return new ResponseViewModel() { Exception = ex, Message = ex.Message, Status = MsgBox.failed_status };
            }
        }
    }
}
