using Restaurant_Management.IServices;
using Restaurant_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant_Management.Services
{
    public class BranchService : IBranch
    {
        Restaurant_ManagementEntities1 ctx = new Restaurant_ManagementEntities1();

        public List<Branch> BranchList()
        {
            //List<tblBranch> = ctx.tblBranches.ToList();
            List<Branch> branchList = ctx.tblBranches.Select(s => new Branch
            {
                Id = s.Id,
                Address = s.Address,
                Country = s.tblCountry.Name,
                State = s.tblState.Name,
                City = s.tblCity.Name

            }).ToList();

            return branchList;
        }

        public SelectList City_Dropdown()
        {
            return new SelectList(ctx.tblCities.ToList(), "Id", "Name");
        }

        public SelectList Country_Dropdown()
        {
            return new SelectList(ctx.tblCountries.ToList(), "Id", "Name");
        }

        

        public SelectList State_Dropdown()
        {
            return new SelectList(ctx.tblStates.ToList(), "Id", "Name");
        }
    }
}