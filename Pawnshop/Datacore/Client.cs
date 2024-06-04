using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Datacore
{
    public   class Client
    {
        public string name { get; set; }
        public string address { get; set; }
        public string contactInfo { get; set; }

        public Client(string name, string address, string contactInfo)
        {
            this.name = name;
            this.address = address;
            this.contactInfo = contactInfo;
        }

        public override string ToString()
        {
            return $"" +
                $"*{name}*{address}*{contactInfo}";
        }   
    }
}
