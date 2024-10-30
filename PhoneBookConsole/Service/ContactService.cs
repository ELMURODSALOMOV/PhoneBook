using PhoneBookConsole.Brokers.LoggingBroker;
using PhoneBookConsole.Brokers.Loggings;
using PhoneBookConsole.Brokers.Storages;
using PhoneBookConsole.Models;

namespace PhoneBookConsole.Service
{
    internal class ContactService : IContactService
    {

        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public ContactService()
        {
            this.storageBroker = new StorageBroker();
            this.loggingBroker = new LoggingBroker();
        }

        public Contact AddContact(Contact contact)
        {
            return contact is null
                ? CreateAndLogInvalidContact()
                : ValidateAndAddContact(contact);
        }

        public void ReadAllContacts()
        {
            Contact[] contacts = this.storageBroker.GetAllContact();

            foreach (Contact contact in contacts)
            {
                this.loggingBroker.LogInformation($"{contact.Id}. {contact.Name} - {contact.Phone}");
            }

            this.loggingBroker.LogInformation($"=== End of contacts ===");
        }

        public Contact ReadContact(string phone)
        {
            return phone is null
                ? ReadAndLogInvalidPhone()
                : ValidateAndReadContact(phone);
        }


        public bool RemoveContact(string phone)
        {
            return phone is null
                ? RemoveAndLogInvalidPhone()
                : ValidateAndRemoveContact(phone);
        }


        public bool ResetContact(Contact contact)
        {
            return contact is null
                ? ResetAndLogInvalidContact()
                : ValidateAndResetContact(contact);
        }


        private Contact ValidateAndAddContact(Contact contact)
        {
            if (contact.Id is 0
                || String.IsNullOrWhiteSpace(contact.Name)
                || String.IsNullOrWhiteSpace(contact.Phone))
            {
                this.loggingBroker.LogError("Contact details missing.");
                return new Contact();
            }
            else
            {
                return this.storageBroker.InsertContact(contact);
            }
        }

        private Contact CreateAndLogInvalidContact()
        {
            this.loggingBroker.LogError("Contact is invalid.");
            return new Contact();
        }
        private bool ValidateAndRemoveContact(string phone)
        {
            if (String.IsNullOrWhiteSpace(phone))
            {
                this.loggingBroker.LogError("Phone details missing.");
                return false;
            }
            else
            {
                this.loggingBroker.LogInformation("Contect deleted.");
                return this.storageBroker.DeleteContact(phone);
            }
        }

        private bool RemoveAndLogInvalidPhone()
        {
            this.loggingBroker.LogError("Contact is invalid.");
            return false;
        }
        private Contact ValidateAndReadContact(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone) is true)
            {
                this.loggingBroker.LogError("No phone details.");
                return new Contact();
            }
            else
            {
                Contact contact = this.storageBroker.GetContact(phone);
                this.loggingBroker.LogInformation($"{contact.Id}. {contact.Name} - {contact.Phone}");
                return contact;
            }
        }

        private Contact ReadAndLogInvalidPhone()
        {
            this.loggingBroker.LogError("Invalid phone.");
            return new Contact();
        }

        private bool ValidateAndResetContact(Contact contact)
        {
            if (contact.Id is 0
               || String.IsNullOrWhiteSpace(contact.Name)
               || String.IsNullOrWhiteSpace(contact.Phone))
            {
                this.loggingBroker.LogError("Contact details missing.");
                return false;
            }
            else
            {
                this.loggingBroker.LogInformation("Contact updated.");
                return this.storageBroker.UpdateContact(contact);
            }
        }

        private bool ResetAndLogInvalidContact()
        {
            this.loggingBroker.LogError("Contact is invalid.");
            return false;
        }
    }
}
