using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NuGet.Configuration;
using SearchableDesign.Repository;
using SearchableDesign.Repository.Repository.ManageEmployees;
using SearchableDesign.Repository.ViewModels;
using SearchableDesign.UI.Models;
using System.Data;
using System.Text;

namespace SearchableDesign.UI.Controllers
{
    public class ManageEmployeeController : Controller
    {
        private readonly IManageEmployeeRepository _manageEmployeeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IWebHostEnvironment _hostingEnvironment;
        public ManageEmployeeController(IManageEmployeeRepository manageEmployeeRepository, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment)
        {
            this._manageEmployeeRepository = manageEmployeeRepository;
            this._httpContextAccessor = httpContextAccessor;
            this._hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetEmployees(EmployeeSearchVM model)
        {
            JsonOutput json = new JsonOutput();
            try
            {
                var employeeLists = _manageEmployeeRepository.GetEmployees(model);
                json.Success = true;
                json.Result = employeeLists;
                json.Result2 = model.Total;
            }
            catch (Exception ex)
            {
                json.Success = false;
                json.Message = ex.Message;
            }
            return Json(json);

        }
        [HttpPost]
        public JsonResult Delete(string Id)
        {
            JsonOutput json = new JsonOutput();
            EmployeeViewModel model = new EmployeeViewModel();
            try
            {
                model.Id = Convert.ToString(Id);
                var result = _manageEmployeeRepository.DeleteEmployee(model);
                if (result.Status == "SUCCESS")
                {
                    json.Success = true;
                    json.Result = result.Message;
                }
            }
            catch (Exception ex)
            {
                json.Success = false;
                json.Message = ex.Message;
            }
            return Json(json);
        }
        [HttpPost]
        public JsonResult ExportToExcel(EmployeeSearchVM model)
        {
            JsonOutput json = new JsonOutput();
            try
            {
                var employeelists = _manageEmployeeRepository.GetEmployeeToExcel(model);
                var dt = Utility.ToDataTable<EmployeeViewModel>(employeelists.AsQueryable());
                _httpContextAccessor.HttpContext.Session.Set("ExcelReport", Utility.ExportToExcel(dt));
                json.Success = true;
                return Json(json);
            }
            catch (Exception ex)
            {
                json.Success = false;
                json.Message = ex.Message;
            }
            return Json(json);
        }
        
        public IActionResult Download()
        {
            if (_httpContextAccessor.HttpContext.Session.TryGetValue("ExcelReport", out byte[] data))
            {
                _httpContextAccessor.HttpContext.Session.Remove("ExcelReport");

                string fileName = "EmployeeLists" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
                return File(data, "application/octet-stream", fileName);

            }
            else
            {
                return new EmptyResult();
            }
        }
        [HttpPost]
        public JsonResult AddNewEmployee(ImageUploadViewModel img,EmployeeViewModel model)
        {
            JsonOutput json = new JsonOutput();
            try
            {
                string ProfileImagePath="";
                if (img.ImageSize > 0)
                {
                    ProfileImagePath = SaveImage(img);
                }
                model.ProfileImagePath = ProfileImagePath;
                var result = _manageEmployeeRepository.AddEmployee(model);
                if(result.Status== "SUCCESS")
                {
                    json.Success=true;
                    json.Message=result.Message;
                }
                else
                {
                    json.Success = false;
                    json.Message = result.Message;
                }
                return Json(json);
            }
            catch (Exception ex)
            {
                json.Success = false;
                json.Message = ex.Message;
            }
            return Json(json);
        }
        private string SaveImage(ImageUploadViewModel model)
        {
            string path = "img";
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string filePath = sWebRootFolder + "\\img"; ;
            try
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(path);
                }
                var newFileName = model.ImageName.Split('.');
                string fName = newFileName[0];
                string ext = newFileName[1];

                model.ImageName = fName + "_" + Guid.NewGuid().ToString("N").Substring(0, 5) + "." + ext;
                string bData = model.Image.Substring(model.Image.IndexOf(",") + 1);
                byte[] binaryData = Convert.FromBase64String(bData);
                MemoryStream ms = new MemoryStream(binaryData);
                // write to file
                filePath = filePath + "\\" + model.ImageName;
                FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);
                file.Close();
                ms.Close();
                model.ImageFullPath ="/" +path+"/"+ model.ImageName;
                return model.ImageFullPath;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public JsonResult UpdateEmployee(ImageUploadViewModel img, EmployeeViewModel model)
        {
            JsonOutput json = new JsonOutput();
            try
            {
                string ProfileImagePath = "";
                if (img.ImageSize > 0)
                {
                    ProfileImagePath = SaveImage(img);
                }
                model.ProfileImagePath = ProfileImagePath;
                var result = _manageEmployeeRepository.UpdateEmployee(model);
                if (result.Status == "SUCCESS")
                {
                    json.Success = true;
                    json.Message = result.Message;
                }
                else
                {
                    json.Success = false;
                    json.Message = result.Message;
                }
                return Json(json);
            }
            catch (Exception ex)
            {
                json.Success = false;
                json.Message = ex.Message;
            }
            return Json(json);
        }
    }
}
