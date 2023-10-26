using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Networking_Lab1
{
    public class User
    {
        public User() { }
        public String UserID { get; set; }
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public string Title{ get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }

        public User(String userID, string surName, string firstName, string title, string position, string phone, string email, string location)
        {
            UserID = userID;
            SurName = surName;
            FirstName = firstName;
            Title = title;
            Position = position;
            Phone = phone;
            Email = email;
            Location = location;
        }
    }
}
