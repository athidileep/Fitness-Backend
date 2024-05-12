using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.User
{
    public class UserFilterModel
    {
        public int SASSId { get; set; }
        public int UserType { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    } 
}
