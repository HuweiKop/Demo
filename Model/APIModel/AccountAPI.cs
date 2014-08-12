using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.APIModel
{
    public class LoginResponse : ReturnBaseModel
    {
        public LoginResult user_info { get; set; } 
    }

    public class LoginResult
    {
        public string user_code { get; set; }

        public string user_name { get; set; }
    }
}
