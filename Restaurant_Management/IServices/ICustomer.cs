using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management.IServices
{
    public interface ICustomer
    {
        bool Save(tblCustomer obj);

        tblCustomer Get(int Id);

        bool Delete(int Id);

        List<tblCustomer> GetAll();

        int Save_Cust(tblCustomer obj);
    }
}
