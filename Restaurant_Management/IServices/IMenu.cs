using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management.IServices
{
    public interface IMenu
    {
        bool Save(tblMenu obj);

        tblMenu Get(int Id);

        bool Delete(int Id);

        List<tblMenu> GetAll();
    }
}
