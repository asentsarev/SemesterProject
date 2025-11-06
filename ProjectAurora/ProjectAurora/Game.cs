namespace ProjectAurora
{
    public class Game
    {
        private static Room? currentRoom;
        private static Room? previousRoom;
        private bool hasDamKey = false;
        private bool talkedToLiora = false;
        private bool hasDesertKey = false;
        private static Room? maintenenceTent;

        private Room? damplant;
        private Room? controlroom;
        public Game()
        {
            CreateRooms();
            CreateNPCs();
        }

        public void CreateRooms()
        {

            Room? start = new Room("Aurora Control Hub", "You stand inside the Aurora Control Hub, the heart of the last renewable energy initiative." +
                "\r\nThe air hums with faint backup power. Screens flicker, showing maps of four darkened regions." +
                "\r\nA workbench lies in the corner with scattered tools.\r\n");
            currentRoom = start;

            // solar rooms and their items
            Room? solarDesert = new Room("Solar Desert", "After walking for an hour you find yourself in a desolate land.\r\n" +
                "The desert stretches before you. Towers of sand cover the solar field. Heat shimmers across the horizon.\r\n");
            Room? desertHub = new Room("Desert Hub", "You find a small hub that looks like it could have life.\r\n" +
                "You notice a map in front of the hub with the areas in the desert:\r\n" +
                "Maintenence tent(west), Aurora Hub(east), Solar panel field(north), Junkyard(south)\r\n" +
                "You decide to go inside and there you find Dr. Liora Sunvale\r\n" +
                "She welcomes you inside and is ready to answer your questions. (talk)\r\n");

            Room? maintenenceTentOutside = new Room("Maintence tent outside","Before going in, the scientist guarding the tent ask you a question: \r\n" +
                "'What happens if solar panels overheat?'\r\n" +
                "Your options are: (1) More energy, (2) Less efficiency, (3) Catch fire\r\n");
            Console.Write("> ");

            maintenenceTent = new Room("Maintenence tent", "You go inside the tent and there greets you a wooden box with the text\r\n" +
                "Resource area\r\n" +
                "You chose to open to the box and inside it is a key. You will need it for your progress in the Solar Desert, so you take the key.\r\n");

            desertHub.AddNPC("Dr. Liora Sunvale", "Welcome young scientist!\r\n" +
                "Our mission is to save the 'Solar panel field' and find all burried solar panels!\r\n" +
                "We have thought of 2 methods of doing it and you're more than welcome to chose which one you preffer.\r\n" +
                "1 of them is a temporary fix, so choose wisely!\r\n" +
                "Visit the maintenece tent to get more information.(west)\r\n");

            Room? solarPanelFields = new Room("Solar Panel Fields","You find yourself in the Solar Panel Fields and notice a lot of piles of sand.\r\n" +
                "You try to dig into one and you find a solar panel. There are thousands of them.\r\n" +
                "How will you clean up the piles:\r\n" +
                "(1) Water Hose (unreliable) (2) Robotic maintenece\r\n");
            Console.Write("> ");


            //add rooms if talked
            if (currentRoom == desertHub && talkedToLiora)
            {

            }


            // hydro rooms and their items
            Room? hydroHub = new("Hydro Hub", "You started walking towards the river. But you reach an area where the road separates into 4. " +
                "\r\nLuckily there is a sign that reads:\r\n" +
                "=Welcome, you are at the Hydro Hub=\r\n" +
                "=Outside(south), Research Center(north), Hydroelectric dam(east), Tundra forrest(west)=\r\n");

            Room? damplant = new("The Dam Plant", "After a short stroll you arrive at the riverside with no bridge leading acoross. \r\n" +
                "However there is a an obstruction that connects the two sides: A huge Hydroelectric Plant. And you see an enterance, that you can enter(inside) \r\n" +
                "however there is a lock on the door\r\n");

            Room? researchcenter = new("Research Center", "You enter inside the lobby of an Aurora outpost, this building gives help and guidiance to engineers on their missions, \r\n" +
                "it has 4 sections: a Library(up), the Cafeteria(right), the Lab(left) and an ominous Basement(down). The way back to the Hydro Hub is South\r\n");

            Room? hydroResourcearea = new("Tundra Forrest", "You have entered the Tundra forrest.\r\n");
            Item berries = new("berries", "A cluster of edible-looking berries. Maybe they could be used as bait or food.\r\n");
            hydroResourcearea.AddItem(berries);
            Item pinecone = new("pinecone", "A large, sticky pinecone. It feels heavy, perhaps useful for starting a fire.\r\n");
            hydroResourcearea.AddItem(pinecone);

            Room? library = new("Library", "Loads of heavy shelves hold thousands of technical theory, documents and old logbooks. \r\n" +
                "A single chair is occupied by a person reading one of the books. \r\n" +
                "The lobby is downstrairs(down)\r\n");

            Room? cafeteria = new("Caferetia", "A regular cafeteria. Near the serving station, you see a small, misplaced item lying among the cutlery. It looks like a key, dou you take it?(take key) It might come in handy later... \r\n" +
                "The lobby is to the Left(left).\r\n"); 
            
            Item damKey = new("key", "A small metal tool with the Aurora symbol on it. It doesn't quite fit the shape of the rest of the spoons and forks, perhaps you should investigate? (take) \r\n");
            cafeteria.AddItem(damKey);

            Room? hydrolab = new("Hydro Lab", "A workspace cluttered with beakers and other equipment. A researcher in a clean white coat is hard at work. The lobby is to the Right(right).\r\n");

            Room? basement = new("Basement", "A dark, seemingly abandoned utility area. The air is cold and heavy. You see a series of locked storage crates. The lobby is Up(up).\r\n");

            Room? bonus = new("Top of the Hill", "After climbing up the hill you find a forgotten toolbox. You see a box labeled levers. The only way down is back to the Tundra (south).\r\n");
            Item lever = new("lever", "A heavy, stainless steel lever. It looks like it could replace a rusted, jammed control.\r\n");
            bonus.AddItem(lever);

            controlroom = new("Control room", "You walk deep inside the dam to the Control room. Directly ahead is the emergency restart control panel with the restart lever marked, \r\n" +
                "however the levers are completely rusted and jammed shut. The only way to go is leaving and going back out to the DampPlant(outside).\r\n");


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
            researchcenter.SetExit("left", hydrolab);
            researchcenter.SetExit("down", basement);
            library.SetExit("down", researchcenter);
            cafeteria.SetExit("left", researchcenter);
            hydrolab.SetExit("right", researchcenter);
            basement.SetExit("up", researchcenter);
            hydroResourcearea.SetExit("east", hydroHub);
            hydroResourcearea.SetExit("north", bonus);
            bonus.SetExit("south", hydroResourcearea);
            start.SetExit("north", hydroHub);

            
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

            currentRoom = outside;

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
                {Console.WriteLine(currentRoom?.LongDescription);
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
                            break;

                        case "2":
                            Console.WriteLine("Good job! You got it right.");
                            currentRoom = maintenenceTent;
                            hasDesertKey = true;
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine($"You can't go {direction}!");
            }
        }


        //take command
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
                hasDamKey = true; // key flag for the door dam door
                currentRoom?.RemoveItem(itemToTake.Name);
                Console.WriteLine($"You take the {itemToTake.Name}.");
            }
            else if (itemNameLower == "lever" || itemNameLower == "berries" || itemNameLower == "pinecone")
            {


                currentRoom?.RemoveItem(itemToTake.Name);
                Console.WriteLine($"You take the {itemToTake.Name}.");
            }
            else
            {

                Console.WriteLine($"You can't take the {itemNameLower}.");
            }


        }

        private void TryMoveInside()
        {
            if (currentRoom == damplant) //Check if player is at the right location (ie Dam Plant)
            {
                if (hasDamKey) //check if player has the key
                {
                    previousRoom = currentRoom;
                    currentRoom = controlroom; // Move to the Control Room
                    Console.WriteLine("The key clicks perfectly into a hidden lock next to the entrance.");
                    Console.WriteLine("You turn the key and the heavy door slides open. You step inside.");
                }
                else
                {
                    //if the player doesnt have the hydro key
                    Console.WriteLine("The entrance to the control room is sealed shut. It looks like it requires a specialized key.");
                }
            }
            else //if the player is not at the Dam Plant
            {
                Console.WriteLine("You can only go 'inside' the control room from the Dam Plant.");
            }
        }




        private static void PrintWelcome()
        {
            Console.WriteLine("The year is 2075. The world has fallen into darkness. Cities flicker, villages wait in silence. " +
                "\r\nYou are part of Project Aurora — humanity’s last hope to restore clean power. " +
                "\r\nArmed with your tools, knowledge, and determination, you set out to bring light back.\r\n");

            Console.WriteLine("Your mission: travel to four regions and repair their energy plants. " +
                "\r\nYour choices will shape the future of humanity.\r\n");

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
