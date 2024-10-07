using Restaurant_Management.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant_Management.Services
{
    public class OrderItemsService : IOrderItems
    {
        Restaurant_ManagementEntities1 ctx = new Restaurant_ManagementEntities1();
        public bool Delete(int Id)
        {
            try
            {
                var obj = ctx.tblOrderItems.Find(Id);
                if (obj != null)
                {
                    ctx.tblOrderItems.Remove(obj);
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            
            return false;
        }

        public tblOrderItem Get(int Id)
        {
            return ctx.tblOrderItems.Find(Id);
        }

        public List<tblOrderItem> GetAll()
        {
            return ctx.tblOrderItems.ToList();
        }

        public bool save(tblOrderItem obj)
        {
            try
            {
                ctx.tblOrderItems.Attach(obj);
                ctx.Entry(obj).State = obj.Id > 0 ? System.Data.Entity.EntityState.Modified : System.Data.Entity.EntityState.Added;

                ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
           
        }

        public int Save_OrderItems(tblOrderItem obj)
        {
            try
            {
                ctx.tblOrderItems.Attach(obj);
                ctx.Entry(obj).State = obj.Id > 0 ?
                    System.Data.Entity.EntityState.Modified
                    : System.Data.Entity.EntityState.Added;

                ctx.SaveChanges();
                return obj.Id; // After SaveChanges, the Id will be updated for new entries
            }
            catch (Exception)
            {
                return -1; // Return an invalid Id in case of an error
            }
        }
    }
}