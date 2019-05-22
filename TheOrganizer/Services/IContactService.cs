using System.Collections.Generic;
using TheOrganizer.Model;

namespace TheOrganizer.Services
{
    public interface IContactService
    {
        bool AddContact(Contact contact);

        bool EditContact(Contact contact);

        bool RemoveContact(int contactId, int ownerId);

        IEnumerable<Contact> GetContacts(int ownerId);
    }
}
