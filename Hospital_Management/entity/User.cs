using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management.entity
{
    public class User
    {
        private int userId { get; set; }
        private string username { get; set; }
        private string password { get; set; }
        private string userType { get; set; }

        // Default constructor
        public User() { }

        // Parameterized constructor
        public User(int userId, string username, string password, string userType)
        {
            this.userId = userId;
            this.username = username;
            this.password = password;
            this.userType = userType;
        }

        public override string ToString()
        {
            // Override the ToString method for a custom string representation of the object.
            return $"User ID: {userId}, Username: {username}, UserType: {userType}";
        }
    }
}