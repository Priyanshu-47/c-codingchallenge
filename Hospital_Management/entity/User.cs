using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management.entity
{
    public class User
    {
        private int user_id { get; set; }
        private string username { get; set; }
        private string password { get; set; }
        private string user_type { get; set; }

        // Default constructor
        public User() { }

        // Parameterized constructor
        public User(int user_id, string username, string password, string user_type)
        {
            this.user_id = user_id;
            this.username = username;
            this.password = password;
            this.user_type = user_type;
        }

        public override string ToString()
        {
            // Override the ToString method for a custom string representation of the object.
            return $"User ID: {user_id}, Username: {username}, UserType: {user_type}";
        }
    }
}