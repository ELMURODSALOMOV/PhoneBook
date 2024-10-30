using PhoneBookConsole.Models;

namespace PhoneBookConsole.Brokers.Storages
{
    internal class StorageBroker : IStorageBroker
    {
        private readonly string filePath = "../../../Assets/ContactFileDB.txt";
        private bool isUpdateOrDelete;

        public StorageBroker()
        {
            isUpdateOrDelete = false;
            EnsurFileExists();
        }
        public bool DeleteContact(string phone)
        {
            string[] contacLines = File.ReadAllLines(filePath);

            for(int itaration = 0; itaration < contacLines.Length; itaration++)
            {
                string contacLine = contacLines[itaration];
                string[] contacProperties = contacLine.Split('*');

                if (contacProperties[2].Contains(phone))
                {
                    isUpdateOrDelete = true;
                    contacLines[itaration] = null;
                    break;
                }
            }

            if(IsUpdateOrDelete() is true)
            {
                for(int itaration = 0; itaration < contacLines.Length; itaration++)
                {
                    if (contacLines[itaration] is not null)
                    {
                        File.AppendAllText(filePath, $"{contacLines[itaration]}\n");
                    }
                }
                return true;
            }
            return false;
        }

        public Contact[] GetAllContact()
        {
            string[] contactLines = File.ReadAllLines(filePath);

            Contact[] contacts = new Contact[contactLines.Length];
            for (int itaration = 0; itaration < contactLines.Length; itaration++)
            {
                string contactLine = contactLines[itaration];
                string[] contactProperties = contactLine.Split('*');

                Contact contact = new Contact()
                {
                    Id = Convert.ToInt32(contactProperties[0]),
                    Name = contactProperties[1],
                    Phone = contactProperties[2]
                };

                contacts[itaration] = contact;
            }

            return contacts;
        }

        public Contact GetContact(string phone)
        {
            Contact contact = new Contact();
            string[] contactLines = File.ReadAllLines(filePath);

            for(int itaration = 0; itaration < contactLines.Length;itaration++)
            {
                string contactLine = contactLines[itaration];
                string[] contactProperties = contactLine.Split('*');

                if (contactProperties[2].Contains(phone) is true)
                {
                    contact.Id = Convert.ToInt32(contactProperties[0]);
                    contact.Name = contactProperties[1];
                    contact.Phone = contactProperties[2];
                    break;
                }
            }
            return contact;
        }

        public Contact InsertContact(Contact contact)
        {
            string contactLine = $"{contact.Id}*{contact.Name}*{contact.Phone}";
            File.AppendAllText(filePath, contactLine );

            return contact;
        }

        public bool UpdateContact(Contact contact)
        {
            string[] contactLines = File.ReadAllLines(filePath);

            for(int itaration = 0; itaration < contactLines.Length; itaration++)
            {
                string contactLine = contactLines[itaration];
                string[] contactProperties = contactLine.Split("*");

                if (contactProperties[0].Contains(contact.Id.ToString()) is true)
                {
                    contactProperties[1] = contact.Name;
                    contactProperties[2] = contact.Phone;
                    isUpdateOrDelete = true;
                    break;
                }
            }

            if( IsUpdateOrDelete() is true)
            {
                for(int itaration = 0; itaration < contactLines.Length; itaration ++)
                {
                    if (contactLines[itaration] is not null)
                    {
                        File.AppendAllText(filePath, $"{contactLines[itaration]}\n");
                    }
                }
                return true;
            }
            return false;
        }

        private bool IsUpdateOrDelete()
        {
            if(isUpdateOrDelete is true)
            {
                File.Delete(filePath);
                File.Create(filePath).Close();
                return true;
            }
            return false;   
        }

        private void EnsurFileExists()
        {
            bool isFileThere = File.Exists(filePath);

            if (isFileThere is false)
            {
                File.Create(filePath).Close();
            }
        }
    }
}
