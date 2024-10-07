using Restaurant_Management.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant_Management.Services
{
    public class CityService : ICity
    {
        Restaurant_ManagementEntities1 ctx = new Restaurant_ManagementEntities1();
        public tblCity Get(int Id)
        {
            return ctx.tblCities.Find(Id);
        }

        public List<tblCity> GetAll()
        {
            return ctx.tblCities.ToList();
        }

        public bool Save(tblCity obj)
        {
            try
            {
                ctx.tblCities.Attach(obj);
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