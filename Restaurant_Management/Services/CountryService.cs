using Restaurant_Management.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant_Management.Services
{
    public class CountryService : ICountry
    {
        Restaurant_ManagementEntities1 ctx = new Restaurant_ManagementEntities1();
        public tblCountry Get(int Id)
        {
            return ctx.tblCountries.Find(Id);
        }

        public List<tblCountry> GetAll()
        {
            return ctx.tblCountries.ToList();
        }

        public bool Save(tblCountry obj)
        {
            try
            {
                ctx.tblCountries.Attach(obj);
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