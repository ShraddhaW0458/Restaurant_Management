using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant_Management.Models
{
    public class Branch
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public Nullable<int> StateId { get; set; }
        public Nullable<int> CityId { get; set; }
        public Nullable<int> CountryId { get; set; }

        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }

    }
}