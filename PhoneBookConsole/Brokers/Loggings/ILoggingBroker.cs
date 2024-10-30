namespace PhoneBookConsole.Brokers.LoggingBroker
{
    internal interface ILoggingBroker
    {
        void LogInformation(string message);
        void LogError(string userMessage);
        void LogError(Exception exception);
    }
}
