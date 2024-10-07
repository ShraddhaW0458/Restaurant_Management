using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management.IServices
{
    public interface ICity
    {
        bool Save(tblCity obj);

        tblCity Get(int Id);

        List<tblCity> GetAll();
    }
}
