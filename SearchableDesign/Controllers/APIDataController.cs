using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.DotNet.MSIdentity.Shared;
using Newtonsoft.Json;
using SearchableDesign.Repository.ViewModels;
using SearchableDesign.UI.Helper;
using SearchableDesign.UI.Models;

using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Policy;
using System.Text.Json;
using System.Xml.Linq;

namespace SearchableDesign.UI.Controllers
{
    public class APIDataController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<JsonResult> GetData(int pageNo)
        {
            JsonOutput json = new JsonOutput();
            
            try
            {
                int perpage = 10;
                string apiUrl = "https://dummy.restapiexample.com/api/v1/employees";
                 
                var data= await APIHelper.ApiRespond(apiUrl);
                if (data.Success)
                {
                    var res = JsonConvert.DeserializeObject<APIRespondViewModel>(data.Result.ToString());
                    var perpagedata = res?.data?.GetRange(((perpage*pageNo)-perpage), (perpage * pageNo));
                    json.Result = res.data;
                    json.Result3 = perpagedata;
                    json.Result2 = res?.data.Count;
                    json.Success= true; 
                    json.Message = "Fetched Successfully !!";
                    return Json(json);
                }
                json.Message = "Error fetching data !!";
                json.Success = false;
                return Json(json);
            }
            catch (Exception ex)
            {
                json.Success = false;
                json.Message=ex.Message;
                return Json(json);
            }
            
        }

        [HttpPost]
        public async Task<JsonResult> GetDataWithPagination(int pageNo,List<Employee> list)
        {
            JsonOutput json = new JsonOutput();

            try
            {
                int perpage = 10;
               
                if (list.Count>0)
                {
                    var perpagedata = list.GetRange(((perpage * pageNo) - perpage),list.Count - ((perpage * pageNo) - perpage)> perpage? perpage: list.Count - ((perpage * pageNo) - perpage));
                    json.Result = perpagedata;

                    json.Result2 = list.Count;
                    json.Success = true;
                    
                    return Json(json);
                }                
                json.Success = false;
                return Json(json);
            }
            catch (Exception ex)
            {
                json.Success = false;
                json.Message = ex.Message;
                return Json(json);
            }

        }

    }
}
