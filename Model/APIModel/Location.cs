using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.APIModel
{
    public class Location
    {
        public decimal? lng { get; set; }

        public decimal? lat { get; set; }

        public Location(decimal? lng, decimal? lat)
        {
            this.lng = lng;
            this.lat = lat;
        }

        public Location() { }
    }
}
