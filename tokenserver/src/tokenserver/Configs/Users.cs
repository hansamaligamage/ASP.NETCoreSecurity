using IdentityServer4.Services.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tokenserver.Configs
{
    internal class Users
    {

        public static List<InMemoryUser> GetUsers()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Username = "hansamali",
                    Password = "123456"
                }
            };
        }

    }
}
