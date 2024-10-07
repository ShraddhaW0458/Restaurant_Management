using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management.IServices
{
    public interface IUserRole
    {
        bool Save(tblUserRole obj);

        tblUserRole Get(int Id);

        List<tblUserRole> GetAll();

    }
}
