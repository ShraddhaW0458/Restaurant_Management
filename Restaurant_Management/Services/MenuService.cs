using Restaurant_Management.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant_Management.Services
{
    public class MenuService : IMenu
    {
        Restaurant_ManagementEntities1 ctx = new Restaurant_ManagementEntities1();
        public bool Delete(int Id)
        {
            try
            {
                var obj = ctx.tblMenus.Find(Id);
                if (obj != null)
                {
                    ctx.tblMenus.Remove(obj);
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception )
            {


            }

            return false;
        }

        public tblMenu Get(int Id)
        {
            return ctx.tblMenus.Find(Id);
        }

        public List<tblMenu> GetAll()
        {
            return ctx.tblMenus.ToList();
        }

        public bool Save(tblMenu obj)
        {
            try
            {
                ctx.tblMenus.Attach(obj);
                ctx.Entry(obj).State = obj.Id > 0 ? 
                    System.Data.Entity.EntityState.Modified 
                    : System.Data.Entity.EntityState.Added;

                ctx.SaveChanges();
                return true;
            }
            catch (Exception )
            {

                return false;
            }
            
        }
    }
}