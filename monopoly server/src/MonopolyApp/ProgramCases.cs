using System;
using System.Collections.Generic;

namespace MonopolyApp
{
    class Cases
    {
        // Events on operations
        static public event WriteLog UserCreatedEvent;
        static public event WriteLog PaidFromTheBalanceEvent;
        static public event WriteLog PaidToOtherPlayerEvent;
        static public event WriteLog PassedStartEvent;
        static public event WriteLog AddedMoneyEvent;
        static public event WriteLog NoMorePlayersAllowedEvent;
        static public event WriteLog GotAllStatsEvent;
        static public event WriteLog GotUserStatsEvent;
        static public event WriteLog NotRecognisedCommandEvent;
        public static void Action(ActionJsonObject operation, ref List<User> listOfUsers, ref bool allowMorePlayers)
        {
            double howMany = Math.Round(operation.howMany, 2);
            string toDo = operation.type;
            string from = operation.from;
            string to = operation.to;
            User user1, user2;
            try
            {
                switch (toDo)
                {
                    case "newPlayer":
                        if (allowMorePlayers)
                        {
                            if (from.Length < 3)
                            throw new ArgumentException($"Username \"{from}\" is too short.");                            

                            listOfUsers.Add(User.CreateNewUser(listOfUsers, from));
                            if (UserCreatedEvent != null)
                                UserCreatedEvent(new TypeEventArgs(operation));
                        }
                        else
                            throw new ArgumentException($"{from} cannot join");
                        break;
                    case "dontAllowMorePlayers":
                        if (allowMorePlayers)
                        {
                            // You can turn off joining 2 or more players joined
                            if (listOfUsers.Count > 1)
                            {
                                allowMorePlayers = !allowMorePlayers;
                                if (NoMorePlayersAllowedEvent != null)
                                    NoMorePlayersAllowedEvent(new TypeEventArgs(operation));
                            }
                            else
                                throw new Exception("You need at least 2 players to start game");
                        }
                        break;
                    case "payTo":
                        try
                        {
                            // get two users then transfer money
                            user1 = User.GetUser(listOfUsers, from);
                            user2 = User.GetUser(listOfUsers, to);
                            user1.PayToOtherPlayer(user2, howMany);

                            if (PaidToOtherPlayerEvent != null)
                                PaidToOtherPlayerEvent(new TypeEventArgs(operation));

                        }
                        catch (ArgumentException ex)
                        {
                            throw ex;
                        }
                        break;

                    case "pay":
                        try
                        {
                            // get user then remove some money
                            user1 = User.GetUser(listOfUsers, from);
                            user1.PayFromTheBalance(howMany);

                            if (PaidFromTheBalanceEvent != null)
                                PaidFromTheBalanceEvent(new TypeEventArgs(operation));

                        }
                        catch (ArgumentException ex)
                        {
                            throw ex;
                        }
                        break;
                    case "addMoney":
                        // add money to player
                        user1 = User.GetUser(listOfUsers, to);
                        user1.AddMoney(howMany);

                        if (AddedMoneyEvent != null)
                            AddedMoneyEvent(new TypeEventArgs(operation));

                        break;
                    case "userStats":
                        // raise GotUserStatsEvent if not null
                        if (GotUserStatsEvent != null)
                        GotUserStatsEvent(new TypeEventArgs(operation));

                        break;
                    case "start":
                        // get user then add him some money
                        user1 = User.GetUser(listOfUsers, to);
                        user1.PassedStart();
            
                        if (PassedStartEvent != null)
                            PassedStartEvent(new TypeEventArgs(operation));

                        break;
                    case "allStats":
                        // TEMPORARY!
                        foreach (User user in listOfUsers)
                        {
                            Console.WriteLine(user);
                        }

                        if (GotAllStatsEvent != null)
                        GotAllStatsEvent(new TypeEventArgs(operation));

                        break;
                    default:
                        if (NotRecognisedCommandEvent != null)
                        NotRecognisedCommandEvent(new TypeEventArgs(operation));

                        break;
                }
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
        }
    }
}