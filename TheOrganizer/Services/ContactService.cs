using System.Collections.Generic;
using System.Linq;
using TheOrganizer.Model;

namespace TheOrganizer.Services
{
    public class ContactService : IContactService
    {
        private readonly TheOrganizerDBContext _db;

        public ContactService(TheOrganizerDBContext db)
        {
            _db = db;
        }

        public bool AddContact(Contact contact)
        {
            if (contact != null)
            {
                _db.Contacts.Add(contact);
                _db.SaveChanges();
                return true;
            }
            else return false;
        }

        public bool EditContact(Contact contact)
        {
            Contact OldContact = _db.Contacts.Find(contact.Id);

            if (OldContact != null && OldContact.OwnerId == contact.OwnerId)
            {
                _db.Entry(OldContact).CurrentValues.SetValues(contact);
                _db.SaveChanges();
                return true;
            }
            else return false;
        }

        public IEnumerable<Contact> GetContacts(int ownerId) => _db.Contacts.Where(x => x.OwnerId == ownerId);

        public bool RemoveContact(int contactId, int ownerId)
        {
            var contact = _db.Contacts.Find(contactId);

            if (contact != null && contact.OwnerId == ownerId)
            {
                _db.Contacts.Remove(contact);
                _db.SaveChanges();
                return true;
            }
            else return false;
        }
    }
}
