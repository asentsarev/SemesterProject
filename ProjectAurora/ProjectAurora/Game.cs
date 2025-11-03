namespace ProjectAurora
{
    public class Game
    {
        private static Room? currentRoom;
        private static Room? previousRoom;
        private static List<Room>? rooms;

        public Game()
        {
            CreateRooms();
            CreateNPCs(rooms);
        }

        public void CreateRooms()
        {

            Room? start = new Room("Aurora Control Hub", "You stand inside the Aurora Control Hub, the heart of the last renewable energy initiative." +
                "\r\nThe air hums with faint backup power. Screens flicker, showing maps of four darkened regions." +
                "\r\nA workbench lies in the corner with scattered tools.\r\n");
            rooms.Add(start);
            Room? solarDesert = new Room("Solar Desert", "The desert stretches before you. Towers of sand cover the solar field. Heat shimmers across the horizon." +
                "\r\nThere you meet Dr. Liora Sunvale");
            rooms.Add(solarDesert);
            currentRoom = start;


            // hydro rooms and their items
            Room? hub = new("Hydro Hub", "You started walking towards the river. But you reach an area where the road separates into 4. Luckily there is a sign that reads =Welcome, you are at the Hydro Hub= /n = Outside(south), Research Center(north), Hydroelectric dam(east), Tundra forrest(west)=");

            rooms? damplant = new("The Dam Plant", "After a short stroll you arrive at the riverside with no bridge leading acoross. However there is a an obstruction that connects the two sides: A huge Hydroelectric Plant. And you see an enterance, that you can enter(inside) however there is a lock on the door");
            
            Room? researchcenter = new("Research Center", "You enter inside the lobby of an Aurora outpost, this building gives help and guidiance to engineers on their missions, it has 4 sections: a Library(up), the Cafeteria(right), the Lab(left) and an ominous Basement(down). The way back to the Hydro Hub is South");
            
            Room? resourcearea = new("Tundra Forrest", "You have entered the Tundra forrest.");
            Item berries = new("berries", "A cluster of edible-looking berries. Maybe they could be used as bait or food.");
            resourcearea.AddItem(berries);
            Item pinecone = new("pinecone", "A large, sticky pinecone. It feels heavy, perhaps useful for starting a fire.");
            resourcearea.AddItem(pinecone);            
            
            Room? library = new("Library", "Loads of heavy shelves hold thousands of technical theory, documents and old logbooks. A single chair is occupied by a person reading one of the books. The lobby is downstrairs(down)");
            
            Room? cafeteria = new("Caferetia", "A regular cafeteria. Near the serving station, you see a small, misplaced item lying among the cutlery. It looks like a key, dou you take it?(take key) It might come in handy later... The lobby is to the Left(left)."); Item damKey = new("key", "A small metal tool with the Aurora symbol on it. It doesn't quite fit the shape of the rest of the spoons and forks, perhaps you should investigate? (take) ");
            cafeteria.AddItem(damKey);

            Room? hydrolab = new("Hydro Lab", "A workspace cluttered with beakers and other equipment. A researcher in a clean white coat is hard at work. The lobby is to the Right(right).");
            
            Room? basement = new("Basement", "A dark, seemingly abandoned utility area. The air is cold and heavy. You see a series of locked storage crates. The lobby is Up(up).");
            
            Room? bonus = new("Top of the Hill", "After climbing up the hill you find a forgotten toolbox. You see a box labeled levers. The only way down is back to the Tundra (south).");
            Item lever = new("lever", "A heavy, stainless steel lever. It looks like it could replace a rusted, jammed control.");
            bonus.AddItem(lever);
            
            controlroom = new("Control room", "You walk deep inside the dam to the Control room. Directly ahead is the emergency restart control panel with the restart lever marked, however the levers are completely rusted and jammed shut. The only way to go is leaving and going back out to the DampPlant(outside).");


            // hydro area directions
            hub.SetExit("south", outside);
            hub.SetExit("east", damplant);
            hub.SetExit("north", researchcenter);
            hub.SetExit("west", resourcearea);
            damplant.SetExit("west", hub);
            damplant.SetExit("inside", controlroom);
            controlroom.SetExit("outside", damplant);
            researchcenter.SetExit("south", hub);
            researchcenter.SetExit("up", library);
            researchcenter.SetExit("right", cafeteria);
            researchcenter.SetExit("left", hydrolab);
            researchcenter.SetExit("down", basement);
            library.SetExit("down", researchcenter);
            cafeteria.SetExit("left", researchcenter);
            hydrolab.SetExit("right", researchcenter);
            basement.SetExit("up", researchcenter);
            resourcearea.SetExit("east", hub);
            resourcearea.SetExit("north", bonus);
            bonus.SetExit("south", resourcearea);


        }

        private void CreateNPCs(List<Room> rooms)
        {
            NPC Liora = new NPC("Dr. Liora Sunvale", "", rooms.Find(x => x.ShortDescription == "Solar Desert"));
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
    public class Item
{
    private string name;
    private string description;

    public string Name { get { return name; } }
    public string Description { get { return description; } }
    
    public Item(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    public string GetLongDescription()
    {
        return Name + ": " + Description;
    }
}

    
}
