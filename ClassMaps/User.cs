using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Emulator.ClassMaps
{
    public class User
    {
        private int userId;
        private string username;
        private string password;
        private string email;

        public User() {}

        public virtual int UserId 
        {
            get { return userId;  }
            set { userId = value; }
        }

        public virtual string Username 
        {
            get { return username;  }
            set { username = value; }
        }

        public virtual string Password 
        {
            get { return password;  }
            set { password = value; }
        }

        public virtual string Email 
        {
            get { return email;  }
            set { email = value; }
        }
    }
}
