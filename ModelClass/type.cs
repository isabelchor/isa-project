using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClass
{
    public class type
    {

        public int ID { get; set; }
        public string types { get; set; }
        public type(int iD, string types)
        {
            ID = iD;
            this.types = types;
        }
        public type() { }  
        
    }
}
