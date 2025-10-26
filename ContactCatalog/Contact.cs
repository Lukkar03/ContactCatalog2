using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog
{
    public class Contact
    {
        public int Id {  get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public List<string> Tags { get; set; } = new ();
    }
}
