﻿using Restaurant_Management.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant_Management.Services
{
    public class MasterService : IMaster
    {
        Restaurant_ManagementEntities1 ctx = new Restaurant_ManagementEntities1();

        public int SaveOrder(tblOrder obj)
        {
            ctx.tblOrders.Attach(obj);
            ctx.Entry(obj).State = obj.Id > 0 ? System.Data.Entity.EntityState.Modified : System.Data.Entity.EntityState.Added;

            ctx.SaveChanges();
            return obj.Id;
        }

        public int SaveReservation(tblReservation obj)
        {
            ctx.tblReservations.Attach(obj);
            ctx.Entry(obj).State = obj.Id > 0 ? System.Data.Entity.EntityState.Modified : System.Data.Entity.EntityState.Added;

            ctx.SaveChanges();
            return obj.Id;
        }

        public List<tblOrderItem> GetOrderItemListByOrderId(int Id)
        {
            
            return ctx.tblOrderItems.Where(w => w.OrderId == Id).Select(s => new { s.Id, s.MenuId, s.Quantity, s.ItemPrice, s.TotalPrice, s.Portion, s.OrderId }).ToList();

        }
    }
}