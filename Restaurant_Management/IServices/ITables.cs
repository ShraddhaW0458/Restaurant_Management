using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management.IServices
{
    public interface ITables
    {
        bool Save(tblTable obj);

        tblTable Get(int Id);

        bool Delete(int Id);

        List<tblTable> GetAll();

        List<tblTable> GetAll_ByBranchId(int Branch_Id);

    }
}
