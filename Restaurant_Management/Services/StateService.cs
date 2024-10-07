using Restaurant_Management.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant_Management.Services
{
    public class StateService : IState
    {
        Restaurant_ManagementEntities1 ctx = new Restaurant_ManagementEntities1();
        public tblState Get(int Id)
        {
            return ctx.tblStates.Find(Id);
        }

        public List<tblState> GetAll()
        {
            return ctx.tblStates.ToList();
        }

        public bool Save(tblState obj)
        {
            try
            {
                ctx.tblStates.Attach(obj);
                ctx.Entry(obj).State = obj.Id > 0 ? System.Data.Entity.EntityState.Modified : System.Data.Entity.EntityState.Added;

                ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}