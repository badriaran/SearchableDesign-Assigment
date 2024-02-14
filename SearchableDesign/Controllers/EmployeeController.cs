using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table.PivotTable;
using SearchableDesign.Domain.Common;
using SearchableDesign.Repository;
using SearchableDesign.Repository.Repository.EmployeeRepository;
using SearchableDesign.Repository.ViewModels;
using SearchableDesign.UI.Helper;
using SearchableDesign.UI.Models;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace SearchableDesign.UI.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private IWebHostEnvironment _hostingEnvironment;
        //private readonly IHttpContextAccessor HttpContextAccessor;
        public EmployeeController(IEmployeeRepository employeeRepository, IWebHostEnvironment hostingEnvironment)
        {
            this._employeeRepository = employeeRepository;
            this._hostingEnvironment = hostingEnvironment;
           
        }
        public IActionResult Index()
        {
            return View();
        }
        private string SaveFile(FileUploadViewModel model)
        {
            string path = "UploadedFiles";
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string filePath = _hostingEnvironment.ContentRootPath + "UploadedFiles";
            try
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(path);
                }
                var newFileName = model.DocumentName.Split('.');
                string fName = newFileName[0];
                string ext = newFileName[1];

                model.DocumentName = fName + "_" + Guid.NewGuid().ToString("N").Substring(0, 5) + "." + ext;
                string bData = model.Document.Substring(model.Document.IndexOf(",") + 1);
                byte[] binaryData = Convert.FromBase64String(bData);
                MemoryStream ms = new MemoryStream(binaryData);
                // write to file
                filePath = filePath + "\\" + model.DocumentName;
                FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);
                file.Close();
                ms.Close();
                model.DocumentFullPath = filePath;
                return ext;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        
        [HttpPost]
        public JsonResult UploadFile(FileUploadViewModel model)
        {
            JsonOutput json = new JsonOutput();
            int Total = 0;
            try
            {
                string extension = SaveFile(model);
                DataTable dt = new DataTable();
                List<int> SkippedRow = new List<int>();

                if (extension == "xlsx")
                {
                    dt = Utility.ReadExcelFile(model.DocumentFullPath).Item1;
                    SkippedRow= Utility.ReadExcelFile(model.DocumentFullPath).Item2;
                }
                else if (extension == "csv")
                {
                    dt = Utility.ReadCSVFile(model.DocumentFullPath);
                }

                List<EmployeeUploadViewModel> viewModel = new List<EmployeeUploadViewModel>();

                string StatusClass = "";
                string strSno = "";
                bool save_flag1 = false ;

                ExcelHelper.ValidateEmployeeData(ref Total,  ref StatusClass, dt, viewModel, ref save_flag1);

                json.Success = true;
                json.Result = viewModel;
                json.Result2 = SkippedRow;
                json.Result3 = save_flag1;
                json.Message = "File Uploaded Successfully";
                
                return Json(json);
            }
            catch (Exception ex)
            {
                json.Success = false;
                json.Message = ex.Message;
                return Json(json);

            }

        }
        public IActionResult DownloadTemplate()
        {
            byte[] fileBytes = null;
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = @"Template";
            sFileName = string.Format("{0}{1}", sFileName, ".xlsx");
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));

            using (ExcelPackage package = new ExcelPackage(file))
            {
                
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("EmployeeTemplate");

                string[] columnnArray = new string[] {
                    "SN","Full Name","Date Of Birth","Gender","Salary","Designation"
                };

                for (int i = 1; i <= columnnArray.Count(); i++)
                {
                    worksheet.Cells[1, i].Value = columnnArray[i - 1];
                    worksheet.Column(i).Width = 20;
                    worksheet.Column(i).AutoFit();
                }

                using (var range = worksheet.Cells[1, 1, 1, 6])
                {
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(0, 194, 146));
                    range.Style.Font.Color.SetColor(Color.White);
                }
                using (ExcelRange Rng = worksheet.Cells[1, 1, 20, 6])
                {
                    Rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    Rng.Style.Border.Top.Color.SetColor(Color.Black);
                    Rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    Rng.Style.Border.Left.Color.SetColor(Color.Black);
                    Rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    Rng.Style.Border.Right.Color.SetColor(Color.Black);
                    Rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    Rng.Style.Border.Bottom.Color.SetColor(Color.Black);
                    Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    Rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    Rng.Style.WrapText = true;
                }

                fileBytes = package.GetAsByteArray();
                if (fileBytes == null || fileBytes.Length == 0)
                {
                    return NotFound();
                }
            }
            return File(
                 fileContents: fileBytes,
                 contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                 fileDownloadName: sFileName
             );
        }
        public JsonResult SaveUploadData(List<EmployeeUploadViewModel> model)
        {
            JsonOutput json = new JsonOutput();
            try
            { 
                if (model != null)
                {
                    var response = _employeeRepository.SaveUploadData(model);
                    if(response.Status== "success")
                    {
                        json.Success = true;
                        json.Result = response.Message;
                        return Json(json);
                    }                
                }
            }
            catch (Exception ex)
            {
                json.Success = false;
                json.Message= ex.Message;
                return Json(json);
            }
            return Json(json);
        }
    }
}
