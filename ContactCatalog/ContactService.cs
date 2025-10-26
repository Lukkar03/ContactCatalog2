using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using Microsoft.Extensions.Logging;

namespace ContactCatalog
{
    public class ContactService
    {
        private readonly Dictionary<int, Contact> contacts = new();
        private readonly HashSet<string> emails = new(StringComparer.OrdinalIgnoreCase);
        private readonly ILogger logger;

        public ContactService(ILogger logger)
        {
            this.logger = logger;
        }

        public void AddContact(Contact c)
        {
            if (!IsValidEmail(c.Email))
            {
                logger.LogWarning("Ogiltig email: {Email}", c.Email);
                throw new Exception($"Ogiltig e-post: {c.Email}");
            }

            if (!emails.Add(c.Email))
            {
                logger.LogWarning("Dubblett-email: {Email}", c.Email);
                throw new Exception($"E-post redan finns: {c.Email}");
            }

            contacts.Add(c.Id, c);
            logger.LogInformation("Kontakt tillagd: {Name}", c.Name);
        }

        public IEnumerable<Contact> GetAll() => contacts.Values;

        public IEnumerable<Contact> SearchByName(string term) =>
            contacts.Values
                .Where(c => c.Name.Contains(term, StringComparison.OrdinalIgnoreCase))
                .OrderBy(c => c.Name);

        public IEnumerable<Contact> FilterByTag(string tag) =>
            contacts.Values
                .Where(c => c.Tags.Contains(tag, StringComparer.OrdinalIgnoreCase))
                .OrderBy(c => c.Name);

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch { return false; }
        }
    }
}