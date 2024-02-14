using SearchableDesign.Infrastructure;
using SearchableDesign.Repository.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchableDesign.Repository.Repository.AccountRepository
{
    public class AccountRepository:IAccountRepository
    {
        public ResponseViewModel GetUserProfile(string usercode)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                     new SqlParameter("@user_code",usercode),
                     new SqlParameter("@v_out_status", SqlDbType.NVarChar, 50),
                     new SqlParameter("@v_out_message", SqlDbType.NVarChar, 500)
                };
                var result = DAO.ExecuteStoredProcedure("sp_UserLogin", param);
                return new ResponseViewModel() { Message = result.Message, Status = result.Status };
            }
            catch (Exception ex)
            {

                return new ResponseViewModel() { Message = ex.Message, Status = "Failed" };
            }

        }
    }
}
