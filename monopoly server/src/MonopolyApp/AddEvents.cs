using System;

namespace MonopolyApp
{
    static public class AddEvents
    {
        public static void AddConsoleEvents()
        {
            Cases.UserCreatedEvent += ConsoleLogs.ConsoleLogOnEvent;
            Cases.UserCreatedEvent += MessageSender.SendMessage;

            Cases.PaidFromTheBalanceEvent += ConsoleLogs.ConsoleLogOnEvent;
            Cases.PaidFromTheBalanceEvent += MessageSender.SendMessage;

            Cases.PaidToOtherPlayerEvent += ConsoleLogs.ConsoleLogOnEvent;
            Cases.PaidToOtherPlayerEvent += MessageSender.SendMessage;

            Cases.PassedStartEvent += ConsoleLogs.ConsoleLogOnEvent;
            Cases.PassedStartEvent += MessageSender.SendMessage;

            Cases.AddedMoneyEvent += ConsoleLogs.ConsoleLogOnEvent;
            Cases.AddedMoneyEvent += MessageSender.SendMessage;

            Cases.NoMorePlayersAllowedEvent += ConsoleLogs.ConsoleLogOnEvent;
            Cases.NoMorePlayersAllowedEvent += MessageSender.SendMessage;

            Cases.GotAllStatsEvent += ConsoleLogs.ConsoleLogOnEvent;

            Cases.GotUserStatsEvent += ConsoleLogs.ConsoleLogOnEvent;

            Cases.NotRecognisedCommandEvent += ConsoleLogs.ConsoleLogOnEvent;
            Cases.NotRecognisedCommandEvent += MessageSender.SendMessage;

            Cases.ExceptionEvent += ConsoleLogs.ConsoleLogOnEvent;
            Cases.ExceptionEvent += MessageSender.SendErrorMessage;

            Connection.CannotDeserializeDataEvent += ConsoleLogs.ConsoleLogOnEvent;
            Connection.CannotDeserializeDataEvent += MessageSender.SendErrorMessage;
        }
    }
}