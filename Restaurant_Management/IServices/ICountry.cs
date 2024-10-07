using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management.IServices
{
    public interface ICountry
    {
        bool Save(tblCountry obj);

        tblCountry Get(int Id);

        List<tblCountry> GetAll();
    }
}
