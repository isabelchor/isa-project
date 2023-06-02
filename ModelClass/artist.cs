using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClass
{
    public class artist
    {

        public int artistID { get; set; }
        public string Name { get; set; }
        public string Birthday { get; set; }
        public string Password { get; set; }
        public artist(int artistID, string name, string birthday, string password)
        {
            this.artistID = artistID;
            Name = name;
            this.Birthday = birthday;
            this.Password= password;
        }
        public artist() { }
    }
}
