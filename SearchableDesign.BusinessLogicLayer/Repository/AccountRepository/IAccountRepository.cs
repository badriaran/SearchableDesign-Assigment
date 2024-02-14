using SearchableDesign.Repository.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchableDesign.Repository.Repository.AccountRepository
{
    public interface IAccountRepository
    {
        ResponseViewModel GetUserProfile(string usercode);
    }
}
