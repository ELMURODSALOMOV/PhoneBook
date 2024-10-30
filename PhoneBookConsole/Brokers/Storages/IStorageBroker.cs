using PhoneBookConsole.Models;

namespace PhoneBookConsole.Brokers.Storages
{
    internal interface IStorageBroker
    {
        Contact InsertContact(Contact contact);
        bool DeleteContact(string phone);
        bool UpdateContact(Contact contact);
        Contact[] GetAllContact();
        Contact GetContact(string phone);
    }
}
