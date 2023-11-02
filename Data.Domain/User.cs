using Data.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain
{
    public class User : BaseEntity
    {

        public string  UserName { get; set; }
        public string Password { get; set; }

    }
}
