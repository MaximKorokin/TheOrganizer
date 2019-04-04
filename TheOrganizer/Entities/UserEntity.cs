using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheOrganizer.Model;

namespace TheOrganizer.Entities
{
    public class UserEntity : User
    {
        public string Token { get; set; }
    }
}
