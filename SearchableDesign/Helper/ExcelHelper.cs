using OfficeOpenXml;
using SearchableDesign.Repository.ViewModels;
using System.Data;
using System.Reflection;
using System.Text;

namespace SearchableDesign.UI.Helper
{
    public class ExcelHelper
    {
        public static void ValidateEmployeeData(ref int Total, ref string StatusClass, DataTable dt, List<EmployeeUploadViewModel> model, ref bool save_flag1)
        {
            int row = 1;
            List<bool> save_flag = new List<bool>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    EmployeeUploadViewModel VM = new EmployeeUploadViewModel();
                    List<string> errors = new List<string>();
                   
                    StringBuilder sb = new StringBuilder();
                    if (string.IsNullOrEmpty(Convert.ToString(dr["FullName"])))
                    {
                        errors.Add("Employee name is required");
                    }
                    else VM.FullName = Convert.ToString(dr["FullName"]);

                    if (string.IsNullOrEmpty(Convert.ToString(dr["DOB"])))
                    {
                        errors.Add("Date of Birth is required");
                    }
                    else VM.DOB = Convert.ToDateTime(dr["DOB"]);

                    if (string.IsNullOrEmpty(Convert.ToString(dr["Gender"])))
                    {
                        errors.Add("Gender is required");
                    }
                    else VM.Gender = Convert.ToString(dr["Gender"]);

                    if (string.IsNullOrEmpty(Convert.ToString(dr["Designation"])))
                    {
                        errors.Add("Designation is required");
                    }
                    else VM.Designation = Convert.ToString(dr["Designation"]);

                    if (string.IsNullOrEmpty(Convert.ToString(dr["Salary"])))
                    {
                        errors.Add("Salary is required");
                    }
                    else VM.Salary = Convert.ToDecimal(dr["Salary"]);

                    if (errors.Count() > 0)
                    {
                        Total++;
                        StatusClass = "bg-danger";
                        sb.Append("<a class=\"mytooltip uploaderror\" id=\"uploaderror_" + row + "\" href=\"javascript:void(0)\"><i class=\"fa fa-warning text-inverse\"></i><span class=\"tooltip-content5\"><span class=\"tooltip-text3\"><span class=\"tooltip-inner2\"><ul>");
                        foreach (string msg in errors)
                        {
                            sb.Append("<li>" + msg.ToString() + "</li>");
                        }
                        sb.Append("</ul></span></span></span>");
                        save_flag.Add(false);
                    }
                    else
                    {
                        StatusClass = "";
                        sb.Append("<i class=\"fa fa fa-check text-success\"></i>");
                        save_flag.Add(true);
                    }
                    VM.StatusMsg = sb.ToString();
                    VM.StatusClass = StatusClass;
                    VM.sn = row;
                    row++;

                    model.Add(VM);
                }
            }

            save_flag1 = !save_flag.Contains(false); 
        }


    }
}
