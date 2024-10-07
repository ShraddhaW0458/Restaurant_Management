using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management.IServices
{
    public interface IState
    {
        bool Save(tblState obj);

        tblState Get(int Id);

        List<tblState> GetAll();
    }
}
