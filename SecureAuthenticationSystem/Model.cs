using SecureAuthenticationSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureAuthenticationSystem
{
    public class Model : User
    {
        List<User> users = new List<User>();

        public void Store(string email, string password)
        {
            foreach (var user in users)
            {
                if (user.email == null)
                {
                    user.email = email;
                    user.password = password;

                }
            }
        }

        public bool Destroy(int id)
        {

            id -= 1;
            var i = 0;

            if (id >= users.Count())
            {
                return false;
            }
            else
            {
                foreach (var user in users)
                {
                    if (i == (users.Count() - 1))
                    {
                        users = null;
                    }
                    else
                    {
                        users = users;
                    }
                }
                return true;
            }
        }

    }
}