namespace WorldOfZuul
{
    public class Game
    {
        private Room? currentRoom;
        private Room? previousRoom;
        private bool hasDamKey = false;
        public Game()
        {
            CreateRooms();
        }

        private void CreateRooms()
        {

            Room? outside = new("Outside", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.");
            Room? theatre = new("Theatre", "You find yourself inside a large lecture theatre. Rows of seats ascend up to the back, and there's a podium at the front. It's quite dark and quiet.");
            Room? pub = new("Pub", "You've entered the campus pub. It's a cozy place, with a few students chatting over drinks. There's a bar near you and some pool tables at the far end.");
            Room? lab = new("Lab", "You're in a computing lab. Desks with computers line the walls, and there's an office to the east. The hum of machines fills the room.");
            Room? office = new("Office", "You've entered what seems to be an administration office. There's a large desk with a computer on it, and some bookshelves lining one wall.");

            //main hydro rooms rooms
            Room? hub = new("Hydro Hub", "You started walking towards the river. Now you reach a cliff where the road separates into 4. Luckily there is a sign that reads: =Welcome, you are at the Hydro Hub= under the greeting: =South -outside, North -Research Center, East -Hydroelectric dam, West -Tundra forrest.=");
            Room? damplant = new("The Dam Plant", "After a short stroll you arrive at a riverside with no bridge leading acoross. However there is a an obstruction that connects the two sides: A huge Hydroelectric Plant. And you see an enterance");
            Room? researchcenter = new("Research Center", "You stand inside the lobby of an Aurora outpost, this building gives help and guidiance to engineers on their missions, it has 4 sections: a Library(up), the Cafeteria(right), the Lab(left) and an ominous Basement(down). The way back to the Hydro Hub is South");
            Room? resourcearea = new("Tundra Forrest", "You have entered the Tundra forrest.");

            //hydro research center sub rooms
            Room? library = new("Library", "Loads of heavy shelves hold thousands of technical theory, documents and old logbooks. A single chair is occupied by a person reading one of the books. The lobby is down");
            Room? cafeteria = new("Caferetia", "A regular cafeteria where a few people enjoying a meal. Near the serving station, you see some some plates and cutlery but one of the utensils seems to be misplaced, You can try and put the misplaced cutlery back where it belongs (take). The lobby is to the Left.");
            Item damKey = new("Misplaced utensil", "A small metal tool with the Aurora symbol on it. It doesn't quite fit the shape of the rest of the spoons and forks, perhaps you should investigate? (take) ");
            cafeteria.AddItem(damKey);      
           
            Room? lab = new("Lab", "A workspace cluttered with beakers and other equipment. A researcher in a clean white coat is hard at work. The entrance is to the Right.");
            Room? basement = new("Basement", "A dark, seemingly abandoned utility area. The air is cold and heavy. You see a series of locked storage crates. The entrance is Up.");
            
            //further rooms

            Room? bonus = new("Top of the Hill", "After climbing up the hill you find a forgotten toolbox. You see a box labeled levers. The only way down is back South to the Tundra.");
            Room? controlroom = new("Control room", "You walk deep inside the dam to the Control room. Directly ahead is the emergency restart control panel with the restart lever marked, however the levers are completely rusted and jammed shut. The only way is outside to the DampPlant.");

            outside.SetExits(hub, theatre, lab, pub); // North, East, South, West [I added my region hub to the North]

            //area directions from the hydro hub
            hub.SetExit("south", outside); // [Added the reverse way back from the hub to the outside South]
            hub.SetExit("east", damplant);
            hub.SetExit("north", researchcenter);
            hub.SetExit("west", resourcearea);


            //area directions from the DamPlant
            damplant.SetExit("west", hub);
            DamPlant.SetExit("inside", turbineChamber);

            //directions from the Dam Control Room
            turbineChamber.SetExit("outside", DamPlant);

            //area directions from the Hydro Center
            researchcenter.SetExit("south", hub);
            researchCenter.SetExit("up", library);
            researchCenter.SetExit("right", cafeteria);
            researchCenter.SetExit("left", lab);
            researchCenter.SetExit("down", basement);

            // directions back to the Hydro Research Center lobby
            library.SetExit("down", ResearchCenter);
            cafeteria.SetExit("left", ResearchCenter);
            lab.SetExit("right", ResearchCenter);
            basement.SetExit("up", ResearchCenter);



            //area directions from the Tundra 
            resourcearea.SetExit("east", hub);
            ResourceArea.SetExit("north", bonus);

            //directions from the hydro bonus room 
            bonus.SetExit("south", resourcearea);

            theatre.SetExit("west", outside);

            pub.SetExit("east", outside);

            lab.SetExits(outside, office, null, null);

            office.SetExit("west", lab);

            currentRoom = outside;


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
                        Move(command.Name);
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

            Console.WriteLine("Thank you for playing World of Zuul!");
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

            // Simple 'flag' logic: check if the item is the Misplaced utensil.
            if (itemToTake.Name.ToLower() == "misplaced utensil" || itemToTake.Name.ToLower() == "dam key")
            {
                hasDamKey = true;
                currentRoom?.RemoveItem(itemToTake.Name);
                Console.WriteLine($"You take the {itemToTake.Name}. Its weight is noticeable.");
            }
            else
            {
                Console.WriteLine($"You can only take the Misplaced utensil right now.");
            }
        }


        private static void PrintWelcome()
        {
            Console.WriteLine("Welcome to the World of Zuul!");
            Console.WriteLine("World of Zuul is a new, incredibly boring adventure game.");
            PrintHelp();
            Console.WriteLine();
        }

        private static void PrintHelp()
        {
            Console.WriteLine("You are lost. You are alone. You wander");
            Console.WriteLine("around the university.");
            Console.WriteLine();
            Console.WriteLine("Navigate by typing 'north', 'south', 'east', or 'west'.");
            Console.WriteLine("Type 'look' for more details.");
            Console.WriteLine("Type 'back' to go to the previous room.");
            Console.WriteLine("Type 'help' to print this message again.");
            Console.WriteLine("Type 'quit' to exit the game.");
        }
    }

public class Item
{
    // Fields
    private string name;
    private string description;

    // Properties
    public string Name { get { return name; } }
    public string Description { get { return description; } }

    /**
     * Constructor for items.
     */
    public Item(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    
      //Returns the long description of the item (its name and description).
    
    public string GetLongDescription()
    {
        return Name + ": " + Description;
    }
}







}
