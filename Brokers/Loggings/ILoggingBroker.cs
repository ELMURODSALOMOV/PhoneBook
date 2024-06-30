//----------------------------------------
// Great Code Team (c) All rights reserved
//----------------------------------------

namespace PhoneBook.Brokers.Loggings
{
    internal interface ILoggingBroker
    {
        void LogInformation(string message);
        void LogError(string userMessage);
        void LogError(Exception exception);
    }
}
