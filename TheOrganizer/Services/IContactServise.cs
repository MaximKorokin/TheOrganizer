using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheOrganizer.Model;

namespace TheOrganizer.Services
{
    public interface IContactServise
    {
        bool AddContact(Contact contact);

        bool EditContact(Contact contact);

        bool RemoveContact(int comtactId, int ownerId);

        IEnumerable<Contact> GetContacts(int ownerId);
    }
}
