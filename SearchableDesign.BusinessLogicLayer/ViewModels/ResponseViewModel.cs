using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchableDesign.Repository.ViewModels
{
    public class ResponseViewModel
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public int LastId { get; set; }
        public string CaseRecNo { get; set; }
    }
    public static class MsgBox
    {
        public static string save_msg = "Successfully Saved.";
        public static string file_upload_msg = "File uploaded successfully.";
        public static string update_msg = "Successfully Updated.";
        public static string delete_msg = "Successfully Deleted.";
        public static string id_not_found = "Id Not Found";
        public static string success_status = "success";
        public static string failed_status = "failed";
        public static string import_msg = "Data Import Successfully";
        public static string genetate_msg = "Generated Successfully";
        public static string user_msg = "User already exists";
        public static string user_not_found = "User doesnot exists";
    }
}
