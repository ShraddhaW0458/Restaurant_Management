using Restaurant_Management.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant_Management.Services
{
    public class UserRoleService : IUserRole
    {
        Restaurant_ManagementEntities1 ctx = new Restaurant_ManagementEntities1();
        public tblUserRole Get(int Id)
        {
            return ctx.tblUserRoles.Find(Id);
        }

        public List<tblUserRole> GetAll()
        {
            return ctx.tblUserRoles.ToList();
        }

        public bool Save(tblUserRole obj)
        {
            try
            {
                ctx.tblUserRoles.Attach(obj);
                ctx.Entry(obj).State = obj.Id > 0 ? System.Data.Entity.EntityState.Modified : System.Data.Entity.EntityState.Added;

                ctx.SaveChanges();
                return true;

            }
            catch (Exception er)
            {

                throw;
            }
        }
    }
}