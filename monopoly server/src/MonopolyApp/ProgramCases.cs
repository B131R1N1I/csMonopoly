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
        static public event WriteMessage GotAllStatsEvent;
        static public event WriteMessage GotUserStatsEvent;
        static public event WriteLog NotRecognisedCommandEvent;
        static public event WriteErrorMessage ExceptionEvent;
        public static void Action(ActionJsonObject operation, ref List<User> listOfUsers, ref bool allowMorePlayers)
        {
            float howMany = (float)Math.Round(operation.howMany, 2);
            string toDo = operation.type;
            string from = operation.from;
            string to = operation.to;
            User user1, user2;
            try
            {
                switch (toDo)
                {
                    // Adding new player
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


                    // no more players allowed 
                    case "dontAllowMorePlayers":
                        if (allowMorePlayers)
                        {
                            // You can turn off joining 2 or more players joined
                            if (listOfUsers.Count >= 2)
                            {
                                allowMorePlayers = false;
                                if (NoMorePlayersAllowedEvent != null)
                                    NoMorePlayersAllowedEvent(new TypeEventArgs(operation));
                            }
                            else
                                throw new Exception("You need at least 2 players to start game");
                        }
                        break;


                    // payment operation (to player)
                    case "payTo":
                        if (allowMorePlayers)
                            throw new Exception("allowMorePlayers is not false.");
                        try
                        {
                            // get two users then transfer money
                            user1 = User.GetUser(listOfUsers, from);
                            user2 = User.GetUser(listOfUsers, to);
                            user1.PayToOtherPlayer(user2, howMany);

                            if (PaidToOtherPlayerEvent != null)
                                PaidToOtherPlayerEvent(new TypeEventArgs(operation));

                        }
                        catch (ArgumentException)
                        {
                            throw;
                        }
                        break;


                    // payment operation
                    case "pay":
                        if (allowMorePlayers)
                            throw new Exception("allowMorePlayers is not false.");
                        try
                        {
                            // get user then remove some money
                            user1 = User.GetUser(listOfUsers, from);
                            user1.PayFromTheBalance(howMany);

                            if (PaidFromTheBalanceEvent != null)
                                PaidFromTheBalanceEvent(new TypeEventArgs(operation));

                        }
                        catch (ArgumentException)
                        {
                            throw;
                        }
                        break;


                    // add money to player
                    case "addMoney":
                        if (allowMorePlayers)
                            throw new Exception("allowMorePlayers is not false.");
                        // add money to player
                        user1 = User.GetUser(listOfUsers, to);
                        user1.AddMoney(howMany);

                        if (AddedMoneyEvent != null)
                            AddedMoneyEvent(new TypeEventArgs(operation));

                        break;


                    // show user's stats
                    case "userStats":

                        if (GotUserStatsEvent != null)
                            GotUserStatsEvent(new User[] { User.GetUser(listOfUsers, from) });
                        break;


                    // add 2mln for start
                    case "start":
                        if (allowMorePlayers)
                            throw new Exception("allowMorePlayers is not false.");
                        // get user then add him some money
                        user1 = User.GetUser(listOfUsers, to);
                        user1.PassedStart();

                        if (PassedStartEvent != null)
                            PassedStartEvent(new TypeEventArgs(operation));

                        break;


                    // show all players' stats
                    case "allStats":

                        if (GotAllStatsEvent != null)
                            GotAllStatsEvent(listOfUsers.ToArray());

                        break;


                    default:
                        if (NotRecognisedCommandEvent != null)
                            NotRecognisedCommandEvent(new TypeEventArgs(operation));

                        break;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionEvent != null)
                    ExceptionEvent(ex);
            throw;
            }
        }
    }
}