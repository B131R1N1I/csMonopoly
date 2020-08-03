using System;

namespace MonopolyApp
{
    static public class AddEvents
    {
        public static void AddConsoleEvents()
        {
           Cases.UserCreatedEvent += ConsoleLogs.ConsoleLogOnEvent;

           Cases.PaidFromTheBalanceEvent += ConsoleLogs.ConsoleLogOnEvent;

           Cases.PaidToOtherPlayerEvent += ConsoleLogs.ConsoleLogOnEvent;

           Cases.PassedStartEvent += ConsoleLogs.ConsoleLogOnEvent;

           Cases.AddedMoneyEvent += ConsoleLogs.ConsoleLogOnEvent;

           Cases.NoMorePlayersAllowedEvent += ConsoleLogs.ConsoleLogOnEvent;

           Cases.GotAllStatsEvent += ConsoleLogs.ConsoleLogOnEvent;

           Cases.GotUserStatsEvent += ConsoleLogs.ConsoleLogOnEvent;

           Cases.NotRecognisedCommandEvent += ConsoleLogs.ConsoleLogOnEvent;

           Cases.ExceptionEvent += ConsoleLogs.ConsoleLogOnEvent; 
           Cases.ExceptionEvent += MessageSender.SendErrorMessage;

           Connection.CannotDeserializeDataEvent += MessageSender.SendErrorMessage; 
        }
    }
}