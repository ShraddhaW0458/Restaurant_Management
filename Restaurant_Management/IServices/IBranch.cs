using Restaurant_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Restaurant_Management.IServices
{
    public interface IBranch
    {
        //datatype MethodName (Parameters/Model);
        SelectList Country_Dropdown();

        SelectList State_Dropdown();

        SelectList City_Dropdown();

        List<Branch> BranchList();
    }
}
