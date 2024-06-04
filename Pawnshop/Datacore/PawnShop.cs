using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Datacore
{
    public class PawnShop
    {
        public string name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public List<Application> applications = new List<Application>();
    }
}
