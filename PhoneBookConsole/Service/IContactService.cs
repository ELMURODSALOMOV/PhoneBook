using PhoneBookConsole.Models;

namespace PhoneBookConsole.Service
{
    internal interface IContactService
    {
        Contact AddContact(Contact contact);
        void ReadAllContacts();
        bool RemoveContact(string phone);
        bool ResetContact(Contact contact);
        Contact ReadContact(string phone);
    }
}
