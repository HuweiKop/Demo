﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.APIModel
{
    public class CityApi 
    {
        public string city_code { get; set; }

        public string city_name { get; set; }

        public Location location { get; set; }
    }
}