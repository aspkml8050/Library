using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibData.Contract
{
    //Pass any one to get result or empty both to get all
    public class UserType
    {
        public int? UserTypeId { get; set; }
        public string UserTypeName { get; set; }

    }
}
