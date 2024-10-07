using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management.IServices
{
    public interface IMaster
    {
        int SaveReservation(tblReservation obj);
        int SaveOrder(tblOrder obj);

        List<tblOrderItem> GetOrderItemListByOrderId(int Id);
    }
}
