using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog
{
    namespace ContactCatalog
    {
        public class InvalidEmailException : Exception
        {
            public InvalidEmailException(string email) : base($"Ogiltig e-post: {email}") { }
        }

        public class DuplicateEmailException : Exception
        {
            public DuplicateEmailException(string email) : base($"E-post redan finns: {email}") { }
        }
    }
}
