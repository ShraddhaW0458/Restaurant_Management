using Restaurant_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management.IServices
{
     public interface ILogin
    {
        
       string SaveUser(User obj);
    }
}
