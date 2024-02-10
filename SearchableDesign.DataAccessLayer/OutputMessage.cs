using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchableDesign.Infrastructure
{
    public class OutputMessage
    {
        private string _message = string.Empty;
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        private string _status = string.Empty;
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
        private string errormessage = string.Empty;
        public string ErrorMessage
        {
            get { return errormessage; }
            set { errormessage = value; }
        }
        private SqlParameterCollection _msg_param = null;
        public SqlParameterCollection Msg_Params
        {
            get { return _msg_param; }
            set { _msg_param = value; }
        }
        public OutputMessage()
        {

        }
    }
}
