using System;
using System.Collections.Generic;

namespace MonopolyApp
{
    class Cases
    {
        static public event WriteLog UserCreatedEvent;
        static public event WriteLog PaidFromTheBalanceEvent;
        static public event WriteLog PaidToOtherPlayerEvent;
        static public event WriteLog PassedStartEvent;
        static public event WriteLog AddedMoneyEvent;
        static public event WriteLog NoMorePlayersAllowed;
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
                            listOfUsers.Add(User.CreateNewUser(listOfUsers, from));
                            if (UserCreatedEvent != null)
                            UserCreatedEvent(new TypeEventArgs(operation));
                        else
                            throw new ArgumentException($"{operation.from} cannot join"); 
                        break;
                    case "dontAllowMorePlayers":
                        if (allowMorePlayers)
                        {
                            if (listOfUsers.Count > 1)
                            {
                            allowMorePlayers = !allowMorePlayers;
                            Console.WriteLine($"Now no one can join to this game. {operation.from} has blocked it. " +
                            "If you want more players to join - restart the system.");
                            
                            if (NoMorePlayersAllowed != null)
                            NoMorePlayersAllowed(new TypeEventArgs(operation));
                            }
                            else
                            throw new Exception("You need at least 2 players to start game");
                        }
                        break;
                    case "payTo":
                        try
                        {
                            user2 = User.GetUser(listOfUsers, to);
                            User.GetUser(listOfUsers, from).PayToOtherPlayer(user2, howMany);
                            Console.WriteLine($"{from} successfully paid {howMany}MLN to {user2.name}.");

                            if (PaidToOtherPlayerEvent != null)
                            PaidToOtherPlayerEvent(new TypeEventArgs(operation));

                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "pay":
                        try
                        {
                            User.GetUser(listOfUsers, from).PayFromTheBalance(howMany);

                            if (PaidFromTheBalanceEvent != null)
                            PaidFromTheBalanceEvent(new TypeEventArgs(operation));

                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "addMoney":
                        user1 = User.GetUser(listOfUsers, to);
                        user1.AddMoney(howMany);

                        if (AddedMoneyEvent != null)
                        AddedMoneyEvent(new TypeEventArgs(operation));

                        break;
                    case "userStats":
                        Console.WriteLine(from);
                        break;
                    case "start":
                        user1 = User.GetUser(listOfUsers, to);
                        user1.PassedStart();
                        Console.WriteLine($"{user1.name} just passed the start and earn 2 MLN. Now {user1.name}'s balance is {user1.balance}MLN.");

                        if (PassedStartEvent != null)
                        PassedStartEvent(new TypeEventArgs(operation));

                        break;
                    case "allStats":
                        foreach (User user in listOfUsers)
                        {
                            Console.WriteLine(user);
                        }
                        break;
                    default:
                        Console.WriteLine("I don't recognize this command");
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}