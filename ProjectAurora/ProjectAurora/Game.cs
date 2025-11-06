using System;
using System.Drawing;
using System.Threading; 
//^^ these are for the hydro minigame

namespace ProjectAurora
{
    public class Game
    {
        private static Room? currentRoom;
        private static Room? previousRoom;
        private static Room? start;

        private bool hasDamKey = false;
        private bool hasNewLever = false;
        private bool leverRepaired = false;
        private bool leverDerusted = false;
        private bool hasBerries = false;
        private bool hasPinecone = false;
        private int successfulPresses = 0;
        private const int QuickTimeEvents = 5;
        private bool qteComplete = false;
        private bool hasRoboticParts1 = false;
        private bool hasRoboticParts2 = false;
        private bool hasWaterHose = false;




        private bool talkedToLiora = false;
        private bool hasDesertKey = false;
        private static Room? MaintenanceTent;
        private static Room? desertHub;
        private static Room? junkyard;
        private static Room? solarPanelFields;
        private static Room? MaintenanceTentOutside;
        private static Room? scraps1;
        private static Room? scraps2;
        private static Room? waterSupplies;

        private Room? damplant;
        private Room? controlroom;
        public Game()
        {
            CreateRooms();
            CreateNPCs();
        }

        public void CreateRooms()
        {

            start = new Room("Aurora Control Hub", "You stand inside the Aurora Control Hub, the heart of the last renewable energy initiative." +
                "\r\nThe air hums with faint backup power. Screens flicker, showing maps of four darkened regions." +
                "\r\nA workbench lies in the corner with scattered tools.\r\n");
            currentRoom = start;

            // solar rooms and their items
            Room? solarDesert = new Room("Solar Desert", "After walking for hours you find yourself in a desolate land.\r\n" +
                "The desert stretches before you. Towers of sand cover the solar field. Heat shimmers across the horizon.\r\n" +
                "You find a small hub that looks like it could have life(west)");
            desertHub = new Room("Desert Hub", "You notice a map in front of the hub with the areas in the desert:\r\n" +
                "Maintenance tent(west), Aurora Hub(east), Solar panel field(north), Junkyard(south)\r\n" +
                "You decide to go inside and there you find Dr. Liora Sunvale\r\n" +
                "She welcomes you inside and is ready to answer your questions. (talk)\r\n");

            MaintenanceTentOutside = new Room("Maintenece tent outside", "Before going in, the scientist guarding the tent ask you a question: \r\n" +
                "'What happens if solar panels overheat?'\r\n" +
                "Your options are: (1) More energy, (2) Less efficiency, (3) Catch fire\r\n");
            Console.Write("> ");

            MaintenanceTent = new Room("Maintenance tent", "You go inside the tent and there greets you a wooden box with the text\r\n" +
                "Junkyard\r\n" +
                "You chose to open to the box and inside it is a key. You will need it for your progress in the Solar Desert, so you take the key.\r\n");

            desertHub.AddNPC("Dr. Liora Sunvale", "Welcome young scientist!\r\n" +
                "Our mission is to save the 'Solar panel field' and find all burried solar panels!\r\n" +
                "We have thought of 2 methods of doing it and you're more than welcome to chose which one you preffer.\r\n" +
                "1 of them is a temporary fix, so choose wisely!\r\n" +
                "Visit the maintenece tent to get more information.(west)\r\n");

            solarPanelFields = new Room("Solar Panel Fields","You find yourself in the Solar Panel Fields and notice a lot of piles of sand.\r\n" +
                "You try to dig into one and you find a solar panel. There are thousands of them.\r\n" +
                "How will you clean up the piles:\r\n" +
                "(1) Water Hose (unreliable) (2) Robotic maintenece\r\n");
            Console.Write("> ");

            junkyard = new Room("Junkyard","You use the key to go inside the junkyard and there you find 3 exits labeled:" +
                "Water supplies(west) Scraps 1(south) Scraps 2(east)\r\n");

            scraps1 = new Room("Scrapyard 1", "After going inside you see a huge pile of scraps.\r\n" +
                "You start searching for materials and inside you find robotic parts and you take them.\r\n" +
                "(+Robotic parts)");

            scraps2 = new Room("Scrapyard 2", "After going inside you see a huge pile of scraps.\r\n" +
                "You start searching for materials and inside you find robotic parts and you take them.\r\n" +
                "(+Robotic parts)");

            waterSupplies = new Room("Water Supplies", "After going inside you see a huge pile of water supplies.\r\n" +
                "You start searching for materials and inside you long water hose with a portable water tank and you take them.\r\n" +
                "(+Water Hose)");


            solarDesert.SetExit("west", desertHub);
            desertHub.SetExit("east", start);


            // hydro rooms and their items
            Room? hydroHub = new("Hydro Hub", "You started walking towards the river. But you reach an area where the road separates into 4. " +
                "\r\nLuckily there is a sign that reads:\r\n" +
                "==Welcome, you are at the Hydro Hub==\r\n" +
                "==Outside(south), Research Center(north), Hydroelectric Dam(east), Tundra forrest(west)==\r\n");

            damplant = new("The Dam Plant", "After a short stroll you arrive at the riverside with no bridge leading acoross. \r\n" +
                "However there is a an obstruction that connects the two sides: A huge Hydroelectric Plant. And you see an enterance, that you can enter(inside) \r\n" +
                "but there is a lock on the door\r\n");

            Room? researchcenter = new("Research Center", "You enter inside the lobby of an Aurora outpost, this building gives help" +
                "and guidiance to engineers on their missions, \r\n" +
                "it has 2 main sections: a Library(up) and the Cafeteria(right). The way back to the Hydro Hub is (South)\r\n");

            Room? hydroResourcearea = new("Tundra Forrest", "You have entered the Tundra forrest. There are giant trees as far as you can see.\r\n" +
                "You see some items you that you can move, a few pinecones on the ground(take pinecone) and some berries on a nearby bush(take berries), you can try to wander around but it might get you lost\r\n");
            Item berries = new("berries", "A cluster of edible-looking berries. Maybe they could be useful.\r\n");
            hydroResourcearea.AddItem(berries);
            Item pinecone = new("pinecone", "A large,  pinecone.\r\n");
            hydroResourcearea.AddItem(pinecone);

            Room? library = new("Library", "Loads of heavy shelves hold thousands of technical theory, documents and old logbooks.\r\n" +
                "A single chair is occupied by a person reading one of the books, you can approach ther (talk). \r\n" +
                "The lobby is downstrairs(down)\r\n");
            library.AddNPC("Dr. Amara Riversong", "Oh, hello. Looking for information on the Dam? It's truly bad luck, due to climate change the weather became even more\r\n" +
                            "extreme up here North as a result the dam's pipes have frozen and the lever that could reboot the pipes\r\n" +
                            "has became rusty and stuck shut. To add to the troubles, apparently the dam's key was misplaced by\r\n" +
                            "a previous Aurora member, now we can't enter the Control Room even if we had the acid to derust the lever\r\n" +
                            "but I have some good news aswell, I have worked out an acid that could derusting the lever \r\n" +
                            "and restart the energy production of the dam, it requires berry juice and pinecone dust\r\n" +
                            "You should find both around the Tundra forrest \r\n" +
                            "This is all the information you should need to save the Dam");


            Room? cafeteria = new("Cafeteria", "A regular cafeteria. Near the serving station, you see a small, out of place item lying among the cutlery. It looks like a key, dou you take it?(take key) It might come in handy later... \r\n" +
                "The lobby is to the Left(left).\r\n");

            Item damKey = new("key", "A small metal tool with the Aurora symbol on it. It doesn't quite fit the shape of the rest of the spoons and forks, maybe you should investigate? (take key) \r\n");
            cafeteria.AddItem(damKey);

            Room? bonus = new("Top of the Hill", "After climbing up the hill you find a forgotten toolbox. You see a box labeled levers, take one?(take lever).\r\n" +
            "The only way down is back to the Tundra (south).\r\n");
            Item lever = new("lever", "A heavy, stainless steel lever. It looks like it could replace a rusted, jammed control.\r\n");
            bonus.AddItem(lever);

            controlroom = new("Control room", "You walk deep inside the dam to the Control room. Directly ahead is the emergency restart control panel with the restart lever marked, \r\n" +
            "however the levers are completely rusted and jammed shut. You need to derust or replace if you ever want to resteart teh plant. The only way to go is leaving and going back out to the DampPlant(outside).\r\n");


            // hydro area directions
            hydroHub.SetExit("south", start);
            hydroHub.SetExit("east", damplant);
            hydroHub.SetExit("north", researchcenter);
            hydroHub.SetExit("west", hydroResourcearea);
            damplant.SetExit("west", hydroHub);
            damplant.SetExit("inside", controlroom);
            controlroom.SetExit("outside", damplant);
            researchcenter.SetExit("south", hydroHub);
            researchcenter.SetExit("up", library);
            researchcenter.SetExit("right", cafeteria);
            library.SetExit("down", researchcenter);
            cafeteria.SetExit("left", researchcenter);
            hydroResourcearea.SetExit("east", hydroHub);
            hydroResourcearea.SetExit("north", bonus);
            bonus.SetExit("south", hydroResourcearea);
            start.SetExit("north", hydroHub);
            start.SetExit("west", solarDesert);


            //windy highlands
            Room? outside = new("You are outside.", "You are standing outside on the peak of the windy highlands. To the south is ridge path leading to an abandoned cabin.");
            Room? cabin = new("You are inside the cabin.", "You've entered an old, abandoned cabin once used by maintenance crews. You can see old notes and spare parts scattered on the ground. To the east is a door which seems to lead to the garden.");
            Room? garden = new("You are in the garden.", "You're standing in the garden, now overgrown with weeds and bushes. You can feel the cold wind on your face. To the north is an old, half-broken shed. To the south you can see the turbines turning faintly in the distance.");
            Room? shed = new("You are in the shed.", "You are standing in the old shed. To the north is a desk with some papers sticking out.");
            Room? turbines = new("You are at the turbines.", "You are standing outside, between the wind turbines. Some of them seem to be turned off, while others are spinning slowly. To the east is a control tower seemingly connected to the turbines. From the west you can hear a stream of water behind some trees.");
            Room? tower = new("You are inside the tower.", "You've entered the control tower. You can hear a faint static sound in the background. To the north are some old computers faintly flickering. To the east is an office.");
            Room? office = new("You are inside the office.", "You've entered what seems to be an administration office. You can see blueprints and written entries scattered across the floor. You can hear rustling from behind a bookshelf towards the south.");
            Room? stream = new("You found a stream of water.", "You are standing next to a stream of water. To the north you can see an abandoned bonfire with a few tents nearby.");

            outside.SetExit("south", cabin);

            cabin.SetExits(outside, garden, null, null);

            garden.SetExits(shed, null, turbines, cabin);

            shed.SetExit("south", garden);

            turbines.SetExits(garden, tower, null, stream);

            tower.SetExits(null, office, null, turbines);

            office.SetExit("west", tower);

            stream.SetExit("east", turbines);

            //currentRoom = outside;

        }

        private void CreateNPCs()
        {
        }

        public void Play()
        {
            Parser parser = new();

            PrintWelcome();

            bool continuePlaying = true;
            while (continuePlaying)
            {
                Console.WriteLine(currentRoom?.ShortDescription);
                Console.Write("> ");

                string? input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Please enter a command.");
                    continue;
                }

                Command? command = parser.GetCommand(input);

                if (command == null)
                {
                    Console.WriteLine(currentRoom?.LongDescription);
                    Console.WriteLine("I don't know that command.");
                    continue;
                }

                switch (command.Name)
                {
                    case "look":
                        Console.WriteLine(currentRoom?.LongDescription);
                        break;

                    case "back":
                        if (previousRoom == null)
                            Console.WriteLine("You can't go back from here!");
                        else
                            currentRoom = previousRoom;
                        break;


                    case "take":
                        TakeItem(command);
                        break;

                    case "use":
                        UseItem(command);
                        break;

                    case "north":
                    case "south":
                    case "east":
                    case "west":
                    case "right":
                    case "up":
                    case "down":
                    case "left":
                    case "outside":
                        Move(command.Name);
                        break;

                    case "inside":
                        TryMoveInside();
                        break;

                    case "talk":
                        currentRoom?.PrintNPCName();
                        currentRoom?.PrintDialog();
                        if (currentRoom?.ShortDescription == "Desert Hub")
                        {
                            talkedToLiora = true;
                        }
                        break;

                    case "quit":
                        continuePlaying = false;
                        break;

                    case "help":
                        PrintHelp();
                        break;



                    default:
                        Console.WriteLine("I don't know what command.");
                        break;
                }
            }

            Console.WriteLine("Thank you for playing Project Aurora!");
        }

        private void Move(string direction)
        {
            if (currentRoom == desertHub && talkedToLiora)
            {
                desertHub?.SetExit("west", MaintenanceTentOutside);
                desertHub?.SetExit("north", solarPanelFields);
                MaintenanceTent?.SetExit("east", desertHub);
                junkyard?.SetExit("west", waterSupplies);
                junkyard?.SetExit("south", scraps1);
                junkyard?.SetExit("east", scraps2);
                junkyard?.SetExit("north", desertHub);
                solarPanelFields?.SetExit("south", desertHub);
                if (hasDesertKey)
                {
                    desertHub?.SetExit("south", junkyard);
                }
            }

            if (currentRoom?.Exits.ContainsKey(direction) == true)
            {
                previousRoom = currentRoom;
                currentRoom = currentRoom?.Exits[direction];
                Console.WriteLine(currentRoom?.LongDescription);


                

                if (currentRoom?.ShortDescription == "Maintenece tent outside")
                {
                    string? input = Console.ReadLine();

                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine("Please enter an answer.");
                        return;
                    }

                    switch (input)
                    {
                        case "1":
                        case "3":
                            Console.WriteLine("Wrong answer! Try again.");
                            previousRoom = currentRoom;
                            currentRoom = desertHub;
                            break;

                        case "2":
                            Console.WriteLine("Good job! You got it right.");
                            previousRoom = desertHub;
                            currentRoom = MaintenanceTent;
                            Console.WriteLine(currentRoom?.LongDescription);
                            hasDesertKey = true;
                            break;
                        default:
                            break;
                    }
                }

                if (currentRoom?.ShortDescription == "Scrapyard 1") 
                {
                    hasRoboticParts1 = true;
                }
                if (currentRoom?.ShortDescription == "Scrapyard 2")
                {
                    hasRoboticParts2 = true;
                }
                if (currentRoom?.ShortDescription == "Water Supplies")
                {
                    hasWaterHose = true;
                }

                if (currentRoom?.ShortDescription == "Solar Panel Fields")
                {
                    string? input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine("Please select an item.");
                        return;
                    }
                    switch (input)
                    {
                        case "1":
                            if(hasWaterHose)
                            {
                                Console.WriteLine("You manage to clean the solar panels and get them working again, but the fix is only temporary.");
                                currentRoom = start;
                                previousRoom = null;
                                return;
                            }
                            else
                            {
                                Console.WriteLine("You do not have that item!");
                                return;
                            }
                        case "2":
                            if(hasRoboticParts1 && hasRoboticParts2)
                            {
                                Console.WriteLine("You manage to use the robotic parts you've found to make a robot that will maintain the functionality of the panels!\r\n" +
                                    "You have saved the Solar Desert!");
                                currentRoom = start;
                                previousRoom = null;
                            }
                            break;

                    }
                }
            }
            else
            {
                Console.WriteLine($"You can't go {direction}!");
            }
        }


        //takeitem command
        private void TakeItem(Command command)
        {
            if (!command.HasSecondWord())
            {
                Console.WriteLine("Take what?");
                return;
            }

            string itemName = command.SecondWord;
            Item? itemToTake = currentRoom?.GetItem(itemName);

            if (itemToTake == null)
            {
                Console.WriteLine($"There is no {itemName} here to take.");
                return;
            }

            string itemNameLower = itemToTake.Name.ToLower();

            if (itemNameLower == "key")
            {
                hasDamKey = true;
                currentRoom?.RemoveItem(itemToTake.Name);
                Console.WriteLine($"You take the {itemToTake.Name}.");
            }

            else if (itemNameLower == "lever")
            {
                hasNewLever = true;
                currentRoom?.RemoveItem(itemToTake.Name);

            }

            else if (itemNameLower == "berries")
            {
                hasBerries = true;
                Console.WriteLine($"You pick up the {itemToTake.Name}.");
                currentRoom?.RemoveItem(itemToTake.Name);
            }

            else if (itemNameLower == "pinecone")
            {
                hasPinecone = true;
                Console.WriteLine($"You pick up the {itemToTake.Name}.");
                currentRoom?.RemoveItem(itemToTake.Name);
            }
            else
            {

                Console.WriteLine($"You can't take the {itemNameLower}.");
            }


        }

        //entering the hydro controlroom
        private void TryMoveInside()
        {
            if (currentRoom == damplant)
            {
                if (hasDamKey)
                {
                    previousRoom = currentRoom;
                    currentRoom = controlroom;
                    Console.WriteLine("The key clicks perfectly into a hidden lock next to the entrance.");
                    Console.WriteLine("You turn the key and the heavy door slides open. You step inside.\r\n" +
                    "Now you can see the rusty restart lever, you can use the items you collected to fix it (use item)");
                }
                else
                {

                    Console.WriteLine("The entrance to the control room is sealed shut. It looks like it requires a specialized key.");
                }
            }
            else
            {
                Console.WriteLine("You can only go 'inside' the control room from the Dam Plant.");
            }
        }



        //useitem command
        private void UseItem(Command command)
        {
            if (!command.HasSecondWord())
            {
                Console.WriteLine("Use what?");
                return;
            }

            string itemName = command.SecondWord.ToLower();

            if (itemName == "lever" && hasNewLever && currentRoom?.ShortDescription == "Control room")
            {
                if (!leverRepaired)
                {
                    leverRepaired = true;
                    Console.WriteLine("You successfully replace the rusted lever with the heavy, stainless steel one. It moves smoothly now.");
                    Console.WriteLine("The control panel is ready for use! You can now use the panel to reactivate the dam.");
                    return;
                }
                else
                {
                    Console.WriteLine("The lever is already repaired and functioning.");
                    return;
                }
            }


            if (currentRoom != null && currentRoom.ShortDescription == "Control room" &&(itemName == "berries" || itemName == "pinecone"))
            {
    
            if (hasBerries && hasPinecone)
            {
            Console.WriteLine($"You use the {itemName} to begin combining the berry juice and pinecone dust...");
            QTE_Start();
            return;
            }
            else
            {
            Console.WriteLine("You need both the berry juice and the pinecone dust to create the derusting acid.");
            if (!hasBerries) Console.WriteLine("Missing: Berries (Find them in the Tundra forrest)");
            if (!hasPinecone) Console.WriteLine("Missing: Pinecone (Find it in the Tundra forrest)");
            return;
            }
            }


            Console.WriteLine($"You cannot currently use the {itemName} here or you don't possess it.");
        }

        //HydroQTE start + requirements
        private void QTE_Start()
        {
            if (qteComplete)
            {
                Console.WriteLine("The control panel is already active.");
                return;
            }

            if (leverRepaired)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nThe New lever is in place! With a strong pull, the emergency sequence is initiated and **completed instantly**.");
                Console.WriteLine("✅ QTE BYPASSED! The Hydroelectric Dam is now back!");
                Console.ResetColor();
                QTE_Win();
                return;
            }

            if (hasBerries && hasPinecone)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n*** QTE ACTIVATED ***");
                Console.WriteLine("You now have started to try and derust the lever, however it requires a delicate balance between pine dust and berry juice");
                Console.WriteLine("Be ready! You must press the corresponding key within 1 second!");
                Console.ResetColor();

                successfulPresses = 0;
                StartQTE();
            }
            else
            {
                Console.WriteLine("You need a successful lever repair, or both the berries AND the pinecone to activate the emergency sequence.");
            }
        }

        //HydroQTE game
        private void StartQTE()
        {
            ConsoleKey[] possibleKeys = { ConsoleKey.A, ConsoleKey.S, ConsoleKey.D, ConsoleKey.J, ConsoleKey.K, ConsoleKey.L };
            Random random = new Random();

            while (successfulPresses < QuickTimeEvents)
            {
                Console.WriteLine("\n----------------------------------");
                Console.WriteLine($"Initiating Challenge {successfulPresses + 1}...");

                Thread.Sleep(3000);

                ConsoleKey requiredKey = possibleKeys[random.Next(possibleKeys.Length)];

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\n PRESS **{requiredKey}** NOW!");
                Console.ResetColor();

                if (CheckKey(requiredKey))
                {
                    successfulPresses++;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" Success! Moving to the next step...");
                    Console.ResetColor();
                }
                else
                {
                    QTE_Fail();
                    return;
                }
            }
            QTE_Win();
        }


        //the checking system for the hydro minigame
        private bool CheckKey(ConsoleKey requiredKey)
        {            
            DateTime startTime = DateTime.Now;

            int timeLimitMs = 1000;

            while ((DateTime.Now - startTime).TotalMilliseconds < timeLimitMs)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyPress = Console.ReadKey(intercept: true);

                    if (keyPress.Key == requiredKey)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                Thread.Sleep(5);
            }
            return false; 
        }


        private void QTE_Win()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n SUCCESS! ");
            Console.WriteLine("The control panel lights up fully, confirming the **Hydroelectric Dam is now back to full power!**");
            Console.WriteLine("You have completed the Hydro challenge and restored power to the region.");
            Console.ResetColor();
            qteComplete = true;
        }


        private void QTE_Fail()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n FAILURE! ");
            Console.WriteLine("You failed to input the correct sequence in time.");
            Console.WriteLine("You must clean off and reset the acid.");
            Console.ResetColor();

            successfulPresses = 0;

            Thread.Sleep(10000);
            Console.WriteLine("\n>>> 10-Second cleanup done. You may attempt the reaction again. <<<");
        }




        private static void PrintWelcome()
        {
            Console.WriteLine("The year is 2075. The world has fallen into darkness. Cities flicker, villages wait in silence. " +
                "\r\nYou are part of Project Aurora — humanity’s last hope to restore clean power. " +
                "\r\nArmed with your tools, knowledge, and determination, you set out to bring light back.\r\n");

            Console.WriteLine("Your mission: travel to four regions and repair their energy plants. " +
                "\r\nYour choices will shape the future of humanity.\r\n");

            Console.WriteLine("You can journey in 1 of 4 directions, you can pick the River Valley(north) Solar Desert(west), \r\n" +
                "Volcanic Plains(east), or the Windy Highlands(south)");

            PrintHelp();
            Console.WriteLine();
        }

        private static void PrintHelp()
        {
            Console.WriteLine("Navigate by typing the name of the room you would like to go to.");
            Console.WriteLine("Type 'look' for more details.");
            Console.WriteLine("Type 'back' to go to the previous room.");
            Console.WriteLine("Type 'help' to print this message again.");
            Console.WriteLine("Type 'quit' to exit the game.");
        }
    }
}
