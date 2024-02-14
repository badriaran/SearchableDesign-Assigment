using SearchableDesign.Domain.Helper;
using SearchableDesign.Infrastructure;
using SearchableDesign.Repository.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SearchableDesign.Repository.Repository.ManageEmployees
{
    public class ManageEmployeeRepository:IManageEmployeeRepository
    {
        public IEnumerable<EmployeeViewModel> GetEmployees(EmployeeSearchVM model)
        {
            var employees = new List<EmployeeViewModel>();
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    
                    new SqlParameter("@ImportedDate",model.UploadedDate),
                    new SqlParameter("@Start",model.StartRow),
                    new SqlParameter("@End",model.EndRow),
                };
                DataTable ds = DAO.GetTable("sp_GetEmployees", CommandType.StoredProcedure, param);
                if (ds.Rows.Count>0)
                {
                    foreach (DataRow row in ds.Rows)
                    {
                        var employee = new EmployeeViewModel()
                        {
                            FullName =Helper.GetDBNUllString(row["FullName"]),
                            Designation = Helper.GetDBNUllString(row["Designation"]),
                            Salary =Convert.ToDecimal(row["Salary"]),
                            DOB = Convert.ToDateTime(row["DOB"]),
                            Gender = Helper.GetDBNUllString(row["Gender"]),
                            Id = Helper.GetDBNUllString(row["Id"]),
                            ProfileImagePath = Helper.GetDBNUllString(row["ProfileImagePath"])
                        };
                        employees.Add(employee);
                    }
                    model.Total = Convert.ToInt32(ds.Rows[0]["total"]);
                }
                return employees;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ResponseViewModel DeleteEmployee(EmployeeViewModel model)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                     new SqlParameter("@Id",model.Id),
                     new SqlParameter("@v_out_status",SqlDbType.VarChar, 50),
                     new SqlParameter("@v_out_message", SqlDbType.VarChar, 8000)
                };
                var result = DAO.ExecuteStoredProcedure("sp_DeleteEmployeeInfo", param);
                return new ResponseViewModel() { Message = result.Message, Status = result.Status };
            }
            catch (Exception ex)
            {
                return new ResponseViewModel() { Exception = ex, Message = ex.Message, Status = MsgBox.failed_status };
            }
        }
        public IEnumerable<EmployeeViewModel> GetEmployeeToExcel(EmployeeSearchVM model)
        {
            var employees = new List<EmployeeViewModel>();
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {

                    new SqlParameter("@ImportedDate",model.UploadedDate)
                };
                DataTable ds = DAO.GetTable("sp_GetEmployeesToExcel", CommandType.StoredProcedure, param);
                if (ds.Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Rows)
                    {
                        var employee = new EmployeeViewModel()
                        {
                            FullName = Helper.GetDBNUllString(row["FullName"]),
                            Designation = Helper.GetDBNUllString(row["Designation"]),
                            Salary = Convert.ToDecimal(row["Salary"]),
                            DOB = Convert.ToDateTime(row["DOB"]),
                            Gender = Helper.GetDBNUllString(row["Gender"]),
                            Id = Helper.GetDBNUllString(row["Id"]),
                        };
                        employees.Add(employee);
                    }
                    model.Total = Convert.ToInt32(ds.Rows[0]["total"]);
                }
                return employees;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ResponseViewModel AddEmployee(EmployeeViewModel model)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                     new SqlParameter("@Fullname",model.FullName),
                     new SqlParameter("@DOB",model.DOB),
                     new SqlParameter("@Salary",model.Salary),
                     new SqlParameter("@Designation",model.Designation),
                     new SqlParameter("@Gender",model.Gender),
                     new SqlParameter("@ProfileImagePath",model.ProfileImagePath),
                     
                     new SqlParameter("@v_out_status",SqlDbType.VarChar, 50),
                     new SqlParameter("@v_out_message", SqlDbType.VarChar, 8000)
                };
                var result = DAO.ExecuteStoredProcedure("sp_AddEmployeeInfo", param);
                return new ResponseViewModel() { Message = result.Message, Status = result.Status };
            }
            catch (Exception ex)
            {
                return new ResponseViewModel() { Exception = ex, Message = ex.Message, Status = MsgBox.failed_status };
            }
        }
        public ResponseViewModel UpdateEmployee(EmployeeViewModel model)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@Id",model.Id),
                     new SqlParameter("@Fullname",model.FullName),
                     new SqlParameter("@DOB",model.DOB),
                     new SqlParameter("@Salary",model.Salary),
                     new SqlParameter("@Designation",model.Designation),
                     new SqlParameter("@Gender",model.Gender),
                     new SqlParameter("@ProfileImagePath",model.ProfileImagePath),

                     new SqlParameter("@v_out_status",SqlDbType.VarChar, 50),
                     new SqlParameter("@v_out_message", SqlDbType.VarChar, 8000)
                };
                var result = DAO.ExecuteStoredProcedure("sp_UpdateEmployeeInfo", param);
                return new ResponseViewModel() { Message = result.Message, Status = result.Status };
            }
            catch (Exception ex)
            {
                return new ResponseViewModel() { Exception = ex, Message = ex.Message, Status = MsgBox.failed_status };
            }
        }
    }
}
