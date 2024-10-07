using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management.IServices
{
    public interface IOrderItems
    {
        bool save(tblOrderItem obj);

        tblOrderItem Get(int Id);

        bool Delete(int Id);

        List<tblOrderItem> GetAll();

        int Save_OrderItems(tblOrderItem obj);
    }
}
