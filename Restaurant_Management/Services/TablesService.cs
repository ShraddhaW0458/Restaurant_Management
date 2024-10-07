using Restaurant_Management.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant_Management.Services
{
    public class TablesService : ITables
    {
        Restaurant_ManagementEntities1 ctx = new Restaurant_ManagementEntities1();
        public bool Delete(int Id)
        {
            try
            {
                var obj = ctx.tblTables.Find(Id);
                if (obj != null)
                {
                    ctx.tblTables.Remove(obj);
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

        public tblTable Get(int Id)
        {
            return ctx.tblTables.Find(Id);
        }

        public List<tblTable> GetAll()
        {
           return ctx.tblTables.ToList();
        }

        public List<tblTable> GetAll_ByBranchId(int Branch_Id)
        {
            return ctx.tblTables.Where(w=>w.BranchId == Branch_Id).ToList();
        }


        public bool Save(tblTable obj)
        {
            try
            {
                ctx.tblTables.Attach(obj);
                ctx.Entry(obj).State = obj.Id > 0 ? 
                    System.Data.Entity.EntityState.Modified 
                    : System.Data.Entity.EntityState.Added;

                ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }
    }
}